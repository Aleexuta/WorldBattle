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

    
    //TODO
    //combo nr de ghiciri pe ecran 

    public partial class GameUI : Window
    {
        int NRPOZE = 2;
        int SIZEBUTTONS = 40;   
        private NetworkStream stream;
        private Game game;
        private System.ComponentModel.IContainer components = null;
        private Button[] mytableButtons = new Button[64];
        private Button[] opponetsButtons = new Button[64];

        private int NrElementePlasate = 0;
        private int NrCombo = 0;
        private int NrNimeriri = 0;
        private int NrOcupate = 0;

        private string tipTeren;
        private string username;


        ImagePos[] images = new ImagePos[4];
        public GameUI(NetworkStream stream, String player, String username, String tipTeren)
        {
            InitializeComponent();
            //TODO

            this.tipTeren = tipTeren;
            this.username = username;

            //if(tipTeren == "")  //urmeaza cazurile
            //{
            //    var uri = new Uri("url poza");
            //    var bitmap = new BitmapImage(uri);
            //    GameGrid.Background = new ImageBrush(bitmap);
            //}


            //mai intai face butoanele tuturor
            //seteaza butoanele adversarului pe disable

            GenerateOpponentButtonArray();
            GenerateMyTableButtonArray();
            GeneratePhotos();
            disable_enableButtons(opponetsButtons, false);
            this.stream = stream;
            this.game = Game.getInstance(player);
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
            else if(dataString[0]=="Select") //adversarul a apasat pe ceva , verific la mine ce fel de buton e si transmit mai departe
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
                    incrementPhotoNumber(nrbut);
                    //TODO
                    //trimite la poza ca a fost nimerit. cand e full trebuie sa trimita la mesaj ca a ghicit poza si id-ul ei
                    ImagePos img = IsFullPhoto(nrbut);//daca poza e full trimitem poza catre adversar cu pozitile aferente
                    if (img!=null)
                    {
                        newmessage += ",Photo," + img.getID() + "," + 
                            Convert.ToString(img.m_fullleft) + "," +
                            Convert.ToString(img.m_fulltop) + "," + 
                            img.getRot()+","+img.getHeightByRot()+","+img.getWidthByRot();
                    }
                    else
                    {
                        newmessage += ",Nophoto";
                    }
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
                    Infolabel.Text="Nu ai nimerit, nu mai ai dreptul la incercari";
                    //TODO
                    //adauga sunnet de tristete?, sau de splash in apa, dar diferit de cel de bomba

                    disable_enableButtons(opponetsButtons, false);
                    yourTurnButton.IsEnabled = true;
                }
                else
                {
                    yourTurnButton.IsEnabled = false;
                    game.setTypeYourTable(nrbut, TypesBoard.TestedFull);
                    BombPlassed();
                    if (dataString[3]=="Photo")
                    {
                        setFullPhotoOppTable(dataString[4],Convert.ToDouble(dataString[5]),Convert.ToDouble( dataString[6]), Convert.ToInt32(dataString[7]),
                            Convert.ToInt32(dataString[8]), Convert.ToInt32(dataString[9]));
                        //TODO
                        //daca a primit la mesaj si faptul ca a intregit un element se ia id ul si se adauga poza la pozitia respectiva.
                        //ATENTIE, primim id-ul pozei, si rotatia pe langa nr butonului
                        //verifica terminarea jocului
                        VerifyGameOver();

                    }




                    //TODO
                    //adauga sunet de bomba
                }

                UpdateYourBoard(nrbut);
                opponetsButtons[nrbut].IsEnabled = false;
            }
            else if(dataString[0]=="GameOver")
            {
                //TODO
                //afiseaza mesaj ca am pierdut, disable tot
                MessageBox.Show("Ai pierdut");
                endGame();
            }
            WaitForResponse();
        }
        private void incrementPhotoNumber(int nrbut)
        {
            ImagePos img = getImage(nrbut);
            img.PhotoTouched();//adversarul a atins poza si incrementam la poza patratelele nimerite
        }
        private void setFullPhotoOppTable(String id,double left, double top, int rot, int height, int width)
        {

            
            //cream imaginea, in functie de idul pozei incarcam un anumit tip de poza, ii setam inaltimea si latimea
            //o plasam dupa parametrii primmiti in canvasul oponentului dedesuptul butoanelor
            MessageBox.Show("Element nimerit");
            String imguri = getUriForId(id);
            //nu mereee
            BitmapImage Bit = new BitmapImage(new Uri(imguri, UriKind.Relative));
            TransformedBitmap trans = new TransformedBitmap();
            trans.BeginInit();
            trans.Source = Bit;

            RotateTransform rot2 = new RotateTransform(rot);
            trans.Transform = rot2;
            trans.EndInit();
            Image newimg = new Image
            {
                Height = height*SIZEBUTTONS,
                Width = width*SIZEBUTTONS
                
            };
            newimg.Source = trans;
            fundalopponent.Children.Add(newimg);
            Canvas.SetLeft(newimg, left);
            Canvas.SetTop(newimg, top);

        }

        private void VerifyGameOver()
        {
            //TODO
            //verifica nr de patratele sa fie egale
            //da disable la toate butoanele, afiseaza mesaj
            //trimite mesaj catre adversar ca  a terminat meciul
            if (NrNimeriri == NrOcupate)
            {
                WriteMessage("GameOver"); //transmitem mesajul ca adversarul a castigat
                MessageBox.Show("Ai castigat");
                endGame();
            }
        }

        private void endGame()
        {
            this.Close();
            //TODO
            //fa ceva aici, o  poza de game over or smth
        }
        private ImagePos IsFullPhoto(int nrbut)
        {
            ImagePos img = getImage(nrbut);//imaginea pt care am nimerit pe tabla de joc
            if(img!=null)
            {
                if (img.isPhotoFull() == true)//daca poza e intragita
                    return img;
            }
            return null;
        }
        private void BombPlassed()
        {
            NrNimeriri++;
            Infolabel.Text = "BUM!.Ai nimerit.\n Mai ai dreptul la o inccercare";
            NrCombo++;
            if(NrCombo>1)
            {  
                ComboLabel.Text = "COMBO x" + NrCombo;
            }

            
        }
        private void WriteMessage(String data)
        {
            Byte[] bytes = new Byte[256];
            bytes = System.Text.Encoding.ASCII.GetBytes(data);
            stream.Write(bytes, 0, bytes.Length);
        }
        private void endTurn()
        {
            this.subtitle.Text = "Wait for opponent to end";
            game.setTurn(false);
            yourTurnButton.IsEnabled = false;
            WriteMessage("YourTurn");
            NrCombo = 0;
            Infolabel.Text = "";
            ComboLabel.Text = "";
        }

        private void yourTurnButton_Click(object sender, RoutedEventArgs e)
        {
            endTurn();
        }

        private void readyButton_Click(object sender, RoutedEventArgs e)
        {
            if (NrElementePlasate < NRPOZE)
            {
                MessageBox.Show("Trebuie sa plasezi toate elementele");
                return;
            }
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
                    subtitle.Text = "It's your opponents turn";
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
        private void OnOpponentClick(object sender, EventArgs e)
        {
            if (game.isOver() == false)
            {
                if (game.isTurn() == true)
                {
                    Button but = (Button)sender;
                    String nume = but.Name.Substring(6,but.Name.Length-6);
                    //MessageBox.Show("Ai selectat butonul nr " + nume);
                    WriteMessage("Select," + nume);//trimitem pozitia pe care dorim sa o incercam 
                }
                WaitForResponse();
            }
        }
        private void GenerateOpponentButtonArray()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    int pos = i * 8 + j;
                    this.opponetsButtons[pos] = new Button {Name ="Button"+pos, Width = SIZEBUTTONS, Height = SIZEBUTTONS };
                    this.opponetsButtons[pos].Click += new RoutedEventHandler(OnOpponentClick);
                    Canvas.SetLeft(this.opponetsButtons[pos], j * SIZEBUTTONS);
                    Canvas.SetTop(this.opponetsButtons[pos], i * SIZEBUTTONS);
                    opponenttable.Children.Add(this.opponetsButtons[pos]);
                }
            }

        }

        private void GenerateMyTableButtonArray()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    int pos = i * 8 + j;
                    this.mytableButtons[pos] = new Button { Name = Convert.ToString("M_Button" + pos), Width = SIZEBUTTONS, Height = SIZEBUTTONS, Opacity=1 };
                    this.mytableButtons[pos].Click += new RoutedEventHandler(OnMyTableClick);
                    Canvas.SetLeft(this.mytableButtons[pos], j * SIZEBUTTONS);
                    Canvas.SetTop(this.mytableButtons[pos], i * SIZEBUTTONS);
                    mytable.Children.Add(this.mytableButtons[pos]);
                }
            }
        }



        ImagePos selectedimage = null;
        //mai bine fa un hashmap cu imaginea ceva legat de cap, ca sa se plaseze in canvas in functie de cap

        private void createPhoto(int id,String uri,Image imgsource,int height, int width, String name, 
            double left, double top, double right, double down,int nrpatratele)
        {
            BitmapImage Bit = new BitmapImage(new Uri(uri, UriKind.Relative));

            imgsource.Source = Bit;
            BitmapImage bit = (BitmapImage)imgsource.Source;
            TransformedBitmap trans = new TransformedBitmap();
            trans.BeginInit();
            trans.Source = bit;
            RotateTransform rot = new RotateTransform(90);
            trans.Transform = rot;
            trans.EndInit();

            imgsource.Source = trans;
            imgsource.Height = SIZEBUTTONS * height;
            imgsource.Width = SIZEBUTTONS * width;
            imgsource.MouseRightButtonDown += new MouseButtonEventHandler(RightClickPhoto);
            imgsource.MouseLeftButtonDown += new MouseButtonEventHandler(LeftClickPhoto);
            imgsource.Name = name;
            images[id] = new ImagePos(name, imgsource, left * SIZEBUTTONS, top * SIZEBUTTONS, right * SIZEBUTTONS, down * SIZEBUTTONS, 90, nrpatratele);
            NrOcupate += images[id].nrParatele;
            images[id].m_heightinit = height;
            images[id].m_widthinit = width;
        }
        private void GeneratePhotos()
        {
            //poza1


            createPhoto(0, "Poze\\barca1.png", poza1, 4, 1, "Barca1", 0, 0, 3, 0, 4);
            //poza 2


            createPhoto(1, "Poze\\avion1.png", poza2, 4, 5,"Avion1" ,1.5, 1.5, 1.5, 2.5, 10);
        }

        private void rotateImage(int i)
        {
            TransformedBitmap bit = (TransformedBitmap)images[i].getImg().Source;
            TransformedBitmap trans = new TransformedBitmap();
            trans.BeginInit();
            bit.Transform.Value.Rotate(90);
            trans.Source = bit;
            
            trans.Transform = bit.Transform;

            trans.EndInit();
            double aux =images[i].getImg().Width;
            images[i].getImg().Width =images[i].getImg().Height;
            images[i].getImg().Height = aux;
            images[i].getImg().Source = trans;
            images[i].addRot(90);

        }
        private bool setTheButtons(int nrphoto, double centerX, double centerY)
        {
            //in functie de numele pozei din vector si rotatia imaginii se dau disable la anumite butoane
            bool rez= images[nrphoto].disableButtons((int)centerX/SIZEBUTTONS,(int)centerY/SIZEBUTTONS);
            if(rez==false)
                MessageBox.Show("Nu se poate efectua aceasta miscare, se depaseste tabla de joc");
            return rez;

        }
        private void movePhoto(double left, double top)//left=distanta fata de stg canvasului a butonului top=fata de top
        {

            //TODO
            //verifica daca imaginea se poate muta la pozitia propusa de user si la rotatia respetiva
            bool rez = false ;
            for (int i = 0; i < NRPOZE; i++)
            {
                if (images[i].getImg() == selectedimage.getImg())
                {
                    rez=setTheButtons(i, left, top);
                    break;
                }
            }
            if (rez == false)
                return;


            Image bodyimage = new Image
            {
                Width = selectedimage.getImg().Width,
                Height = selectedimage.getImg().Height,
                Source = selectedimage.getImg().Source
              
            };
            fundal.Children.Add(bodyimage);
      
            double imgtop=0, imgleft=0;
            

            imgtop = selectedimage.getTopPosition();
            imgleft = selectedimage.getLeftPosition();
            Canvas.SetTop(bodyimage,top-imgtop);
            Canvas.SetLeft(bodyimage, left-imgleft);
            selectedimage.m_fulltop = top - imgtop;
            selectedimage.m_fullleft = left - imgleft;
            selectedimage.getImg().Width = 0;
            selectedimage.getImg().Height = 0;
            selectedimage.getImg().Source = null;
            selectedimage = null;
            //adauga in tabela din game
            NrElementePlasate++;

        }

        private void LeftClickPhoto(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            for (int i= 0;i < NRPOZE;i ++)
                if (images[i].getImg().Source==img.Source) 
                    selectedimage = images[i];
        }

        private void RightClickPhoto(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            for(int i=0;i<NRPOZE;i++)
                if (images[i].getImg() == img)
                    rotateImage(i);
        }

        private void OnMyTableClick(object sender, RoutedEventArgs e)
        {
            Button but = (Button)sender;
            if (selectedimage!=null)
            {
                movePhoto((double)but.GetValue(Canvas.LeftProperty),(double) but.GetValue(Canvas.TopProperty));
            }
        }

        private ImagePos getImage(int nrbutton)
        {
            foreach(var x in images)
            {
                if (x.IsPhotoOnButton(nrbutton))
                    return x;
            }
            return null;
        }


        private String getUriForId(String id)
        {
            if (id == "Avion1")
                return "..\\..\\Poze\\avion1.png";
            if (id == "Barca1")
                return "..\\..\\Poze\\barca1.png";
            return "";
        }
        //restul butoanelor
    }
}
