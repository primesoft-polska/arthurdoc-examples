using System.ComponentModel.Composition;
using System.Windows;
using ArthurDocConnector.ViewModels;
using Ninject;

namespace ArthurDocConnector.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    [Export]
    public partial class MainWindow : Window
    {

        [Inject]
        private MainWindowViewModel MainWindowViewModel
        {
            set => DataContext = value;
        }

        public MainWindow()
        {
            InitializeComponent();
        }
    }
}