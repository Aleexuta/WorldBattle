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

        private int nrParatele;//TODO //creaza functia de calculare a nr de patratele in functie de id-ul pozei
        private int nrNimerite;//TODO //creaza functia de adunare la nrnimerite
        public ImagePos(String id,Image img, double left, double top, double right, double down, int rot)
        {
            setID(id);
            setImg(img);
            setRight(right);
            setLeft(left);
            setDown(down);
            setTop(top);
            setRot(rot);
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
            

                    break;
            
                case "Barca3":
            

                    break;
            
                case "Tanc1":
            

                    break;
            
                case "Tanc2":
            

                    break;
            
                case "Tanc3":
            

                    break;
            
                default:
                    break;
            }
            return false;
        }

        private bool disableIdAvion1(int x,int y)
        {
            int pos = y*8+x ;
            Game game = Game.getInstance();
            try
            {
                if (m_rot == 0)
                {
                    if (x - 1 > 0 || x - 2 > 7 || y - 2 < 0 || y + 2 > 7)
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
                }
                else if (m_rot == 90)
                {
                    if (y < 0 || y + 3 > 7)
                        return false;
                    game.setTypeMyTable(pos, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos + 8, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos + 16, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos + 24, TypesBoard.UntestedFull);
                }
                else if (m_rot == 180)
                {
                    if (x > 7 || x - 3 < 0)
                        return false;
                    game.setTypeMyTable(pos, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos - 1, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos - 2, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos - 3, TypesBoard.UntestedFull);
                }
                else
                {
                    if (y > 7 || y - 3 < 0)
                        return false;
                    game.setTypeMyTable(pos, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos - 8, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos - 16, TypesBoard.UntestedFull);
                    game.setTypeMyTable(pos - 24, TypesBoard.UntestedFull);
                }
            }
            catch(Exception e)
            {
                return false;
            }
            return true;
        }










        public void setID(String id) { m_id = id; }
        public void setLeft(double left) { m_left = left; }
        public void setRight(double right) { m_right = right; }
        public void setTop(double top) { m_top = top; }
        public void setDown(double down) { m_down = down; }
        public void setRot(int rot) { m_rot = rot; }
        public void setImg(Image img) { m_img = img; }
        
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
