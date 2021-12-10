using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WorldBattle
{
    /// <summary>
    /// Interaction logic for Startup.xaml
    /// </summary>
    public partial class Startup : Window
    {
       
        public Startup()
        {
            InitializeComponent();
            string iconUri = "..\\..\\Poze\\logo.jpeg";
            this.Icon = BitmapFrame.Create(new Uri(iconUri, UriKind.Relative));
        }

        private void hostButton_Click(object sender, RoutedEventArgs e)
        {
            CreateGamePage cgp = new CreateGamePage();
            cgp.Show();
            this.Close();
        }

        private void guestButton_Click(object sender, RoutedEventArgs e)
        {
            GuestPage gp = new GuestPage();
            gp.Show();
            this.Close();
        }
    }
}
