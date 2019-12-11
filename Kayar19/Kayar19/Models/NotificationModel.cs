using System;
using System.Collections.Generic;
using System.Text;

namespace Kayar19.Models
{
  public class Notifications
    {
        public string recipient { get; set; }
        public string sender { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
        public string link_type { get; set; }
        public string link_type_id { get; set; }
        public string is_read { get; set; }
        public string read_by { get; set; }
        public string id { get; set; }
        public string date_read { get; set; }
        public string date_sent { get; set; }
    }
    public class MessageModels
    {
        public List<Notifications> notifications { get; set; }
        public bool success { get; set; }

    }
    public class UnreadCounter
    {
        public string NumberOfNotifications { get; set; }
    }
    public class MessageCountModels
    {
        public List<UnreadCounter> count { get; set; }
        public bool success { get; set; }

    }
}
