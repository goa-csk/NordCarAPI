using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace SOSIntegrator
{
    [RunInstaller(true)]
    public partial class SOSServiceInstaller : System.Configuration.Install.Installer
    {
        private ServiceInstaller sosserviceInstaller;
        private ServiceProcessInstaller processInstaller;
        public SOSServiceInstaller()
        {
            // Instantiate installers for process and services.
            processInstaller = new ServiceProcessInstaller();
            sosserviceInstaller = new ServiceInstaller();
           
            // The services run under the system account.
            processInstaller.Account = ServiceAccount.LocalSystem;

            // The services are started manually.
            sosserviceInstaller.StartType = ServiceStartMode.Manual;
          
            // ServiceName must equal those on ServiceBase derived classes.
            sosserviceInstaller.ServiceName = "Service1";
         
            // Add installers to collection. Order is not important.
            Installers.Add(sosserviceInstaller);
            Installers.Add(processInstaller);
            //InitializeComponent();
        }

        //public static void Main()
        //{
        //    Console.WriteLine("Usage: InstallUtil.exe [<service>.exe]");
        //}
    }
}
