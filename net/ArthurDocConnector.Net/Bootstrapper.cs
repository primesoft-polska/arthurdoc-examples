using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ArthurDocConnector.ViewModels;
using ArthurDocConnector.Views;
using Prism.Ninject;

namespace ArthurDocConnector
{
    public class Bootstrapper: NinjectBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return (MainWindow) Kernel.GetService(typeof(MainWindow));
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();
            Application.Current.MainWindow = (MainWindow) Shell;
            if (Application.Current.MainWindow != null) Application.Current.MainWindow.Show();
        }

        protected override void ConfigureKernel()
        {
            base.ConfigureKernel();
            Kernel.Settings.InjectNonPublic = true;
            AddBindings();
        }

        private void AddBindings()
        {
            Kernel.Bind<MainWindow>().ToSelf();
            Kernel.Bind<MainWindowViewModel>().ToSelf();
        }
    }
}
