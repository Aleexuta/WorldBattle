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
using System.Windows.Media.Animation;
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
            GenerateMyTableButtonArray();
            GeneratePhotos();
            disable_enableButtons(opponetsButtons, false);
            this.stream = stream;
            this.game = new Game(player);
            yourTurnButton.IsEnabled=false;
            
            // TODO
            // seteaza subtitlul pe prepare si abia dupa ce e gata prepararea se seteaza randul
            if (player == "First")
            {
                this.subtitle.Text = "You are the host.\nPrepare your table and click on ready.";
            }
            else
            {
                this.subtitle.Text = "You are the guest.\nPrepare your table and click on ready.";
            }
            //  UpdateBoard();
            if (!game.isTurn())
            {
                WaitForResponse();
            }

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
                    WriteMessage("Select," + Convert.ToString(button));//trimitem pozitia pe care dorim sa o incercam 
                }
                WaitForResponse();
            }
        }
        private void OnOurTableClick(object sender, EventArgs e, int button)
        {
            if(game.isOver()==false)
            {
                if(game.isInPrepareMode()==true)
                {
                    //prepar tabla mea
                    //TODO
                    //rotate poza
                    //click pe butonul unde vrem sa punem capul pozei
                    //punem continuarea "barcii" in jurul pct de cap
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
        private void UpdateYourBoard(int position)
        { 
            //updatam interfata in functie de cele doua table de joc
            //TODO?->INTREABA
            //updatetable doar pt pozitia care sa schimbat, nu trebe sa modifici toata tabla
            //pozitia unde am dat click si se aseaza un jeton ramane asa pana la finalul meciului   
            if(game.getTypeFromYourTable(position)==TypesBoard.TestedEmpty)
            {
               // se pune jeton de nu e nimic
                Image brush = new Image();
                brush.Source = new BitmapImage(new Uri("/Poze/GrayX.jpeg", UriKind.Relative));
                opponetsButtons[position].Content=brush;
            }
            else
            {
                //se pune jeton ca e ceva
                Image brush = new Image();
                brush.Source = new BitmapImage(new Uri("/Poze/RedX.jpeg", UriKind.Relative));
                opponetsButtons[position].Content = brush;
            }
        }
        private void ColorDisable(object sender, System.EventArgs e,Button but)
        {
            but.Background = Brushes.AliceBlue;
        }
        private void UpdateMyBoard(int position)
        {
            if(game.getTypeFromTable(position)==TypesBoard.TestedEmpty)
            {
                //seteaza ca a fost incercata
                //nu merge
                mytableButtons[position].Background = new SolidColorBrush(Colors.Green);
                Image brush = new Image();
                brush.Source = new BitmapImage(new Uri("/Poze/GrayX.jpeg", UriKind.Relative));
                brush.Opacity = 0.5;
                mytableButtons[position].Content = brush;
            }
            else
            {
                //seteaza ca a fost incercata si atinsa
                mytableButtons[position].Background = Brushes.Red;
                Image brush = new Image();
                brush.Source = new BitmapImage(new Uri("/Poze/RedX.jpeg", UriKind.Relative));
                brush.Opacity = 0.5;
                mytableButtons[position].Content = brush;
            }
        }

        private void disable_enableButtons(Button[] buttons, bool state)
        {
            for(int i=0; i<buttons.Length; i++)
            {
                buttons[i].IsEnabled = state;
            }
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
                this.subtitle.Text = "Your opponent is ready.";

                if (game.isInPrepareMode() == false)
                {
                    if (game.isTurn() == true)
                    {
                        subtitle.Text = "It's your turn";
                        yourTurnButton.IsEnabled = true;
                        disable_enableButtons(opponetsButtons, true);
                    }
                    else
                    {
                        yourTurnButton.IsEnabled = false;
                        disable_enableButtons(opponetsButtons, false);
                        WriteMessage("YourTurn");
                    }
                }



            }
            else if(dataString[0] == "YourTurn")
            {
                subtitle.Text = "It's your turn";
                game.setTurn(true);
                yourTurnButton.IsEnabled = true;
                disable_enableButtons(opponetsButtons, true);
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
                UpdateMyBoard(nrbut);
                WriteMessage(newmessage);
            }
            else if(dataString[0]=="Verified")//primesc felul de buton ce e
            {
                int nrbut = Convert.ToInt32(dataString[1]);
                if(dataString[2]=="Empty")
                {
                    game.setTypeYourTable(nrbut, TypesBoard.TestedEmpty);
                    disable_enableButtons(opponetsButtons, false);
                    yourTurnButton.IsEnabled = true;
                }
                else
                {
                    yourTurnButton.IsEnabled = false;
                    game.setTypeYourTable(nrbut, TypesBoard.TestedFull);
                }
                //TODO
                //seteaza butonul nrbut al adversarului pe disable
                UpdateYourBoard(nrbut);
                opponetsButtons[nrbut].IsEnabled = false;
            }

            WaitForResponse();
        }

        private void WriteMessage(String data)
        {
            Byte[] bytes = new Byte[256];
            bytes = System.Text.Encoding.ASCII.GetBytes(data);
            stream.Write(bytes, 0, bytes.Length);
        }




        private void endTurn()
        {
            this.subtitle.Text = "Wait for opponent to  end";
            game.setTurn(false);
            yourTurnButton.IsEnabled = false;
            WriteMessage("YourTurn");
        }

        private void yourTurnButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO
            // verifica sa aiba dreptul de terminare(nu mai poate apasa pe tabla oponentului pt ca nu a ghicit
            endTurn();
        }

        private void readyButton_Click(object sender, RoutedEventArgs e)
        {
            // TO DO: VERIFICAM CONDITII INAINTE SA FACEM ASTEA
            //CONDITII : DACA S-AU PUS TOATE BARCILE, ETC ETC

            game.setMineStateReady(false); //we are done preparing... we are ready for fighT
            disable_enableButtons(mytableButtons, false);
            readyButton.IsEnabled = false;
            if (game.isOpponentReady() == true)
            {
                if (game.isTurn() == true)
                {
                    subtitle.Text = "It's your turn";
                    yourTurnButton.IsEnabled = true;
                    disable_enableButtons(opponetsButtons, true);
                }
                else
                {
                    yourTurnButton.IsEnabled = false;
                    disable_enableButtons(opponetsButtons, false);
                    WriteMessage("YourTurn");
                }
            }

            else
            {
                subtitle.Text = "Waiting for opponent";
                WriteMessage("Ready");
            }
            WaitForResponse();
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

            //####DONE###
        }

        private void GenerateMyTableButtonArray()
        {
            this.mytableButtons[0] = MButton0;
            this.mytableButtons[1] = MButton1;
            this.mytableButtons[2] = MButton2;
            this.mytableButtons[3] = MButton3;
            this.mytableButtons[4] = MButton4;
            this.mytableButtons[5] = MButton5;
            this.mytableButtons[6] = MButton6;
            this.mytableButtons[7] = MButton7;

            //this.mytableButtons[8] = MButton8;
            //this.mytableButtons[9] = MButton9;
            //this.mytableButtons[10] = MButton10;
            //this.mytableButtons[11] = MButton11;
            //this.mytableButtons[12] = MButton12;
            //this.mytableButtons[13] = MButton13;
            //this.mytableButtons[14] = MButton14;
            //this.mytableButtons[15] = MButton15;

            //this.mytableButtons[16] = MButton16;
            //this.mytableButtons[17] = MButton17;
            //this.mytableButtons[18] = MButton18;
            //this.mytableButtons[19] = MButton19;
            //this.mytableButtons[20] = MButton20;
            //this.mytableButtons[21] = MButton21;
            //this.mytableButtons[22] = MButton22;
            //this.mytableButtons[23] = MButton23;

            //this.mytableButtons[24] = MButton24;
            //this.mytableButtons[25] = MButton25;
            //this.mytableButtons[26] = MButton26;
            //this.mytableButtons[27] = MButton27;
            //this.mytableButtons[28] = MButton28;
            //this.mytableButtons[29] = MButton29;
            //this.mytableButtons[30] = MButton30;
            //this.mytableButtons[31] = MButton31;

            //this.mytableButtons[32] = MButton32;
            //this.mytableButtons[33] = MButton33;
            //this.mytableButtons[34] = MButton34;
            //this.mytableButtons[35] = MButton35;
            //this.mytableButtons[36] = MButton36;
            //this.mytableButtons[37] = MButton37;
            //this.mytableButtons[38] = MButton38;
            //this.mytableButtons[39] = MButton39;

            //this.mytableButtons[40] = MButton40;
            //this.mytableButtons[41] = MButton41;
            //this.mytableButtons[42] = MButton42;
            //this.mytableButtons[43] = MButton43;
            //this.mytableButtons[44] = MButton44;
            //this.mytableButtons[45] = MButton45;
            //this.mytableButtons[46] = MButton46;
            //this.mytableButtons[47] = MButton47;

            //this.mytableButtons[48] = MButton48;
            //this.mytableButtons[49] = MButton49;
            //this.mytableButtons[50] = MButton50;
            //this.mytableButtons[51] = MButton51;
            //this.mytableButtons[52] = MButton52;
            //this.mytableButtons[53] = MButton53;
            //this.mytableButtons[54] = MButton54;
            //this.mytableButtons[55] = MButton55;

            //this.mytableButtons[56] = MButton56;
            //this.mytableButtons[57] = MButton57;
            //this.mytableButtons[58] = MButton58;
            //this.mytableButtons[59] = MButton59;
            //this.mytableButtons[60] = MButton60;
            //this.mytableButtons[61] = MButton61;
            //this.mytableButtons[62] = MButton62;
            //this.mytableButtons[63] = MButton63;
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


        Image selectedimage = null;
        //mai bine fa un hashmap cu imaginea ceva legat de cap, ca sa se plaseze in canvas in functie de cap


        private void GeneratePhotos()
        {
            //poza1
            BitmapImage bit = new BitmapImage(new Uri("Poze/barca1.png", UriKind.Relative));
        
            poza1.Source = bit;
            BitmapImage bit2 = (BitmapImage)poza1.Source;
            TransformedBitmap trans = new TransformedBitmap();
            trans.BeginInit();
            trans.Source = bit2;
            RotateTransform rot = new RotateTransform(90);
            trans.Transform = rot;
            trans.EndInit();
            poza1.Source = trans;
            poza1.Height = 60*4;
            poza1.Width = 60;
        }

        private void rotateImage(Image img)
        {
            TransformedBitmap bit = (TransformedBitmap)img.Source;
            TransformedBitmap trans = new TransformedBitmap();
            trans.BeginInit();
            bit.Transform.Value.Rotate(90);
            trans.Source = bit;
            
            trans.Transform = bit.Transform;

            trans.EndInit();
            double aux = img.Width;
            img.Width = img.Height;
            img.Height = aux;
            img.Source = trans;

        }
        private void movePhoto(double left, double top)
        {
            Image bodyimage = new Image
            {
                Width = selectedimage.Width,
                Height = selectedimage.Height,
                Source = selectedimage.Source
              
            };
            mytable.Children.Add(bodyimage);
            Canvas.SetTop(bodyimage,top);
            Canvas.SetLeft(bodyimage, left);
            selectedimage.Width = 0;
            selectedimage.Height = 0;
            selectedimage.Source = null;
            selectedimage = null;
        }

        private void poza1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            selectedimage = poza1;
        }

        private void poza1_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            rotateImage(poza1);
        }

        private void MButton0_Click(object sender, RoutedEventArgs e)
        {
            Button but = (Button)sender;
            if (selectedimage!=null)
            {
                movePhoto((double)but.GetValue(Canvas.LeftProperty),(double) but.GetValue(Canvas.TopProperty));
            }
        }





        //restul butoanelor
    }
}
