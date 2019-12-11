using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kayar19.Models
{
    public class UserCount
    {
        [JsonProperty("NumberOfUsers")]
        public string NumberOfUsers { get; set; }
    }
    public class UserCounter
    {
            public List<UserCount> data { get; set; }
            public bool success { get; set; }

        }
}
