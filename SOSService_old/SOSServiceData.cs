using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SOSService
{
    /// <summary>
    /// Agreement
    /// </summary>
    [DataContract]
    public class Agreement
    {     
        /// <summary>
        /// Number 
        /// </summary>
        [DataMember]
        public int Number { get; set; }
        
        /// <summary>
        /// Name
        /// </summary>
        [DataMember]
        public string Name { get;set; }

    }

    /// <summary>
    /// Address
    /// </summary>
    [DataContract]
    public class Address
    {
        /// <summary>
        /// Street
        /// </summary>
        [DataMember]
        public string Street { get; set; }

        /// <summary>
        /// City
        /// </summary>
        [DataMember]
        public string City { get; set; }

        /// <summary>
        /// ZipCode
        /// </summary>
        [DataMember]
        public string ZipCode { get; set; }

        /// <summary>
        /// Country
        /// </summary>
        [DataMember]
        public string Country { get; set; }

    }

    /// <summary>
    /// Rental
    /// </summary>
    [DataContract]
    public class Rental
    {
        /// <summary>
        /// Station out 
        /// </summary>
        [DataMember]
        public int StationOutId { get; set; }

        /// <summary>
        /// Date out
        /// </summary>
        [DataMember]
        public string DateOut { get; set; }

        /// <summary>
        /// Time out
        /// </summary>
        [DataMember]
        public string TimeOut { get; set; }

        /// <summary>
        /// Date in
        /// </summary>
        [DataMember]
        public string DateIn { get; set; }

        /// <summary>
        /// Time in
        /// </summary>
        [DataMember]
        public string TimeIn { get; set; }

        /// <summary>
        /// Information to EC department
        /// </summary>
        [DataMember]
        public string ECDepartmentInfo { get; set; }

        /// <summary>
        /// Information to SOS customer
        /// </summary>
        [DataMember]
        public string SOSCustomerInfo { get; set; }

        /// <summary>
        /// Winter tires
        /// </summary>
        [DataMember]
        public bool WinterTires { get; set; }

        /// <summary>
        /// Automatic
        /// </summary>
        [DataMember]
        public bool AutomaticGear { get; set; }

        /// <summary>
        /// Towbar
        /// </summary>
        [DataMember]
        public bool Towbar { get; set; }

        /// <summary>
        /// ChildSeat
        /// </summary>
        [DataMember]
        public bool ChilSeat { get; set; }

        /// <summary>
        /// GPS
        /// </summary>
        [DataMember]
        public bool GPS { get; set; }

        /// <summary>
        /// ExtraDriver
        /// </summary>
        [DataMember]
        public bool ExtraDriver { get; set; }

        /// <summary>
        /// FuelDeposit
        /// </summary>
        [DataMember]
        public bool FuelDeposit { get; set; }

        /// <summary>
        /// MaxGOP, credit limit
        /// </summary>
        [DataMember]
        public int MaxGOP { get; set; }

        /// <summary>
        /// Pick up
        /// </summary>
        [DataMember]
        public bool Pickup{ get; set; }

        /// <summary>
        /// Pickup adress
        /// </summary>
        [DataMember]
        public Address PickupAdress { get; set; }

        /// <summary>
        /// Delivery
        /// </summary>
        [DataMember]
        public bool Delivery { get; set; }

        /// <summary>
        /// Delivery adress
        /// </summary>
        [DataMember]
        public Address DeliveryAdress { get; set; }

        /// <summary>
        /// Car brand
        /// </summary>
        [DataMember]
        public string CarBrand { get; set; }

        /// <summary>
        /// Car model
        /// </summary>
        [DataMember]
        public string CarModel { get; set; }

    }

    /// <summary>
    /// Driver
    /// </summary>
    [DataContract]
    public class Driver
    {
        /// <summary>
        /// Number 
        /// </summary>
        [DataMember]
        public string Phone { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Adresss
        /// </summary>
        [DataMember]
        public Address address { get; set; }

    }

    /// <summary>
    /// Incomming reservation 
    /// </summary>
    [DataContract]
    public class Reservation
    {
        /// <summary>
        /// SOS Reference Id
        /// </summary>
        [DataMember]
        public int SOSId { get; set; }

        /// <summary>
        /// AgreementId
        /// </summary>
        [DataMember]
        public int AgreementId { get; set; }

        /// <summary>
        /// EC Reservation number,
        /// blank new reservation
        /// </summary>
        [DataMember]
        public string ECReservationNumer { get; set; }

        /// <summary>
        /// EC contract number,
        /// blank new reservation
        /// </summary>
        [DataMember]
        public string ECContractNumer { get; set; }

        /// <summary>
        /// Driver
        /// </summary>
        [DataMember]
        public Driver driver { get; set; }


    }
}