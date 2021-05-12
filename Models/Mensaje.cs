using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kit.Model;
using Kit.Sql.Attributes;

namespace RS232.Models
{
    public class Mensaje:ModelBase
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public TimeSpan Time { get; set; }
        public string Texto { get; set; }
        public bool Entrante { get; set; }

        public Mensaje()
        {
          

        }
        public void Save()
        {
            LiteConnection.GetConnection().Insert(this);
        }
    }
}
