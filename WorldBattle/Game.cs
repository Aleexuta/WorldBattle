using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldBattle
{
    enum TypesBoard
    {

        TestedEmpty,
        TestedFull,
        UntestedEmpty,
        UntestedFull
    };

    class Game
    {
        private int DIM = 8;
        private TypesBoard[] YourBoardBullets;
        private TypesBoard[] MyBoardBullets;
        private String player;
        private String gameInfo;
        private String recentMove;
        private bool gameOver;
        private bool turn;
        private bool prepare;
        private bool opponentready;

        static private Game instance=null;
        private Game(String player)
        {
            this.MyBoardBullets = new TypesBoard[DIM * DIM];
            this.YourBoardBullets = new TypesBoard[DIM * DIM];
            for (int i = 0; i < DIM * DIM; i++)
            {
                this.MyBoardBullets[i] = TypesBoard.UntestedEmpty;
                this.YourBoardBullets[i] = TypesBoard.UntestedEmpty;
            }
            this.player = player;
            this.gameOver = false;
            this.prepare = true;
            this.opponentready = false;
            this.gameInfo = "";
            this.recentMove = "";
            if (player == "First")
            {
                this.turn = true;
            }
            else
            {
                this.turn = false;
            }
            PrepareMyTable();
        }
        public void disableButtonsMyTable()
        {
            for (int i = 0; i < DIM * DIM; i++)
            {
                this.MyBoardBullets[i] = TypesBoard.UntestedEmpty;
            }
        }
        static public Game getInstance(String player)
        {

            instance = new Game(player);
            return instance;
        }
        static public Game getInstance()
        {
            if (instance!=null)
                return instance;
            return null;
        }
        public TypesBoard getTypeFromTable(int nrbut) 
        {
            return MyBoardBullets[nrbut];
        }
        public TypesBoard getTypeFromYourTable(int nrbut)
        {
            return YourBoardBullets[nrbut];
        }
        public void setTypeMyTable(int nrbut,TypesBoard type)
        {
            if(MyBoardBullets[nrbut] == TypesBoard.UntestedFull)
                if (type == TypesBoard.UntestedFull)
                    throw new Exception("Ocupata");

            
            MyBoardBullets[nrbut] = type;
        }
        public void setTypeYourTable(int nrbut, TypesBoard type)
        {
            YourBoardBullets[nrbut] = type;
        }
        public String GetGameInfo() { return this.gameInfo; }
        public String GetRecentMove(){ return this.recentMove; }
        public bool isOver() { return gameOver; }
        public bool isTurn() { return turn; }
        public void setTurn(bool x) { turn = x; }
        public bool isInPrepareMode() { return prepare; }
        public bool isOpponentReady() { return opponentready; }
        public void setOpponentReady(bool x){ this.opponentready = x;}
        public void setMineStateReady(bool x) { this.prepare = x; }
        
        
        
        public void EndGame()
        {
            this.gameOver = true;
        }
        private void PrepareMyTable()
        {
            //pune jucatorul sa aleaga unde pune piesele
            //buton ready
        }
    }
}
