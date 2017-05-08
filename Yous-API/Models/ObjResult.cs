using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Yous_API.Models
{
    public class ObjResult
    {
        public bool success { get; set; }

        public string message { get; set; }

        public object data { get; set; }

        public string code { get; set; }
    }
}