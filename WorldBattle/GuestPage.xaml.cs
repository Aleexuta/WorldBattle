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
    /// Interaction logic for GuestPage.xaml
    /// </summary>
    public partial class GuestPage : Window
    {
        private NetworkStream stream;
        public String version = "1.0";
        private string gamePort = "3000";
        public GuestPage()
        {
            InitializeComponent();
        }
        /*
        * Reads message sent through the network stream
        * and returns the data as a String
        */
        private String ReadMessage()
        {
            Byte[] bytes = new Byte[256];
            String data = null;
            Int32 i = stream.Read(bytes, 0, bytes.Length);
            data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
            return data;
        }

        /*
        * Takes a string and sends the message
        * through the network stream
        */
        private void WriteMessage(String data)
        {

            Byte[] bytes = new Byte[256];
            bytes = System.Text.Encoding.ASCII.GetBytes(data);
            stream.Write(bytes, 0, bytes.Length);
        }
        /*
        * Sends a message through the network stream to notify the
        * other player when they disconnect
        */
        private void SendDisconnectMessage()
        {
            if (stream.CanRead)
            {
                WriteMessage("Disconnected");
                stream.Close();
            }
        }

        private void JoinGameButton_Click(object sender, RoutedEventArgs e)
        {

            //var username = usernameTextBox.Text;
            string ip = ipTextBox.Text;

            //if (username == "" || ip == "")
            //{
            //    MessageBox.Show("Please fill all the gaps!");
            //    return;
            //}

            try
            {
                // Create a TcpClient
                if (ip == "127.0.0.1")
                {
                    string MyIP = "";
                    IPHostEntry Host = default(IPHostEntry);
                    string Hostname = null;
                    Hostname = System.Environment.MachineName;
                    Host = Dns.GetHostEntry(Hostname);
                    foreach (IPAddress IP in Host.AddressList)
                    {
                        if (IP.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            MyIP = Convert.ToString(IP);
                        }
                    }
                    IPAddress localAddr = IPAddress.Parse(MyIP);
                    ip = localAddr.ToString();
                }
                Int32 port = Convert.ToInt32(gamePort);
                TcpClient client = new TcpClient(ip, port);

                infoLabel.Content = "Connecting to host...";

                // Get a client stream for reading and writing.
                this.stream = client.GetStream();

                WriteMessage("1.0");

                String data = ReadMessage();

                if (data == "Connected")
                {
                    infoLabel.Content = "Connected!";
                    this.Hide();
                    GameUI game = new GameUI(stream, "Second");
                    game.ShowDialog();
                }
                else
                {
                    if (data == "Invalid version")
                    {
                        infoLabel.Content = "Wrong version!";
                    }
                    else
                    {
                        infoLabel.Content = data;
                    }
                    stream.Close();
                }

                this.Show();

                SendDisconnectMessage();

                // Close everything
                client.Close();
            }
            catch (ArgumentNullException err)
            {
                infoLabel.Content = "ArgumentNullException: " + err;
            }
            catch (SocketException err)
            {
                infoLabel.Content = "Could not connect to host";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Startup startup = new Startup();
            startup.Show();
            this.Close();
        }
    }
}
