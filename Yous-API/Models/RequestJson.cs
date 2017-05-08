using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Yous_Api.Models
{
    public class RequestJson
    {
        [DataMember(Order = 1)]
        public String Parameters { get; set; }

        [DataMember(Order = 2)]
        public String Method { get; set; }

        [DataMember(Order = 3)]
        public int ForeEndType { get; set; }

        [DataMember(Order = 4)]
        public String Code { get; set; }
    }
}