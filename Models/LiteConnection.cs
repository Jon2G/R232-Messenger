using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kit.Sql.Sqlite;

namespace RS232.Models
{
    public class LiteConnection
    {
        public static SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(new FileInfo(Path.Combine(Kit.Tools.Instance.LibraryPath,"Mensajes.db")),100);
        }
    }
}
