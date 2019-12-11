using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kayar19.Data
{
   public  interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}
