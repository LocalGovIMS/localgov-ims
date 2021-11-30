using System.Runtime.Serialization;

namespace BusinessLogic.Models
{
    [DataContract]
    public class Response
    {
        [DataMember]
        public bool Success { get; set; }

        [DataMember]
        public string ErrorMessage { get; set; }

        [DataMember]
        public string PaymentId { get; set; }
    }
}