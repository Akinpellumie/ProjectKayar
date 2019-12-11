using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kayar19.Models
{
    public class AssetCount
    {
        [JsonProperty("NumberOfAssets")]
        public string NumberOfAssets { get; set; }
    }
    public class AssetCounter
    {
            public List<AssetCount> data { get; set; }
            public bool success { get; set; }
    }
}
