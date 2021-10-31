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


        public Game(String player)
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
        public TypesBoard getTypeFromTable(int nrbut) 
        {
            return MyBoardBullets[nrbut];
        }
        public void setTypeMyTable(int nrbut,TypesBoard type)
        {
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
        public bool isInPrepareMode() { return prepare; }
        public bool isOpponentReady() { return opponentready; }
        public void setOpponentReady(bool x){ this.opponentready = x;}
        
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
