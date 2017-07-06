//using Common.Logging;
using log4net.Config;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Triggers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Repository.Hierarchy;
using log4net.Appender;
using System.Globalization;
using System.IO;
using NordCar.Carla.Data.Infrastructure;
using SOSIntegrator.DAHSOS;
using System.ServiceModel;
//using log4net.Config;

namespace SOSIntegrator
{
    public partial class Service1 : ServiceBase
    {
        private const string Group1 = "BusinessTasks";
        private const string Job = "Job";
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
       
        private static IScheduler _scheduler;
        
        public Service1()
        {
            InitializeComponent();
            
        }

        public static string LoggerFile()
        {
            var rootAppender = ((Hierarchy)LogManager.GetRepository())
                                             .Root.Appenders.OfType<FileAppender>()
                                             .FirstOrDefault();
            string filename = rootAppender != null ? rootAppender.File : string.Empty;

            return filename;

        }
        public void OnDebug()
        {
            OnStart(null);
        }

        protected override void OnStart(string[] args)
        {
            XmlConfigurator.Configure();

            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
            _scheduler = schedulerFactory.GetScheduler();
            _scheduler.Start();
            System.IO.Directory.SetCurrentDirectory(System.AppDomain.CurrentDomain.BaseDirectory);
            Log.Info(string.Format("Starting Windows Service path : {0}", System.AppDomain.CurrentDomain.BaseDirectory));
            Log.Info(string.Format("Carla connection ip={0} port={1}", Properties.Settings.Default.Ip7913, Properties.Settings.Default.Port7913));
            AddJobs();
            }

        private void AddJobs()
        {
            AddHealthMonitoringJob();
            AddRegistrationsToSOS();
       
        }

        public static void AddHealthMonitoringJob()
        {
            const string trigger1 = "HealthMonitoring";

            IDoJob myJob = new HealthMonitiorJob();
            var jobDetail = new JobDetailImpl(trigger1 + Job, Group1, myJob.GetType());
            var trigger = new CronTriggerImpl(
                trigger1,
                Group1,
                Properties.Settings.Default.HealthMonTimer /* every 10 minutes */
                ) { TimeZone = TimeZoneInfo.Utc };
            _scheduler.ScheduleJob(jobDetail, trigger);
            var nextFireTime = trigger.GetNextFireTimeUtc();
            if (nextFireTime != null)
                Log.Info(Group1 + "+" + trigger1, new Exception(nextFireTime.Value.ToString("u")));
        }

        public class HealthMonitiorJob : IDoJob
        {
            public void Execute(IJobExecutionContext context)
            {
                Log.Info(DateTime.UtcNow);
            }
        }

        private void AddRegistrationsToSOS()
        {
            const string trigger1 = "ExportTasksTrigger";
            const string jobName = trigger1 + Job;
            IDoJob myJob = new RegistrationsToSOS();
            var jobDetail = new JobDetailImpl(jobName, Group1, myJob.GetType());
            var trigger = new CronTriggerImpl(
                trigger1,
                Group1,
                Properties.Settings.Default.ServiceQuartzTimer  /* run every day at 2:00 UTC */ ) { TimeZone = TimeZoneInfo.Utc };
            _scheduler.ScheduleJob(jobDetail, trigger);
            var nextFireTime = trigger.GetNextFireTimeUtc();
            if (nextFireTime != null)
                Log.Debug(Group1 + "+" + trigger1, new Exception(nextFireTime.Value.ToString("u")));  
        }

        internal class RegistrationsToSOS : IDoJob
        {
            private NordCar.Carla.Data.Implementation.ECAPIManagerRepository ecr;
            private NordCar.Carla.Data.Entities.BasicStructure basic;
         
            public RegistrationsToSOS()
            {
                basic = new NordCar.Carla.Data.Entities.BasicStructure()
                     {
                         BookTypes = "SOS"
                     };

                ecr = new NordCar.Carla.Data.Implementation.ECAPIManagerRepository(Properties.Settings.Default.Ip7913, Properties.Settings.Default.Port7913, LoggerFile());
            }

            public void ProcessRegistrations()
            {
                try
                {
                    //basic.FunctionId = NordCar.Carla.Data.Entities.FunctionList.GetReservationStatusQueue;
                    var res = ecr.GetReservationStatusQueue(basic, "199696");
                    if (res.Item1.Succes && res.Item2.Count > 0)
                    {
                        var resitems = res.Item2;

                        foreach (var item in resitems)
                        {
                            Log.Info(string.Format("Message ID = {0}",item.MessageId));
                            //basic.FunctionId = NordCar.Carla.Data.Entities.FunctionList.GetResRAData;
                            string response = "";
                  
                            if (item.RANumber != "0")
                            {
                                //RA
                                response = HandleRA(item);

                                
                            }
                            else
                            { 
                                //RES
                  
                                var reservation = ecr.GetResRAData(basic, 1, item.ReservationNumber);
                                Log.Info("Reservations nummer:" + reservation.Item2.ReservationNumber);
                        
                            }
                            
                            DeleteMessage(basic,item,response);
                        }
                    }
                    else
                    {
                        //Log.Error(string.Format("Fejl {0}-{1}-{2}-{3} ", res.Item1.MessageId, res.Item1.Message, res.Item1.ErrorCode, res.Item1.ErrorMessage));
                        Log.Info(string.Format("Queue empty {0}-{1}-{2}-{3} ", res.Item1.MessageId, res.Item1.Message, res.Item1.ErrorCode, res.Item1.ErrorMessage));
                    }
                    
           
                }
                catch (Exception ex)
                {
                    Log.Error("ProcessRegistrations:" + ex);
                }
                
            }

            private string HandleRA(NordCar.Carla.Data.Entities.EC.QueueInfo item)
            {
                try
                {
                    var ecdata = ecr.GetResRAData(basic, 2, item.RANumber);
                    Log.Info("Reservationsnummer:" + ecdata.Item2.ReservationNumber);

                    var soscons = new List<DAHSOS.Contract>();
                    var soscon = new DAHSOS.Contract();

                    soscon.AdaptorCable = ecdata.Item2.Towbar;
                    soscon.Automatic = ecdata.Item2.Automatic;
                    soscon.CarBrand = ecdata.Item2.Brand;
                    soscon.CarFirstRegistered = ecdata.Item2.FirstRegistrationDate;
                    soscon.CarModel = ecdata.Item2.Model;
                    soscon.ChildSeat = ecdata.Item2.ChildSeat;
                    soscon.ContractCreated = ecdata.Item2.RACreatedTime.ToUniversalTime();
                    soscon.ContractNumber = int.Parse(ecdata.Item2.RANumber);
                    soscon.CurrentContractStatus = (DAHSOS.ContractStatus)int.Parse(ecdata.Item2.Status);
                    soscon.ExtraDriver = ecdata.Item2.ExtraDriver;
                    soscon.GpsNavigation = ecdata.Item2.GPS;
                    soscon.Hitch = ecdata.Item2.Towbar;
                    soscon.InvoiceCurrencyCode = ecdata.Item2.InvoiceCurrencyCode;
                    soscon.InvoicedFuelLitre = ecdata.Item2.InvoicedFuelLitre;
                    soscon.InvoicedFuelPrice = ecdata.Item2.InvoicedFuelPrice;
                    soscon.LicensePlate = ecdata.Item2.CarRegistrationNumber;
                    soscon.MilageRegisteredOnContract = ecdata.Item2.MilageRegisteredOnContract;
                    soscon.WinterTires = ecdata.Item2.WinterTires;
                    var Ext = new SOSIntegrator.DAHSOS.ReservationEvent() { StationNumber = int.Parse(ecdata.Item2.StationNoOut), EventTime = Helpers.FromLocalCarlaTimeToUTC(ecdata.Item2.DateOut, ecdata.Item2.TimeOut), ExtensionData = null };
                    var Fil = new SOSIntegrator.DAHSOS.ReservationEvent() { StationNumber = int.Parse(ecdata.Item2.StationNoIn), EventTime = Helpers.FromLocalCarlaTimeToUTC(ecdata.Item2.DateIn, ecdata.Item2.TimeIn), ExtensionData = null };
                    soscon.Extradition = Ext;
                    soscon.Filing = Fil;

                    Log.Info("Object to send:" + Serialize.SerializeObject(soscon));

                    soscons.Add(soscon);

                    var resContext = new DAHSOS.ReservationStatusContext();
                    resContext.SosCaseId = ecdata.Item2.CustomerReferenceNumber == "" ? "0" : ecdata.Item2.CustomerReferenceNumber;
                    resContext.Contracts = soscons.ToArray();
                    resContext.ReservationNumber = int.Parse(item.ReservationNumber);

                    var client = new DAHSOS.EuropCarServiceClient();

                    string response = client.SetReservationStatus(resContext);
                    Log.Info("RA:" + item.RANumber + " " + response);

                    return response;
                }
                catch (FaultException ex)
                {
                    Log.Error("RA-FaultException:" + item.RANumber, ex);
                    return "SOS Service error occured";
                    
                }

                catch (Exception ex)
                {
                    Log.Error("RA:" + item.RANumber, ex);
                    throw ex;
                }
            }

            private void DeleteMessage(NordCar.Carla.Data.Entities.BasicStructure basic,NordCar.Carla.Data.Entities.EC.QueueInfo item, string message)
            {
                try
                {
                    //basic.FunctionId = NordCar.Carla.Data.Entities.FunctionList.ReservationStatusQueueMessageProcessed;
                    var result = ecr.ReservationStatusQueueMessageProcessed(basic, item.MessageId,message);
                    Log.Info(string.Format("Deleted {0}", result.Item2));

                }
                catch (Exception ex)
                {
                    Log.Error(string.Format("Exception {0}", ex.ToString()));
                }
            }

            public void Execute(IJobExecutionContext context)
            {
                ProcessRegistrations();
            }   
        }
        protected override void OnStop()
        {
            Log.Info("Stopping Windows Service: ");
        }

        internal interface IDoJob : IJob
        {

        }
    }
}
