using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldBattle
{
    enum TypesBoard
    {
        Untouched, //nu e nimic-> si nici nu a fost incercata 
        Empty, //a fost testat si e gol
        Checked//a fost testat si e plin
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


        public Game(String player)
        {
            this.MyBoardBullets = new TypesBoard[DIM * DIM];
            this.YourBoardBullets = new TypesBoard[DIM * DIM];
            for (int i = 0; i < DIM * DIM; i++)
            {
                this.MyBoardBullets[i] = TypesBoard.Untouched;
                this.MyBoardBullets[i] = TypesBoard.Untouched;
            }
            this.player = player;
            this.gameOver = false;
            this.prepare = true;
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
        public String GetGameInfo() { return this.gameInfo; }
        public String GetRecentMove(){ return this.recentMove; }
        public bool isOver() { return gameOver; }
        public bool isTurn() { return turn; }
        public bool isInPrepareMode() { return prepare; }
        public void EndGame()
        {
            this.gameOver = true;
        }
        private void PrepareMyTable()
        {
            //pune jucatorul sa aleaga unde pune piesele
        }
    }
}
