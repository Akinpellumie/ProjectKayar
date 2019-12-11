using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kayar19.Models
{
    public class AssignedAssetCount
    {
        [JsonProperty("NumberOfAssignedAssets")]
        public string NumberOfAssignedAssets { get; set; }
    }
    public class AssignedCounter
    {
        public List<AssignedAssetCount> data { get; set; }
        public bool success { get; set; }

    }
}
