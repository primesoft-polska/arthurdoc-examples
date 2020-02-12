using System.Windows;
using System.Windows.Controls;

namespace ArthurDocConnector.Loaders
{
    /// <summary>
    /// Interaction logic for BusyIndicator.xaml
    /// </summary>
    public partial class BusyIndicator : UserControl
    {
        public BusyIndicator()
        {
            InitializeComponent();
        }

        public string CustomMessage
        {
            get => (string)GetValue(MessageProperty);
            set => SetValue(MessageProperty, value);
        }

        public static DependencyProperty MessageProperty =
            DependencyProperty.Register("CustomMessage",
                typeof(string),
                typeof(BusyIndicator),
                new PropertyMetadata("Wczytywanie"));
    }
}
