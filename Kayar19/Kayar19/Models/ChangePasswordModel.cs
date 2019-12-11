using System;
using System.Collections.Generic;
using System.Text;

namespace Kayar19.Models
{
   public class ChangePasswordModel
    {
        public string username { get; set; }
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
    }
}
