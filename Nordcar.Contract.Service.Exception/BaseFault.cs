using System.Runtime.Serialization;

namespace Nordcar.Contract.Service.Exception
{
        [DataContract]
        public class BaseFault
        {
            public BaseFault(string msg)
            {
                this.ErrorMessage = msg;
            }

            [DataMember]
            public string ErrorMessage { get; set; }
        }
    
}
