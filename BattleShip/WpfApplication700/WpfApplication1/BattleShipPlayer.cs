using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    class BattleShipPlayer
    {

        private int[,] gameBoard = null;


        int AirCraftHitCounter = 0;
        int BattleShipHitCounter = 0;
        int SubmarineHitCounter = 0;
        int CruiserHitCounter = 0;
        int DestroyerHitCounter = 0;

        private int ShipsSunk = 0;


        public BattleShipPlayer()
        {
            this.gameBoard = new int[10, 10];
        }


        public int[,] GameBoard
        {
            get { return gameBoard; }

        }



        public bool placeBoat(int shipSize, int xGiven, int yGiven, int orient)
        {

            // 1 = horizontal 
            // 2 = vertical

            int orientation = orient;


            int y = yGiven;
            int x = xGiven;

            Console.WriteLine("In the place method ");

            if (orientation == 1)
            {
                //check the points that follow to see if their empty based on ori.
                if (x + shipSize < 11)
                {
                    for (int space = 0; space < shipSize; space++)
                    {
                        Console.WriteLine("In loop ");
                        if (gameBoard[y, x + space] != 0)
                            throw new Exception("Doesn't fit");
                    }

                }
                else
                    throw new Exception("Doesn't fit");
            }

            else
            {
                if (y + shipSize < 11)
                {
                    for (int space = 0; space < shipSize; space++)
                    {
                        Console.WriteLine("In loop ");
                        if (gameBoard[y + space, x] != 0)
                            throw new Exception("Doesn't fit");
                    }

                }
                else
                    throw new Exception("Doesn't fit");
            }




            if (orientation == 1)
            {
                //assign
                for (int space = 0; space < shipSize; space++)
                {
                    gameBoard[y, x + space] = shipSize;

                }
            }
            else
            {
                //assign
                for (int space = 0; space < shipSize; space++)
                {
                    gameBoard[y + space, x] = shipSize;

                }
            }


            return true;
        }// close placeBoat


        public bool isHit(int x, int y)
        {
            if (gameBoard[y, x] != 0)
            {
                //change the point if theres a hit 
                incHit(gameBoard[y, x]);
                gameBoard[y, x] = 0;
                return true;
            }
            else
            {
                return false;
            }
        }


        /*
         * Keeps track of the hits for each boat 
         */
        private void incHit(int boatNumber)
        {
            switch (boatNumber)
            {
                case 1:
                    SubmarineHitCounter++;
                    break;

                case 2:
                    DestroyerHitCounter++;
                    break;

                case 3:
                    CruiserHitCounter++;
                    break;

                case 4:
                    BattleShipHitCounter++;
                    break;

                case 5:
                    AirCraftHitCounter++;
                    break;
            }

        }

        /*
         * Checks to see if any boat have been sunk
         * 
         * return boat number if sunk else 0
         */
        public int isBoatSunk()
        {
            if (AirCraftHitCounter == 5)
            {
                //set to zero is its executed once
                AirCraftHitCounter = 0;
                ShipsSunk++;
                return 5;
            }

            if (BattleShipHitCounter == 4)
            {
                BattleShipHitCounter = 0;
                ShipsSunk++;
                return 4;
            }

            if (SubmarineHitCounter == 3)
            {
                SubmarineHitCounter = 0;
                ShipsSunk++;
                return 1;
            }

            if (CruiserHitCounter == 3)
            {
                CruiserHitCounter = 0;
                ShipsSunk++;
                return 3;
            }

            if (DestroyerHitCounter == 2)
            {
                DestroyerHitCounter = 0;
                ShipsSunk++;
                return 2;
            }

            return 0;
        }

        /*
         * Checks to see if all the boat are sunk 
         */
        public bool isAllSunk()
        {

            if (ShipsSunk == 5)
                return true;

            else
                return false;
        }





        public override String ToString()
        {
            StringBuilder info = new StringBuilder();

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    info.Append(gameBoard[i, j] + " ");
                }

                info.Append("\n");
            }

            return info.ToString();
        }

    }
}
