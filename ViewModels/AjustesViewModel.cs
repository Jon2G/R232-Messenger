using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kit.Model;

namespace RS232.ViewModels
{
    public class AjustesViewModel : ModelBase
    {
        public SerialPort SerialPort { get; set; }
        public int TimeOut
        {
            get => SerialPort.ReadTimeout;
            set
            {
                SerialPort.ReadTimeout = value;
                SerialPort.WriteTimeout = value;
            }
        }
        public List<string> Puertos { get; set; }

        public AjustesViewModel()
        {
            SerialPort = new SerialPort("COM1")
            {
                BaudRate = 9600,
                StopBits = System.IO.Ports.StopBits.One,
                RtsEnable = true,
                DataBits = 8,
                ReadTimeout = 1500,
                Encoding=Encoding.UTF32
            };
            this.Puertos = new List<string>(SerialPort.GetPortNames());

        }
    }
}
