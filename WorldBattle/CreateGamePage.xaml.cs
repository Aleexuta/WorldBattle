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
    /// Interaction logic for CreateGamePage.xaml
    /// </summary>
    public partial class CreateGamePage : Window
    {
        private NetworkStream stream;
        public String version = "1.0";
        private string gamePort = "3000";
        public CreateGamePage()
        {
            InitializeComponent();
            terrainComboBox.Items.Insert(0, "--Select--");
            terrainComboBox.Items.Add("Naval");
            terrainComboBox.Items.Add("Terestru");
            terrainComboBox.Items.Add("Aviatie");
            terrainComboBox.SelectedIndex = 0;
            
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

        private void CreateGameButton_Click(object sender, RoutedEventArgs e)
        {
            //NE ASIGURAM CA ESTE DAT UN USERNAME SI UN TIP DE TEREN

            var username = usernameTextBox.Text;
            var tipTeren = terrainComboBox.SelectedItem;

            if (username == "" || tipTeren == "--Select--")
            {
                MessageBox.Show("Please complete all the fields! ");
                return;
            }

            TcpListener server = null;
            try
            {
                Int32 port = Convert.ToInt32(gamePort);
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

                MessageBox.Show("This is the game ip: " + localAddr + "\n Press ok to start the server!");
                MessageBox.Show("Waiting for opponent!");
                
                server = new TcpListener(localAddr, port);
                server.Start();
  

                TcpClient client = server.AcceptTcpClient();
                infoLabel.Content = "Connected!";

                // Get a stream object for reading and writing
                this.stream = client.GetStream();

                String data = ReadMessage();

                if (data == this.version)
                {
                    WriteMessage("Connected," + tipTeren.ToString());

                    this.Hide();
                    GameUI game = new GameUI(stream, "First", username, tipTeren.ToString());
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Startup startup = new Startup();
            startup.Show();
            this.Close();
        }
    }
}

