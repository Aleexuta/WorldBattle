using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WorldBattle
{
    class ImagePos
    {
        private String m_id;
        private Image m_img;
        private double m_left;
        private double m_top;
        private double m_down;
        private double m_right;
        private int m_rot;//in functie de rotatia imaginii o sa asezam la left/top/down/right fata de coltul stang-sus al canvasului
        public int m_heightinit { get; set; }
        public int m_widthinit { get; set; }


        public double m_fullleft { get; set; }
        public double m_fulltop { get; set; }

        private int[] listOfNrButtons;
        private int posvector = -1;
        public int nrParatele { get; set; }//TODO //creaza functia de calculare a nr de patratele in functie de id-ul pozei
        private int nrNimerite=0;//TODO //creaza functia de adunare la nrnimerite
        public ImagePos(String id,Image img, double left, double top, double right, double down, int rot,int nrpatratele)
        {
            setID(id);
            setImg(img);
            setRight(right);
            setLeft(left);
            setDown(down);
            setTop(top);
            setRot(rot);
            nrParatele = nrpatratele;
            listOfNrButtons = new int[nrParatele];
        
        }
        
        public bool IsPhotoOnButton(int nrbut)
        {
            for (int i = 0; i < nrParatele; i++)
                if (listOfNrButtons[i] == nrbut)
                    return true;
            return false;
        }
        public void PhotoTouched()
        {
            nrNimerite++;
        }
        public bool isPhotoFull()
        {
            return nrParatele == nrNimerite;
        }
        public bool disableButtons(int x, int y)
        {
            switch (m_id)
            {

                case "Avion1":
            
                    return disableIdAvion1(x,y);
                    break;
            
                case "Avion2":
            

                    break;
            
                case "Avion3":
            

                    break;
            
                case "Barca1":
                    return disableIdBarca1(x, y);

                    break;
            
                case "Barca2":
                    return disableIdBarca2(x, y);

                    break;
                case "Barca3":
                    return disableIdBarca2(x, y);

                    break;

                case "Barca5":
                    return disableIdBarca5(x, y);

                    break;
            
                case "Tanc1":
                    return disableIdTanc1(x, y);

                    break;
            
                case "Tanc2":
                    return disableIdTanc2(x, y);

                    break;
            
                case "Trupe3":
                    return disableIdTrupe3(x, y);

                    break;

                case "Trupe4":
                    return disableIdTrupe4(x, y);

                    break;

                default:
                    break;
            }
            return false;
        }

        private void setButtonsNumbers(int nrbut)
        {
            posvector++;
            listOfNrButtons[posvector] = nrbut;
        }

        private bool disableIdAvion1(int x,int y)
        {
            int pos = y*8+x ;
            Game game = Game.getInstance();
            try
            {
                if (m_rot == 0)
                {
                    if (x - 1 > 0 || x + 2 > 7 || y - 2 < 0 || y + 2 > 7)
                        return false;
                    game.setTypeMyTable(pos, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos - 8, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos - 2, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos - 1, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos + 1, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos + 2, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos + 8, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos + 15, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos + 16, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos + 17, TypesBoard.UntestedFull);


                    setButtonsNumbers(pos);
                    setButtonsNumbers(pos-8);
                    setButtonsNumbers(pos-2);
                    setButtonsNumbers(pos-1);
                    setButtonsNumbers(pos+1);
                    setButtonsNumbers(pos+2);
                    setButtonsNumbers(pos+8);
                    setButtonsNumbers(pos+15);
                    setButtonsNumbers(pos+16);
                    setButtonsNumbers(pos+17);
                }
                else if (m_rot == 90)
                {
                    if (x - 2 < 0 || x + 1 > 7 || y - 2 < 0 || y + 2 > 7)
                        return false;
                    game.setTypeMyTable(pos, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos - 8, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos - 16, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos + 8, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos + 16, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos - 10, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos - 2, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos - 1, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos + 6, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos + 1, TypesBoard.UntestedFull);

                    setButtonsNumbers(pos);
                    setButtonsNumbers(pos - 8);
                    setButtonsNumbers(pos - 16);
                    setButtonsNumbers(pos +8);
                    setButtonsNumbers(pos + 16);
                    setButtonsNumbers(pos -10);
                    setButtonsNumbers(pos -2);
                    setButtonsNumbers(pos -1);
                    setButtonsNumbers(pos + 6);
                    setButtonsNumbers(pos + 1);
                }
                else if (m_rot == 180)
                {
                    if (y - 2 < 0 || y + 1 > 7 || x - 2 < 0 || x + 2 > 7)
                        return false;
                    game.setTypeMyTable(pos, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos - 8, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos - 15, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos - 16, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos - 17, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos - 1, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos - 2, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos + 1, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos + 2, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos + 8, TypesBoard.UntestedFull);

                    setButtonsNumbers(pos);
                    setButtonsNumbers(pos - 8);
                    setButtonsNumbers(pos - 15);
                    setButtonsNumbers(pos - 16);
                    setButtonsNumbers(pos -17);
                    setButtonsNumbers(pos -1);
                    setButtonsNumbers(pos -2);
                    setButtonsNumbers(pos + 1);
                    setButtonsNumbers(pos + 2);
                    setButtonsNumbers(pos + 8);
                }
                else
                {
                    if (x - 1 < 0 || x + 2 > 7 || y - 2 < 0 || y + 2 > 7)
                        return false;
                    game.setTypeMyTable(pos, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos - 1, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos - 8, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos - 16, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos + 8, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos + 16, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos + 1, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos + 2, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos - 6, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos + 10, TypesBoard.UntestedFull);

                    setButtonsNumbers(pos);
                    setButtonsNumbers(pos - 1);
                    setButtonsNumbers(pos - 8);
                    setButtonsNumbers(pos - 16);
                    setButtonsNumbers(pos + 8);
                    setButtonsNumbers(pos + 16);
                    setButtonsNumbers(pos + 1);
                    setButtonsNumbers(pos + 2);
                    setButtonsNumbers(pos - 6);
                    setButtonsNumbers(pos + 10);
                }
            }
            catch(Exception e)
            {
                return false;
            }
            return true;
        }
       

        private bool disableIdBarca1(int x,int y)
        {
            int pos = y * 8 + x;
            Game game = Game.getInstance();
            try
            {
                if (m_rot == 0)
                {
                    if (x < 0 || x + 3 > 7)
                        return false;
                    game.setTypeMyTable(pos, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos + 1, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos + 2, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos + 3, TypesBoard.UntestedFull);

                    setButtonsNumbers(pos);
                    setButtonsNumbers(pos +1);
                    setButtonsNumbers(pos +2);
                    setButtonsNumbers(pos +3);

                }
                else if (m_rot == 90)
                {
                    if (y < 0 || y + 3 > 7)
                        return false;
                    game.setTypeMyTable(pos, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos + 8, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos + 16, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos + 24, TypesBoard.UntestedFull);

                    setButtonsNumbers(pos);
                    setButtonsNumbers(pos + 8);
                    setButtonsNumbers(pos + 16);
                    setButtonsNumbers(pos + 24);
                }
                else if (m_rot == 180)
                {
                    if (x > 7 || x - 3 < 0)
                        return false;
                    game.setTypeMyTable(pos, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos - 1, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos - 2, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos - 3, TypesBoard.UntestedFull);

                    setButtonsNumbers(pos);
                    setButtonsNumbers(pos - 1);
                    setButtonsNumbers(pos - 2);
                    setButtonsNumbers(pos - 3);
                }
                else
                {
                    if (y > 7 || y - 3 < 0)
                        return false;
                    game.setTypeMyTable(pos, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos - 8, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos - 16, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos - 24, TypesBoard.UntestedFull);

                    setButtonsNumbers(pos);
                    setButtonsNumbers(pos -8);
                    setButtonsNumbers(pos -16);
                    setButtonsNumbers(pos -24);
                }
            }
            catch(Exception e)
            {
                return false;
            }
            return true;
        }

        private bool disableIdBarca2(int x, int y)
        {
            int pos = y * 8 + x;
            Game game = Game.getInstance();
            try
            {
                if (m_rot == 0)
                {
                    if (x - 1 < 0 || x > 7 || y - 1 < 0 || y > 7)
                        return false;
                    game.setTypeMyTable(pos, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos - 1, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos - 8 , TypesBoard.UntestedFull);

                    setButtonsNumbers(pos);
                    setButtonsNumbers(pos - 1);
                    setButtonsNumbers(pos - 8);

                }
                else if (m_rot == 90)
                {
                    if (x < 0 || x + 1 > 7 || y - 1 < 0 || y > 7)
                        return false;
                    game.setTypeMyTable(pos, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos - 8, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos + 1, TypesBoard.UntestedFull);

                    setButtonsNumbers(pos);
                    setButtonsNumbers(pos - 8);
                    setButtonsNumbers(pos + 1);
                }
                else if (m_rot == 180)
                {
                    if (x < 0 || x + 1 > 7 || y < 0 || y + 1 > 7)
                        return false;
                    game.setTypeMyTable(pos, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos + 1, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos + 8, TypesBoard.UntestedFull);

                    setButtonsNumbers(pos);
                    setButtonsNumbers(pos + 1);
                    setButtonsNumbers(pos + 8);
                }
                else
                {
                    if (x - 1 < 0 ||  x > 7 || y < 0 || y + 1 > 7)
                        return false;
                    game.setTypeMyTable(pos, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos - 1, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos + 8, TypesBoard.UntestedFull);

                    setButtonsNumbers(pos);
                    setButtonsNumbers(pos - 1);
                    setButtonsNumbers(pos + 8);
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        private bool disableIdBarca5(int x, int y)
        {
            int pos = y * 8 + x;
            Game game = Game.getInstance();
            try
            {
                if (m_rot == 0)
                {
                    if (x  < 0 || x + 2 > 7 || y - 1 < 0 || y + 1 > 7)
                        return false;
                    game.setTypeMyTable(pos, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos - 8, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos + 8, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos + 1, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos + 2, TypesBoard.UntestedFull);

                    setButtonsNumbers(pos);
                    setButtonsNumbers(pos - 8);
                    setButtonsNumbers(pos + 8);
                    setButtonsNumbers(pos + 1);
                    setButtonsNumbers(pos + 2);

                }
                else if (m_rot == 90)
                {
                    if (x - 1 < 0 || x + 1 > 7 || y < 0 || y + 2 > 7)
                        return false;
                    game.setTypeMyTable(pos, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos - 1, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos + 1, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos + 8, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos + 16, TypesBoard.UntestedFull);

                    setButtonsNumbers(pos);
                    setButtonsNumbers(pos - 1);
                    setButtonsNumbers(pos + 1);
                    setButtonsNumbers(pos + 8);
                    setButtonsNumbers(pos + 16);
                }
                else if (m_rot == 180)
                {
                    if (x - 2 < 0 || x  > 7 || y - 1 < 0 || y + 1 > 7)
                        return false;
                    game.setTypeMyTable(pos, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos - 1, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos - 2, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos - 8, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos + 8, TypesBoard.UntestedFull);

                    setButtonsNumbers(pos);
                    setButtonsNumbers(pos - 1);
                    setButtonsNumbers(pos - 2);
                    setButtonsNumbers(pos + 8);
                    setButtonsNumbers(pos - 8);
                }
                else
                {
                    if (x - 1 < 0 || x + 1 > 7 || y - 2 < 0 || y > 7)
                        return false;
                    game.setTypeMyTable(pos, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos - 1, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos + 1, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos - 8, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos - 16, TypesBoard.UntestedFull);

                    setButtonsNumbers(pos);
                    setButtonsNumbers(pos - 1);
                    setButtonsNumbers(pos + 1);
                    setButtonsNumbers(pos - 8);
                    setButtonsNumbers(pos - 16);
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        private bool disableIdTanc1(int x, int y)
        {
            int pos = y * 8 + x;
            Game game = Game.getInstance();
            try
            {
                if (m_rot == 0)
                {
                    if (x < 0 || x + 1 > 7)
                        return false;
                    game.setTypeMyTable(pos, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos + 1, TypesBoard.UntestedFull);

                    setButtonsNumbers(pos);
                    setButtonsNumbers(pos + 1);

                }
                else if (m_rot == 90)
                {
                    if (y < 0 || y + 1 > 7)
                        return false;
                    game.setTypeMyTable(pos, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos + 8, TypesBoard.UntestedFull);

                    setButtonsNumbers(pos);
                    setButtonsNumbers(pos + 8);
                }
                else if (m_rot == 180)
                {
                    if (x - 1 < 0 || x > 7 )
                        return false;
                    game.setTypeMyTable(pos, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos - 1, TypesBoard.UntestedFull);

                    setButtonsNumbers(pos);
                    setButtonsNumbers(pos - 1);
                }
                else
                {
                    if (y - 1 < 0 || y > 7)
                        return false;
                    game.setTypeMyTable(pos, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos - 8, TypesBoard.UntestedFull);

                    setButtonsNumbers(pos);
                    setButtonsNumbers(pos - 8);
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        private bool disableIdTanc2(int x, int y)
        {
            int pos = y * 8 + x;
            Game game = Game.getInstance();
            try
            {
                if (m_rot == 0)
                {
                    if (x - 2 < 0 || x > 7)
                        return false;
                    game.setTypeMyTable(pos, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos - 1, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos - 2, TypesBoard.UntestedFull);

                    setButtonsNumbers(pos);
                    setButtonsNumbers(pos - 1);
                    setButtonsNumbers(pos - 2);

                }
                else if (m_rot == 90)
                {
                    if (y - 2 < 0 || y > 7)
                        return false;
                    game.setTypeMyTable(pos, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos - 8, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos - 16, TypesBoard.UntestedFull);

                    setButtonsNumbers(pos);
                    setButtonsNumbers(pos - 16);
                    setButtonsNumbers(pos - 8);
                }
                else if (m_rot == 180)
                {
                    if (x < 0 || x + 2 > 7)
                        return false;
                    game.setTypeMyTable(pos, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos + 1, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos + 2, TypesBoard.UntestedFull);

                    setButtonsNumbers(pos);
                    setButtonsNumbers(pos + 1);
                    setButtonsNumbers(pos + 2);
                }
                else
                {
                    if (y < 0 || y + 2 > 7)
                        return false;
                    game.setTypeMyTable(pos, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos + 8, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos + 16, TypesBoard.UntestedFull);

                    setButtonsNumbers(pos);
                    setButtonsNumbers(pos + 8);
                    setButtonsNumbers(pos + 16);
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        private bool disableIdTrupe3(int x, int y)
        {
            int pos = y * 8 + x;
            Game game = Game.getInstance();
            try
            {
                if (m_rot == 0)
                {
                    if (x < 0 || x + 1 > 7 || y - 1 < 0 || y > 7)
                        return false;
                    game.setTypeMyTable(pos, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos + 1, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos - 8, TypesBoard.UntestedFull);

                    setButtonsNumbers(pos);
                    setButtonsNumbers(pos + 1);
                    setButtonsNumbers(pos - 8);
                }
                else if (m_rot == 90)
                {
                    if (x < 0 || x + 1 > 7 || y < 0 || y + 1 > 7)
                        return false;
                    game.setTypeMyTable(pos, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos + 1, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos + 8, TypesBoard.UntestedFull);

                    setButtonsNumbers(pos);
                    setButtonsNumbers(pos + 1);
                    setButtonsNumbers(pos + 8);
                }
                else if (m_rot == 180)
                {
                    if (x - 1 < 0 || x > 7 || y < 0 || y + 1 > 7)
                        return false;
                    game.setTypeMyTable(pos, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos - 1, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos + 8, TypesBoard.UntestedFull);

                    setButtonsNumbers(pos);
                    setButtonsNumbers(pos - 1);
                    setButtonsNumbers(pos + 8);
                }
                else
                {
                    if (x - 1 < 0 || x > 7 || y - 1 < 0 || y > 7)
                        return false;
                    game.setTypeMyTable(pos, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos - 1, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos - 8, TypesBoard.UntestedFull);

                    setButtonsNumbers(pos);
                    setButtonsNumbers(pos - 1);
                    setButtonsNumbers(pos - 8);
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        private bool disableIdTrupe4(int x, int y)
        {
            int pos = y * 8 + x;
            Game game = Game.getInstance();
            try
            {
                if (m_rot == 0)
                {
                    if (x < 0 || x + 1 > 7 || y - 2 < 0 || y > 7)
                        return false;
                    game.setTypeMyTable(pos, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos + 1, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos - 8, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos - 16, TypesBoard.UntestedFull);

                    setButtonsNumbers(pos);
                    setButtonsNumbers(pos + 1);
                    setButtonsNumbers(pos - 8);
                    setButtonsNumbers(pos - 16);
                }
                else if (m_rot == 90)
                {
                    if (x < 0 || x + 2 > 7 || y < 0 || y + 1 > 7)
                        return false;
                    game.setTypeMyTable(pos, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos + 1, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos + 2, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos + 8, TypesBoard.UntestedFull);

                    setButtonsNumbers(pos);
                    setButtonsNumbers(pos + 1);
                    setButtonsNumbers(pos + 2);
                    setButtonsNumbers(pos + 8);
                }
                else if (m_rot == 180)
                {
                    if (x - 1 < 0 || x > 7 || y < 0 || y + 2 > 7)
                        return false;
                    game.setTypeMyTable(pos, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos - 1, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos + 8, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos + 16, TypesBoard.UntestedFull);

                    setButtonsNumbers(pos);
                    setButtonsNumbers(pos - 1);
                    setButtonsNumbers(pos + 8);
                    setButtonsNumbers(pos + 16);
                }
                else
                {
                    if (x - 2 < 0 || x > 7 || y - 1 < 0 || y > 7)
                        return false;
                    game.setTypeMyTable(pos, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos - 1, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos - 2, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos - 8, TypesBoard.UntestedFull);

                    setButtonsNumbers(pos);
                    setButtonsNumbers(pos - 1);
                    setButtonsNumbers(pos - 2);
                    setButtonsNumbers(pos - 8);
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        private void calculatePatratele()
        {
            if (m_id == "Avion1")
                nrParatele = 10;
            if (m_id == "Barca1")
                nrParatele = 4;
            if (m_id == "Barca2")
                nrParatele = 3;
            if (m_id == "Barca3")
                nrParatele = 3;
            if (m_id == "Barca5")
                nrParatele = 5;
            if (m_id == "Tanc1")
                nrParatele = 2;
            if (m_id == "Tanc2")
                nrParatele = 2;
            if (m_id == "Trupe3")
                nrParatele = 3;
            if (m_id == "Trupe4")
                nrParatele = 4;

            nrParatele = 0;
        }

        public void setID(String id) { m_id = id; }
        public void setLeft(double left) { m_left = left; }
        public void setRight(double right) { m_right = right; }
        public void setTop(double top) { m_top = top; }
        public void setDown(double down) { m_down = down; }
        public void setRot(int rot) { m_rot = rot; }
        public void setImg(Image img) { m_img = img; }
        
        public String getID() { return m_id; }
        public double getLeft() { return m_left; }
        public double getRight() { return m_right; }
        public double getTop() { return m_top; }
        public double getDown() { return m_down; }
        public Image getImg() { return m_img; }
        public int getRot() { return m_rot; }
        public void addRot(int rot) {
            m_rot += rot;
            if (m_rot == 360)
                m_rot = 0;
        }
        public int getWidthByRot()
        {
            if (m_rot == 90 || m_rot == 270)
                return m_widthinit;
            else return m_heightinit;
        }
        public int getHeightByRot()
        {
            if (m_rot == 180 || m_rot == 0)
                return m_widthinit;
            else return m_heightinit;
        }
        public double getTopPosition()
        {
            if (m_rot == 0)
            {
                return m_top;
            }
            else
            if (m_rot == 90)
            {
                return m_left;
            }
            else
            if (m_rot == 180)
            {
                return m_down;
            }
            else
            {
                return m_right;
            }
        }
        public double getLeftPosition()
        {
            if (m_rot == 0)
            {
                return m_left;
            }
            else
            if (m_rot == 90)
            {
                return m_down;
            }
            else
            if (m_rot == 180)
            {
                return m_right;
            }
            else
            {
                return m_top;
            }
        }

       

    }
}
