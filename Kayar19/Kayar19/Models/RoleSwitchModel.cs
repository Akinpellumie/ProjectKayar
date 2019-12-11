using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kayar19.Models
{
    public class RoleSwitchModel
    {
        [JsonProperty("Admin")]
        public string Admin { get; set; }

        [JsonProperty("StoreKeeper")]
        public string StoreKeeper { get; set; }
        [JsonProperty("User")]
        public string User { get; set; }
    }
    public class SwitchModel
    {
        public List<RoleSwitchModel> roles { get; set; }
        public bool success { get; set; }

    }
}