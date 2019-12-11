using Kayar19.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kayar19.Models
{
    public class Role
    {
        [JsonProperty("Admin")]
        public string Admin { get; set; }

        [JsonProperty("StoreKeeper")]
        public string StoreKeeper { get; set; }

        [JsonProperty("User")]
        public string User { get; set; }
    }
    public class NewUser
    {
        public string fullname { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }

        public string email { get; set; }
        public string username { get; set; }
        public List<Roles> roles { get; set; }

        public string phonenumber { get; set; }
    }
    public class AddNewUserModel
    {
        public string fullname
        {
            get
            {
                return firstname + "  " + lastname;
            }
        }
        public string email { get; set; }

        [JsonProperty("firstname")]
        public string firstname { get; set; }

        [JsonProperty("lastname")]
        public string lastname { get; set; }

        [JsonProperty("phonenumber")]
        public string phonenumber { get; set; }

        [JsonProperty("Image")]
        public string Image { get; set; }

        [JsonProperty("roles")]
        public List<string> roles { get; set; }

        [JsonProperty("staffId")]
        public string staffId { get; set; }

        [JsonProperty("username")]
        public string username { get; set; }
        public AddNewUserModel() { }
        public AddNewUserModel(string username, string firstname, string email, string lastname, string phonenumber)
        {
            this.username = username;
            this.firstname = firstname;
            this.lastname = lastname;
            this.phonenumber = phonenumber;
            this.email = email;

        }
        public bool CheckInformation()
        {
            if (!this.username.Equals("") && !this.firstname.Equals("") && !this.lastname.Equals("") && !this.email.Equals("") && !this.phonenumber.Equals(""))
                return true;
            else
                return false;
        }
    }
}
