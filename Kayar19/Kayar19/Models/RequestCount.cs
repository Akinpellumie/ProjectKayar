using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kayar19.Models
{
    public class StaffRequestCount
    {
        [JsonProperty("NumberOfRequest")]
        public string NumberOfRequest { get; set; }
    }
    public class RequestCount
    {
            public List<StaffRequestCount> data { get; set; }
            public bool success { get; set; }

        }
    }
