using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for GameUI.xaml
    /// </summary>
    /// 


    public partial class GameUI : Window
    {

        private NetworkStream stream;
        private Game game;
        private System.ComponentModel.IContainer components = null;
        private Button[] mytableButtons = new Button[64];
        private Button[] opponetsButtons = new Button[64];

        public GameUI(NetworkStream stream, String player)
        {
            InitializeComponent();
            GenerateButtonArray();
            this.stream = stream;
            this.game = new Game(player);
            if(player=="First")
            {
                this.subtitle.Text = "You are the host.\nYour opponent goes first";
            }
            else
            {
                this.subtitle.Text = "You are the guest. \nIt's your turn first";
            }
            UpdateBoard();
            if(!game.isTurn())
            {
                WaitForResponse();
            }

        }
        #region Windows Form Designer generated code
        private String ReadMessage()
        {
            Byte[] bytes = new Byte[256];
            String data = null;
            Int32 i = stream.Read(bytes, 0, bytes.Length);
            data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
            return data;
        }

        //daca apasam pe tabla adversarului
        private void OnOpponentClick(object sender, EventArgs e, int button)
        {
            if (game.isOver() == false)
            {
                if (game.isTurn() == true)
                {
                    //e randul meu
                }
            }
        }
        private void OnOurTableClick(object sender, EventArgs e, int button)
        {
            if(game.isOver()==false)
            {
                if(game.isInPrepareMode()==true)
                {
                    //prepar tabla mea
                }
            }
        }
        private async void WaitForResponse()
        {
            try
            {
                Byte[] bytes = new Byte[256];
                String data = null;
                Int32 i = await stream.ReadAsync(bytes, 0, bytes.Length);
                data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                UpdateGame(data);
            }
            catch (ObjectDisposedException err)
            {
               // InfoLabel.Text = "Opponent disconnected. You won!";
                game.EndGame();
                stream.Close();
            }
        }
        private void UpdateBoard()
        { 
            //updatam interfata in functie de cele doua table de joc
        }


        public void UpdateGame(String data)
        {
            //updateaza jocul(tablele) in functie de mesajul primit
            String[] dataString = data.Split(',');


        }

        private void WriteMessage(String data)
        {
            Byte[] bytes = new Byte[256];
            bytes = System.Text.Encoding.ASCII.GetBytes(data);
            stream.Write(bytes, 0, bytes.Length);
        }









        private void GenerateButtonArray()
        {
            this.opponetsButtons[0] = Button0;
            this.opponetsButtons[1] = Button1;
            this.opponetsButtons[2] = Button2;
            this.opponetsButtons[3] = Button3;
            this.opponetsButtons[4] = Button4;
            this.opponetsButtons[5] = Button5;
            this.opponetsButtons[6] = Button6;
            this.opponetsButtons[7] = Button7;

            //fa si pt restul butoanelor din tabla oponentului
            //si la tabla noatra??
        }

        #endregion

        private void Button0_Click(object sender, RoutedEventArgs e)
        {
            OnOpponentClick(sender, e, 0);
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            OnOpponentClick(sender, e, 1);
        }
        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            OnOpponentClick(sender, e, 2);
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            OnOpponentClick(sender, e, 3);
        }

        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            OnOpponentClick(sender, e, 4);
        }

        private void Button5_Click(object sender, RoutedEventArgs e)
        {
            OnOpponentClick(sender, e, 5);
        }

        private void Button6_Click(object sender, RoutedEventArgs e)
        {
            OnOpponentClick(sender, e, 6);
        }

        private void Button7_Click(object sender, RoutedEventArgs e)
        {
            OnOpponentClick(sender, e, 7);
        }
    }
}
