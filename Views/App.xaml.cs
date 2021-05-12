using System.Windows;
using RS232.Models;

namespace RS232.Views
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Kit.WPF.Tools.Init();
            LiteConnection.GetConnection().CheckTables(typeof(Mensaje));
        }
    }
}
