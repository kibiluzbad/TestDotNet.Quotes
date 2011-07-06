using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ClientAPI;
using ClientLibrary;

namespace TestDotNet.Quotes.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string UserName = "trademaker01";
        private const string Password = "t01m975kt";
        private readonly ClientConnection _connection;
        
        public MainWindow()
        {
            InitializeComponent();
            _connection = new ClientConnection();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            DisconnectAndLogout();
            ConnectAndLogin();
            LoadQuote();
            StartStreaming();
        }

        private void quotes_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            var quote = (Quote)e.Row.Item;
            switch (quote.Direction)
            {
                case "+":
                    ChangeRowColor(e,
                        Color.FromRgb(0, 255, 0),
                        Color.FromRgb(255, 255, 255));
                    break;
                case "-":
                    ChangeRowColor(e,
                        Color.FromRgb(255, 0, 0),
                        Color.FromRgb(255, 255, 255));
                    break;
                case "=":
                    ChangeRowColor(e,
                        Color.FromRgb(255, 255, 255),
                        Color.FromRgb(0, 0, 0));
                    break;
            }

        }

        void connection_OnQuoteStreaming(Quote quote)
        {
            quotes.ItemsSource = new[] { quote };
        }
        
        private void StartStreaming()
        {
            _connection.OnQuoteStreaming += connection_OnQuoteStreaming;
            _connection.StartQuoteStreaming(objectName.Text);
        }



        private void LoadQuote()
        {
            var quote = new Quote();
            _connection.GetQuote(objectName.Text, out quote);
            if (null != quote)
                quotes.ItemsSource = new[] { quote };
            else
            {
                MessageBox.Show(_connection.ErrorMessage,"Error",MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DisconnectAndLogout()
        {
            _connection.Logout();
            _connection.Disconnect();
        }

        private void ConnectAndLogin()
        {
            _connection.Connect(host.Text, ushort.Parse(port.Text));
            _connection.Login(UserName, Password);
        }

        private static void ChangeRowColor(DataGridRowEventArgs e,
            Color backgroundColor,
            Color foregroundColor)
        {
            e.Row.Background = new SolidColorBrush(backgroundColor);
            e.Row.Foreground = new SolidColorBrush(foregroundColor);
        }

        ~MainWindow()
        {
            DisconnectAndLogout();
        }
    }
}
