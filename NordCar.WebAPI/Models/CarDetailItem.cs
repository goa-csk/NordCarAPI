using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NordCar.WebAPI.Models
{
    //THIS CLASS MAPS TO CLIENT CLASS
    public class CarDetailItem
    {
        /// <summary>
        /// License plate
        /// </summary>
        [Required]
        [MaxLength(11)]
        public string licenseplate { get; set; }
        /// <summary>
        /// Car brand ex. BMW
        /// </summary>
        [Required]
        public string brand { get; set; }
        /// <summary>
        /// Car model ex. 520
        /// </summary>
        [Required]
        public string model { get; set; }
        /// <summary>
        /// Car clasification
        /// </summary>
        public string group { get; set; }
        /// <summary>
        /// Car nationality
        /// </summary>
        [Required]
        public string nationality { get; set; }
        /// <summary>
        /// Car arrival date and time 
        /// </summary>
        [Required]
        public string checkIn { get; set; }
        /// <summary>
        /// Foreign Contract number
        /// </summary>
        public string ra_transfer { get; set; }
        /// <summary>
        /// Fuel
        /// </summary>
        public double fuel { get; set; }
        /// <summary>
        /// The statiion that receive the foreign car
        /// </summary>
        [Required]
        public int station { get; set; }
        /// <summary>
        /// Km
        /// </summary>
        public int km { get; set; }
        /// <summary>
        /// Status always set to (1)OK
        /// </summary>
        [Required]
        public string status { get; set; }
        /// <summary>
        /// Comment
        /// </summary>
        public string comment { get; set; }
        /// <summary>
        /// Winter tires
        /// </summary>
        public bool vintertires { get; set; }
        /// <summary>
        /// Towbar
        /// </summary>
        public bool towbar { get; set; }
        /// <summary>
        /// Number of seats
        /// </summary>
        public int noofseats { get; set; }
        /// <summary>
        /// Automatic transmission
        /// </summary>
        public bool automatictransmission { get; set; }
        /// <summary>
        /// New car or car update
        /// </summary>
        [Required]
        public bool isNew { get; set; }
    }
}