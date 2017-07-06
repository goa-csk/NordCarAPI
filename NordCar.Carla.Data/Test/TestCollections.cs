using NordCar.Carla.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NordCar.Carla.Data.Test
{
    public class TestCollections
    {
        //Dur ikke, skal rettet
        public List<string> GetBrandList()
        {
            return new List<string>() {"Ukendt", "AUDI", "BMW", "Mercedes" };
        }

        //Dur ikke skal rettet
        public List<string> GetModelList(string brand)
        {
            switch (brand)
            {
                case "Ukendt":
                    return new List<string>() { "Ukendt"};
               
                case "AUDI" :
                    return new List<string>() { "Ukendt", "A1", "A2", "A3", "A4", "A5", "A6", "A7", "A8", "TT" };
               
                case "BMW":
                    return new List<string>() { "Ukendt","320", "330", "520", "530", "540" };

                case "Mercedes":
                    return new List<string>() { "Ukendt", "C180", "C200", "C220", "C230", "C240", "C250"};
               
                default :
                    return new List<string>() { "Ukendt" };
            }
        }

        public Car GetSpecificCar(string licensePlate)
        {
           return new Car() { LicensePlate = licensePlate, BrandName = "AUDI", CheckIn = "2014-05-30 23:59", Comment = "Dette er en test", CountryName = "DENMARK", Distance = 305, GroupName = "I", ModelName = "A4", NoOfSeat = 4, RATransfer = "XXXXYYYYZZZZ", StationNo = 6, Status = "Ok", Towbar = true, VinterTires = true, Fuel = 34.9D };
        }


        public List<CarOverview> GetCarList(string licensePlate)
        {
            var carOverviews = new List<CarOverview>();

            for(int i=0; i<=49; i++)
            {
                carOverviews.Add(new CarOverview() { BrandName = "AUDI", Licenseplate = "XX345" + i.ToString(), CheckIn = "12-02-2014", GroupName = "I", ModelName = "A4", RATransfer = "XXDFG" + i.ToString(), StationNo = "6", Status = "Ok" });
            }

            return carOverviews;
        
        }

        public List<CarStatus> GetCarStatusList()
        {
            var carStatuses = new List<CarStatus>();

            carStatuses.Add(new CarStatus() { Name = "New" });
            carStatuses.Add(new CarStatus() { Name = "Ready" });
            carStatuses.Add(new CarStatus() { Name = "Rented" });

            return carStatuses;
           

        }

        public List<CarOverview> Login(string userName, string password)
        {
            var carOverviews = new List<CarOverview>();

            for (int i = 0; i <= 49; i++)
            {
                carOverviews.Add(new CarOverview() { BrandName = "Mercedes", Licenseplate = "XX345" + i.ToString(), CheckIn = "12-02-2014", GroupName = "O", ModelName = "C220", RATransfer = "XXDFG" + i.ToString(), StationNo = "6", Status = "Ready" });
            }

            return carOverviews;

        }
    }
}
