using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ArthurDocConnector
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            InistializeApp();

        }

        private void InistializeApp()
        {
            var bootstrapper = new Bootstrapper();
            try
            {
                bootstrapper.Run();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                Current.Shutdown();
            }
        }
    }
}
