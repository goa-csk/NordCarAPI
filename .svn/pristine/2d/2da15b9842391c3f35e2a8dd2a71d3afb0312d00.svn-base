using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NordCar.Carla.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NordCar.WebAPI.Models.EC
{
    public class BookType
    {
        public string Name { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public BookTypeType BookTypeType { get; set; }
    }
}
