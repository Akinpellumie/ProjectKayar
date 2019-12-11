using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kayar19.Models
{
    public class StaffReqCount
    {
        [JsonProperty("NumberOfRequest")]
        public string NumberOfRequest { get; set; }
    }
    public class StaffCounter
    {
        public List<StaffReqCount> data { get; set; }
        public bool success { get; set; }

    }
}
