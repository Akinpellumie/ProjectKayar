using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kayar19.Models
{
    public class UserAssignedModel
    {
            [JsonProperty("id")]
            public int id { get; set; }

            [JsonProperty("type")]
            public string type { get; set; }

            [JsonProperty("requestId")]
            public int requestId { get; set; }

            [JsonProperty("is_leaf")]
            public string is_leaf { get; set; }

            [JsonProperty("location")]
            public string location { get; set; }

            [JsonProperty("received_by")]
            public string receieved_by { get; set; }

            [JsonProperty("brought_by")]
            public string brought_by { get; set; }

            [JsonProperty("assigned_to")]
            public string assigned_to { get; set; }

            [JsonProperty("is_Assigned")]
            public string is_Assigned { get; set; }

            [JsonProperty("parent_id")]
            public string parent_id { get; set; }

            [JsonProperty("Status")]
            public string Status { get; set; }

            [JsonProperty("Category")]
            public string Category { get; set; }

            [JsonProperty("subDescription")]
            public string subDescription { get; set; }
            [JsonProperty("Comment")]
            public string Comment { get; set; }

            [JsonProperty("Date_created")]
            public DateTime Date_created { get; set; }

            [JsonProperty("item_id")]
            public string item_id { get; set; }

            [JsonProperty("Item_Id")]
            public string Item_Id { get; set; }

            [JsonProperty("item_Name")]
            public string item_Name { get; set; }

            [JsonProperty("item_Desc")]
            public string item_Desc { get; set; }

            [JsonProperty("quantity")]
            public string quantity { get; set; }
            public List<string> serialNumbers { get; set; }

        }
    public class UserAssignedAsset
    {
        public List<UserAssignedModel> items { get; set; }
        public string NumberOfStaffAsset { get; set; }
        public bool success { get; set; }
    }
    }
