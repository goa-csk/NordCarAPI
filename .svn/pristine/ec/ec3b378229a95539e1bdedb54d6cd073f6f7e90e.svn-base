using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using NordCar.Carla.Data.EF.Entities;
using SOSService.Data;


namespace SOSService.BootStrapper
{
    public class Bootstrapper : IBootstrapper
    {
        public void Initialize()
        {
            Mapper.CreateMap<NordCar.Carla.Data.EF.Entities.customerAgreement, Agreement>();
            Mapper.CreateMap<Agreement, NordCar.Carla.Data.EF.Entities.customerAgreement>();

            Mapper.CreateMap<NordCar.Carla.Data.Entities.EC.Location, Data.Location>();
            Mapper.CreateMap<Data.Location, NordCar.Carla.Data.Entities.EC.Location>();

            Mapper.CreateMap<NordCar.Carla.Data.Entities.EC.LocationDetail, Data.LocationDetail>();
            Mapper.CreateMap<Data.LocationDetail, NordCar.Carla.Data.Entities.EC.LocationDetail>();

            Mapper.CreateMap<NordCar.Carla.Data.Entities.EC.OpeningHours, Data.OpeningHours>();
            Mapper.CreateMap<Data.OpeningHours, NordCar.Carla.Data.Entities.EC.OpeningHours>();

            Mapper.CreateMap<NordCar.Carla.Data.Entities.EC.Exception, Data.Exception>();
            Mapper.CreateMap<Data.Exception, NordCar.Carla.Data.Entities.EC.Exception>();

       }
    }
}