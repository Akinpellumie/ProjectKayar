using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kayar19.Models
{
    public class Roles
    {
        [JsonProperty("Admin")]
        public string Admin { get; set; }

        [JsonProperty("Storekeeper")]
        public string StoreKeeper { get; set; }

        [JsonProperty("User")]
        public string User { get; set; }
    }
        public class LoginProfileModel
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        public List<string> roleId { get; set; }
        public string token { get; set; }
        public string Staff_Id { get; set; }

        public string username { get; set; }

        public string name
        {
            get
            {
                return this.firstname + "  " + this.lastname;
            }
        }

        public string capitalizedname
        {
            get
            {
                return this.name.ToUpper();
            }
        }
    }

    public class ProfileModel
    {
        public string email { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }

        public string phonenumber { get; set; }
        public string roleId { get; set; }

       
        public string username { get; set; }

        public string name
        {
            get
            {
                return this.firstname + "  " + this.lastname;
            }
        }

        public string capitalizedName
        {
            get
            {
                return this.name.ToUpper();
            }
        }

    }
}
