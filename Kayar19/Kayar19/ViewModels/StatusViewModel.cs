using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kayar19.ViewModels
{
    public class StatusViewModel
    {
        public List<Status> StatusList { get; set; }

        public StatusViewModel()
        {
            StatusList = GetStatus().OrderBy(t => t.Value).ToList();
        }
        public List<Status> GetStatus()
        {
            var statuses = new List<Status>()
        {
            new Status(){key = 1, Value = "Good"},
            new Status(){key = 2, Value = "Serviceable"},
            new Status(){key = 3, Value = "Bad"}

        };
            return statuses;
        }

    }
    public class Status
    {
        public int key { get; set; }
        public string Value { get; set; }
    }
}
