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

        public GameUI(NetworkStream stream, String color)
        {
            InitializeComponent();
            GenerateButtonArray();


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

        private void GenerateButtonArray()
        {
            this.opponetsButtons[0] = Button0;
            this.opponetsButtons[1] = Button1;
            //this.opponetsButtons[2] = Button2;
            //this.opponetsButtons[3] = Button3;
            //this.opponetsButtons[4] = Button4;
            //this.opponetsButtons[5] = Button5;
            //this.opponetsButtons[6] = Button6;
            //this.opponetsButtons[7] = Button7;

            //fa si pt restul butoanelor din tabla oponentului
            //si la tabla noatra??
        }
        #endregion
    }
}
