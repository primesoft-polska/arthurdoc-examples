using System.ComponentModel.Composition;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using ArthurDocConnector.ViewModels;
using ArthurDoc.Client.Models;
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

        private void DataGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGrid grid = sender as DataGrid;

                if (grid != null && grid.SelectedItems != null && grid.SelectedItems.Count == 1)
                {
                    DataGridRow dgr = grid.ItemContainerGenerator.ContainerFromItem(grid.SelectedItem) as DataGridRow;
                    DownloadedFile file = (DownloadedFile)dgr.Item;

                    System.Diagnostics.Process.Start(file.Path);
                }
            }
        }
    }
}