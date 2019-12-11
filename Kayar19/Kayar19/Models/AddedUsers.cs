using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Kayar19.Models
{
    public class status
    {
        public bool success { get; set; }
    }
    public class Data
    {
        [JsonProperty("FirstName")]
        public string FirstName { get; set; }

        [JsonProperty("LastName")]
        public string LastName { get; set; }

        [JsonProperty("UserName")]
        public string UserName { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("Image")]
         public string Image { get; set; }

        [JsonProperty("Staff_Id")]
        public string Staff_Id { get; set; }

        [JsonProperty("Date_Created")]
        public string Date_Created { get; set; }
        public List<string> Roles { get; set; }

        [JsonProperty("PhoneNumber")]
        public string PhoneNumber { get; set; }
        public string FullName
        {
            get
            {
                return this.FirstName + "  " + this.LastName;
            }
        }
        public string capName
        {
            get
            {
                return this.FullName.ToUpper();
            }
        }
        public ImageSource UserImage
        {

            get
            {

                Uri source = new Uri(Helper.Imageurl + Image);

                return source;
            }
            set { UserImage = value; }
        }
    }
    public class AddedUsers
    {
        public List<Data> data { get; set; }
        public bool  success { get; set; }
        public string UserName { get; internal set; }
    }
   
}
