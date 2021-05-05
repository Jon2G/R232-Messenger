using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kit.Model;

namespace RS232.Models
{
    public class Mensaje:ModelBase
    {
        public TimeSpan Time { get; set; }
        public string Texto { get; set; }
        public bool Entrante { get; set; }

        public Mensaje()
        {
          

        }
    }
}
