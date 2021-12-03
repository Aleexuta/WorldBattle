using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.Net.Sockets;

namespace WorldBattle
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private NetworkStream stream;
        public String version = "1.0";
        public MainWindow()
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

        private void HostButton_Click(object sender, RoutedEventArgs e)
        {
            TcpListener server = null;
            try
            {
                Int32 port = Convert.ToInt32(portTextBox.Text);
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

                server = new TcpListener(localAddr, port);
                server.Start();

                infoLabel.Content = "Waiting for connection...";

                TcpClient client = server.AcceptTcpClient();
                infoLabel.Content = "Connected!";

                // Get a stream object for reading and writing
                this.stream = client.GetStream();

                String data = ReadMessage();

                if (data == this.version)
                {
                    WriteMessage("Connected");

                    this.Hide();
                    GameUI game = new GameUI(stream, "First");
                    game.ShowDialog();
                    
                    SendDisconnectMessage();
                }
                else
                {
                    WriteMessage("Invalid version");
                }

                this.Show();
            }
            catch (SocketException err)
            {
                infoLabel.Content = "SocketException: " + err;
            }
            finally
            {
                // Stop listening for new clients
                server.Stop();
            }
        }

        private void guestButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Create a TcpClient
                string ip = ipTextBox.Text;
                if(ip == "127.0.0.1")
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
                Int32 port = Convert.ToInt32(portTextBox.Text);
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


        
    }
}
