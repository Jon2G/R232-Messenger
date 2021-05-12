using System;
using System.Collections.ObjectModel;
using System.IO.Ports;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Threading;
using Kit;
using Kit.Extensions;
using Kit.Model;
using RS232.Models;
using RS232.Views;

namespace RS232.ViewModels
{
    public class ComunicacionViewModel : ModelBase
    {
        public ICommand AjustesCommand { get; }
        public ICommand EnviarCommand { get; }
        public ObservableCollection<Mensaje> Mensajes { get; set; }
        public AjustesViewModel Ajustes { get; set; }
        public SerialPort Puerto { get => Ajustes.SerialPort; }
        private string _Texto;

        public string Texto
        {
            get => _Texto;
            set
            {
                _Texto = value;
                Raise(() => Texto);
            }
        }
        public ComunicacionViewModel()
        {
            this.AjustesCommand = new Command(AbrirAjustes);
            this.EnviarCommand = new Command(Enviar);
            this.Ajustes = new AjustesViewModel();
            this.Puerto.DataReceived += DataReceived;
            this.Mensajes = new ObservableCollection<Mensaje>();
            CargarMensajes();
        }

        private void CargarMensajes()
        {
            this.Mensajes.AddRange(LiteConnection.GetConnection().Table<Mensaje>().ToList());
        }

        private void DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Recibir();
        }

        private async void Recibir()
        {
            try
            {
                if (!Puerto.IsOpen)
                {
                    return;
                }
                await Task.Delay(500);          
                StringBuilder Textmessage = new StringBuilder(Puerto.ReadExisting());
                if (Textmessage.Length > 0)
                {
                    Puerto.DiscardInBuffer();
                    var mensaje = new Mensaje()
                    {
                        Texto = Textmessage.ToString(),
                        Entrante = true,
                        Time = DateTime.Now.TimeOfDay
                    };
                    mensaje.Save();
                    Action<Mensaje> addMethod = this.Mensajes.Add;
                    await App.Current.Dispatcher.BeginInvoke(addMethod, mensaje);
                }

            }
            catch (System.Exception ex)
            {
                Log.Logger.Error("Al recibir mensaje del puerto",ex);
            }
        }

        private void Enviar()
        {
            try
            {
                Puerto.WriteLine(Texto);

                var mensaje = new Mensaje() {Texto = Texto, Entrante = false, Time = DateTime.Now.TimeOfDay};
                this.Mensajes.Add(mensaje);
                mensaje.Save();
                Texto = string.Empty;
            }
            catch (System.Exception ex)
            {
                Texto = ex.Message;
            }
        }
        public void Cerrar()
        { Puerto.Close(); }
        private void AbrirAjustes()
        {
            Puerto.Close();
            Views.Ajustes ajustes = new Ajustes { Owner = App.Current.MainWindow, DataContext = Ajustes };
            ajustes.ShowDialog();
            Puerto.Open();
            this.Puerto.DataReceived += DataReceived;
        }
    }
}
