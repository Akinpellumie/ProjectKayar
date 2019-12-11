using System;
using System.Collections.Generic;
using System.Text;

namespace Kayar19.Models
{
    public class AssetModel
    {
        public string itemName { get; set; }
        public string itemDescription { get; set; }
        public string quantity { get; set; }
        public string location { get; set; }
        public string receivedBy { get; set; }
        public string broughtBy { get; set; }
        public string status { get; set; }
        public List<string> category { get; set; }
        public List<string> serialNumber { get; set; }
        public string csvDoc { get; set; }
        public string comment { get; set; }

        //public AssetModel(string category, string itemName, string itemDesc, string location, string quantity, string receivedBy, string broughtBy, string status, string comment)
        //{
        //    this.comment = comment;
        //    this.itemName = itemName;
        //    this.itemDescription = itemDesc;
        //    this.category = category;
        //    this.receivedBy = receivedBy;
        //    this.status = status;
        //    this.quantity = quantity;
        //    this.location = location;
        //    this.broughtBy = broughtBy;
        //}

        //public AssetModel(string text1, string text2, string text3, string text4, string text5, string text6, string text7, string text8)
        //{
        //    this.text1 = text1;
        //    this.text2 = text2;
        //    this.text3 = text3;
        //    this.text4 = text4;
        //    this.text5 = text5;
        //    this.text6 = text6;
        //    this.text7 = text7;
        //    this.text8 = text8;
        //}

        public bool CheckInformation()
        {
            if (!this.comment.Equals("") && !this.itemDescription.Equals("") && !this.itemName.Equals("") && !this.status.Equals("") && !this.location.Equals("") && !this.receivedBy.Equals("") && !this.broughtBy.Equals("") && !this.category.Equals("") && !this.quantity.Equals(""))
                return true;
            else
                return false;
        }
       }
}