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
            //TODO
            //mai intai face butoanele tuturor
            //seteaza butoanele adversarului pe disable
            GenerateOpponentButtonArray();
            this.stream = stream;
            this.game = new Game(player);
            //TODO
            //seteaza subtitlul pe prepare si abia dupa ce e gata prepararea se seteaza randul
            //if (player == "First")
            //{
            //    this.subtitle.Text = "You are the host.\nYour opponent goes first";
            //}
            //else
            //{
            //    this.subtitle.Text = "You are the guest. \nIt's your turn first";
            //}
            //UpdateBoard();
            //if (!game.isTurn())
            //{
            //    WaitForResponse();
            //}

        }
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
            //TODO?->INTREABA
            //updatetable doar pt pozitia care sa schimbat, nu trebe sa modifici toata tabla
            //pozitia unde am dat click si se aseaza un jeton ramane asa pana la finalul meciului   

        }


        public void UpdateGame(String data)
        {
            //primeste mesaj de ready

            //primeste mesaj de am terminat tura

            //updateaza jocul(tablele) in functie de mesajul primit
            //primeste o pozitie de la oponent si verifica daca este ceva acolo sau nu
            //transmite raspunsul catre oponent
            //plaseaza pe tabla noastra ca a fost atinsa si raspunsul

            //primeste raspunsul de la pozitia incercata si o pune pe tabla de joc a oponentului


            String[] dataString = data.Split(',');
            if(dataString[0]=="Ready")
            {
                game.setOpponentReady(true);
                if(game.isInPrepareMode()==false)//daca eu sunt gata de joc si e si adversarul
                {
                    if(game.isTurn()==true)//daca e randul meu
                    {
                        //TODO
                        //functie de gata cu prepararea
                        //o sa dea disable la toate butoanele de pe tabla mea si o afiseaza pe a adversarului
                    }
                }

                //oponentul este gata, cand sunt amandoi gata jocul poate sa inceapa
            }
            else if(dataString[0]=="Select") //am apasat pe ceva
            {
                int nrbut = Convert.ToInt32(dataString[1]);//nr butonului
                string newmessage = "Verified";
                //verific ce am pe pozitia aia si trimit un mesaj cu raspunsul si adaug pe tabla mea ca a fost nimerit
                if(game.getTypeFromTable(nrbut)==TypesBoard.UntestedEmpty)
                {
                    //transmite catre adversar ca e empty
                    newmessage +=","+dataString[1]+ ",Empty";
                    game.setTypeMyTable(nrbut, TypesBoard.TestedEmpty);
                }
                else if(game.getTypeFromTable(nrbut)==TypesBoard.UntestedFull)
                {
                    newmessage += "," + dataString[1] + ",Full";
                    game.setTypeMyTable(nrbut, TypesBoard.TestedFull);
                }
               
                WriteMessage(newmessage);
            }
            else if(dataString[0]=="Verified")//primesc felul de buton ce e
            {
                int nrbut = Convert.ToInt32(dataString[1]);
                if(dataString[2]=="Empty")
                {
                    game.setTypeYourTable(nrbut, TypesBoard.TestedEmpty);
                }
                else
                {
                    game.setTypeYourTable(nrbut, TypesBoard.TestedFull);
                }
                //TODO
                //seteaza butonul nrbut al adversarului pe disable
                
            }
        }

        private void WriteMessage(String data)
        {
            Byte[] bytes = new Byte[256];
            bytes = System.Text.Encoding.ASCII.GetBytes(data);
            stream.Write(bytes, 0, bytes.Length);
        }






        private void yourturnbutton_Click(object sender, RoutedEventArgs e)
        {
            //trimite mesaj de trimis 
            //seteaza turn to false
        }

        private void readybutton_Click(object sender, RoutedEventArgs e)
        {
            //trimitem mesaj catre adversar ca e ready
        }


        private void GenerateOpponentButtonArray()
        {
            this.opponetsButtons[0] = Button0;
            this.opponetsButtons[1] = Button1;
            this.opponetsButtons[2] = Button2;
            this.opponetsButtons[3] = Button3;
            this.opponetsButtons[4] = Button4;
            this.opponetsButtons[5] = Button5;
            this.opponetsButtons[6] = Button6;
            this.opponetsButtons[7] = Button7;

            this.opponetsButtons[8] = Button8;
            this.opponetsButtons[9] = Button9;
            this.opponetsButtons[10] = Button10;
            this.opponetsButtons[11] = Button11;
            this.opponetsButtons[12] = Button12;
            this.opponetsButtons[13] = Button13;
            this.opponetsButtons[14] = Button14;
            this.opponetsButtons[15] = Button15;

            this.opponetsButtons[16] = Button16;
            this.opponetsButtons[17] = Button17;
            this.opponetsButtons[18] = Button18;
            this.opponetsButtons[19] = Button19;
            this.opponetsButtons[20] = Button20;
            this.opponetsButtons[21] = Button21;
            this.opponetsButtons[22] = Button22;
            this.opponetsButtons[23] = Button23;

            this.opponetsButtons[24] = Button24;
            this.opponetsButtons[25] = Button25;
            this.opponetsButtons[26] = Button26;
            this.opponetsButtons[27] = Button27;
            this.opponetsButtons[28] = Button28;
            this.opponetsButtons[29] = Button29;
            this.opponetsButtons[30] = Button30;
            this.opponetsButtons[31] = Button31;

            this.opponetsButtons[32] = Button32;
            this.opponetsButtons[33] = Button33;
            this.opponetsButtons[34] = Button34;
            this.opponetsButtons[35] = Button35;
            this.opponetsButtons[36] = Button36;
            this.opponetsButtons[37] = Button37;
            this.opponetsButtons[38] = Button38;
            this.opponetsButtons[39] = Button39;

            this.opponetsButtons[40] = Button40;
            this.opponetsButtons[41] = Button41;
            this.opponetsButtons[42] = Button42;
            this.opponetsButtons[43] = Button43;
            this.opponetsButtons[44] = Button44;
            this.opponetsButtons[45] = Button45;
            this.opponetsButtons[46] = Button46;
            this.opponetsButtons[47] = Button47;

            this.opponetsButtons[48] = Button48;
            this.opponetsButtons[49] = Button49;
            this.opponetsButtons[50] = Button50;
            this.opponetsButtons[51] = Button51;
            this.opponetsButtons[52] = Button52;
            this.opponetsButtons[53] = Button53;
            this.opponetsButtons[54] = Button54;
            this.opponetsButtons[55] = Button55;

            this.opponetsButtons[56] = Button56;
            this.opponetsButtons[57] = Button57;
            this.opponetsButtons[58] = Button58;
            this.opponetsButtons[59] = Button59;
            this.opponetsButtons[60] = Button60;
            this.opponetsButtons[61] = Button61;
            this.opponetsButtons[62] = Button62;
            this.opponetsButtons[63] = Button63;
            //fa si pt restul butoanelor din tabla oponentului
            //si la tabla noatra??
        }

        private void Button0_Click_1(object sender, RoutedEventArgs e)
        {
            OnOpponentClick(sender, e,0);
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

        private void Button8_Click(object sender, RoutedEventArgs e)
        {
            OnOpponentClick(sender, e, 8);
        }

        private void Button9_Click(object sender, RoutedEventArgs e)
        {
            OnOpponentClick(sender, e, 9);
        }

        private void Button10_Click(object sender, RoutedEventArgs e)
        {
            OnOpponentClick(sender, e, 10);
        }

        private void Button11_Click(object sender, RoutedEventArgs e)
        {
            OnOpponentClick(sender, e, 11);
        }

        private void Button12_Click(object sender, RoutedEventArgs e)
        {
            OnOpponentClick(sender, e, 12);
        }

        private void Button13_Click(object sender, RoutedEventArgs e)
        {
            OnOpponentClick(sender, e, 13);
        }

        private void Button14_Click(object sender, RoutedEventArgs e)
        {
            OnOpponentClick(sender, e, 14);
        }

        private void Button15_Click(object sender, RoutedEventArgs e)
        {
            OnOpponentClick(sender, e, 15);
        }

        private void Button16_Click(object sender, RoutedEventArgs e)
        {
            OnOpponentClick(sender, e, 16);
        }

        private void Button17_Click(object sender, RoutedEventArgs e)
        {
            OnOpponentClick(sender, e, 17);
        }

        private void Button18_Click(object sender, RoutedEventArgs e)
        {
            OnOpponentClick(sender, e, 18);
        }

        private void Button19_Click(object sender, RoutedEventArgs e)
        {
            OnOpponentClick(sender, e, 19);
        }

        private void Button20_Click(object sender, RoutedEventArgs e)
        {
            OnOpponentClick(sender, e, 20);
        }

        private void Button21_Click(object sender, RoutedEventArgs e)
        {
            OnOpponentClick(sender, e, 21);
        }

        private void Button22_Click(object sender, RoutedEventArgs e)
        {
            OnOpponentClick(sender, e, 22);
        }

        private void Button23_Click(object sender, RoutedEventArgs e)
        {
            OnOpponentClick(sender, e, 23);
        }

        private void Button24_Click(object sender, RoutedEventArgs e)
        {
            OnOpponentClick(sender, e, 24);
        }

        private void Button25_Click(object sender, RoutedEventArgs e)
        {
            OnOpponentClick(sender, e, 25);
        }

        private void Button26_Click(object sender, RoutedEventArgs e)
        {
            OnOpponentClick(sender, e, 26);
        }

        private void Button27_Click(object sender, RoutedEventArgs e)
        {
            OnOpponentClick(sender, e, 27);
        }

        private void Button28_Click(object sender, RoutedEventArgs e)
        {
            OnOpponentClick(sender, e, 28);
        }

        private void Button29_Click(object sender, RoutedEventArgs e)
        {
            OnOpponentClick(sender, e, 29);
        }

        private void Button30_Click(object sender, RoutedEventArgs e)
        {
            OnOpponentClick(sender, e, 30);
        }

        private void Button31_Click(object sender, RoutedEventArgs e)
        {
            OnOpponentClick(sender, e, 31);
        }

        private void Button32_Click(object sender, RoutedEventArgs e)
        {
            OnOpponentClick(sender, e, 32);
        }

        private void Button33_Click(object sender, RoutedEventArgs e)
        {
            OnOpponentClick(sender, e,33);
        }

        private void Button34_Click(object sender, RoutedEventArgs e)
        {
            OnOpponentClick(sender, e, 34);
        }

        private void Button35_Click(object sender, RoutedEventArgs e)
        {
            OnOpponentClick(sender, e, 35);
        }

        private void Button36_Click(object sender, RoutedEventArgs e)
        {
            OnOpponentClick(sender, e, 36);
        }

        private void Button37_Click(object sender, RoutedEventArgs e)
        {
            OnOpponentClick(sender, e, 37);
        }

        private void Button38_Click(object sender, RoutedEventArgs e)
        {
            OnOpponentClick(sender, e, 38);
        }

        private void Button39_Click(object sender, RoutedEventArgs e)
        {
            OnOpponentClick(sender, e, 39);
        }

        private void Button40_Click(object sender, RoutedEventArgs e)
        {
            OnOpponentClick(sender, e, 40);
        }

        private void Button41_Click(object sender, RoutedEventArgs e)
        {
            OnOpponentClick(sender, e, 41);
        }

        private void Button42_Click(object sender, RoutedEventArgs e)
        {
            OnOpponentClick(sender, e,42);
        }

        private void Button43_Click(object sender, RoutedEventArgs e)
        {
            OnOpponentClick(sender, e,43);
        }

        private void Button44_Click(object sender, RoutedEventArgs e)
        {
            OnOpponentClick(sender, e, 44);
        }

        private void Button45_Click(object sender, RoutedEventArgs e)
        {
            OnOpponentClick(sender, e, 45);
        }

        private void Button46_Click(object sender, RoutedEventArgs e)
        {
            OnOpponentClick(sender, e, 46);
        }

        private void Button47_Click(object sender, RoutedEventArgs e)
        {
            OnOpponentClick(sender, e, 47);
        }

        private void Button48_Click(object sender, RoutedEventArgs e)
        {
            OnOpponentClick(sender, e, 48);
        }

        private void Button49_Click(object sender, RoutedEventArgs e)
        {
            OnOpponentClick(sender, e, 49);
        }

        private void Button50_Click(object sender, RoutedEventArgs e)
        {
            OnOpponentClick(sender, e, 50);
        }

        private void Button51_Click(object sender, RoutedEventArgs e)
        {
            OnOpponentClick(sender, e, 51);
        }

        private void Button52_Click(object sender, RoutedEventArgs e)
        {
            OnOpponentClick(sender, e, 52);
        }

        private void Button53_Click(object sender, RoutedEventArgs e)
        {
            OnOpponentClick(sender, e, 53);
        }

        private void Button54_Click(object sender, RoutedEventArgs e)
        {
            OnOpponentClick(sender, e, 54);
        }

        private void Button55_Click(object sender, RoutedEventArgs e)
        {
            OnOpponentClick(sender, e,55);
        }

        private void Button56_Click(object sender, RoutedEventArgs e)
        {
            OnOpponentClick(sender, e, 56);
        }

        private void Button57_Click(object sender, RoutedEventArgs e)
        {
            OnOpponentClick(sender, e, 57);
        }

        private void Button58_Click(object sender, RoutedEventArgs e)
        {
            OnOpponentClick(sender, e, 58);
        }

        private void Button59_Click(object sender, RoutedEventArgs e)
        {
            OnOpponentClick(sender, e, 59);
        }

        private void Button60_Click(object sender, RoutedEventArgs e)
        {
            OnOpponentClick(sender, e, 60);
        }

        private void Button61_Click(object sender, RoutedEventArgs e)
        {
            OnOpponentClick(sender, e, 61);
        }

        private void Button62_Click(object sender, RoutedEventArgs e)
        {
            OnOpponentClick(sender, e, 62);
        }

        private void Button63_Click(object sender, RoutedEventArgs e)
        {
            OnOpponentClick(sender, e, 63);
        }




        //restul butoanelor
    }
}
