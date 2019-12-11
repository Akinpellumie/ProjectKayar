using System;
using System.Collections.Generic;
using System.Text;

namespace Kayar19.Models
{
    public class Token
    {
        public static int Id { get; set; }
        public static string access_token { get; set; }
        public static string error_description { get; set; }
        public static DateTime expire_date { get; set; }
        public static int expire_in { get; set; }

        public Token()  { }
    }
}
