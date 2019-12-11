﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Kayar19.Models
{
   public class User
    {
        public int Id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }

        public User () { }
        public User(string username, string password)
        {
            this.username = username;
            this.password = password;
        }
        public bool CheckInformation()
        {
            if (!this.username.Equals("") && !this.password.Equals(""))
                return true;
            else
                return false;
        }
    }
}
