using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kayar19.Models
{
        public class Asset
        {
            [JsonProperty("item_id")]
            public string item_id { get; set; }

            [JsonProperty("item_Name")]
            public string item_Name { get; set; }
            [JsonProperty("Status")]
            public string Status { get; set; }
        
        public string itemId
        {
            get
            {
                return item_id;
            }
        }
        public string quantity
        {
            get
            {
                return Quantity;
            }
        }

        public List<RequestModel> items { get; set; }
        public string staffUsername { get; set; }
        public string comment { get; set; }

        [JsonProperty("item_Desc")]
        public string item_Desc { get; set; }

            [JsonProperty("Quantity")]
            public string Quantity { get; set; }
        public string event_id
        {
            get
            {
                return itemId;
            }
        }

        public string assign_quantity
        {
            get
            {
                return quantity;
            }
        }

    }
    public class GetAssetsModel
    {
        public List<Asset> items { get; set; }
        public string quantity { get; set; }
        public string NumberOfStaffAsset { get; set; }
        public List<LotAsset> lot { get; set; }
        public bool success { get; set; }

    }
    public class LotAsset
    {
        [JsonProperty("id")]
        public string id { get; set; }
        public string event_id
        {
            get
            {
                return id;
            }
        }
        public string itemId
        {
            get
            {
                return item_id;
            }
        }

        public string assign_quantity
        {
            get
            {
                return quantity;
            }
        }
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
    public class RequestModel
    {
        public string itemId { get; set; }
        public string quantity { get; set; }
        public string comment { get; set; }
        public string staffUsername { get; set; }
        public List<Asset> items { get; set; }
    }
    public class AssignModel
    {
        public string location { get; set; }
        public string requestStatus { get; set; }
        public string staff_username { get; set; }
        public string storeKeeperUsername { get; set; }
        public List<Asset> assets { get; set; }
    }
    //public class RequestAssetModel
    //{
    //    public string staffUsername { get; set; }
    //    public string comment { get; set; }
    //    public List<RequestModel> items { get; set; }
    //}
}
