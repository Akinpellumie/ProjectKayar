using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kayar19.ViewModels
{
    public class RolesViewModel
    {
        public List<Roles> RoleList { get; set; }

        public RolesViewModel()
        {
            RoleList = GetRoles().OrderBy(t => t.Value).ToList();
        }
        public List<Roles> GetRoles()
        {
            var roles = new List<Roles>()
        {
            new Roles(){key = 1, Value = "Admin"},
            new Roles(){key = 2, Value = "StoreKeeper"},
            new Roles(){key = 3, Value = "User"}

        };
            return roles;
        }

    }
    public class Roles
    {
        public int key { get; set; }
        public string Value { get; set; }
    }
}
