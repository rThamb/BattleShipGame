using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    class BattleShipAI
    {
        private int[,] gameBoard = new int[10, 10];
        private bool EasyAI = true;  

        private int LastXMove;
        private int LastYMove;
        private bool lastMoveHit = false;
        private bool[,] movesSelected = new bool[10, 10];
        private int ShipsSunk = 0;


        int AirCraftHitCounter = 0;
        int BattleShipHitCounter = 0;
        int SubmarineHitCounter = 0;
        int CruiserHitCounter = 0;
        int DestroyerHitCounter = 0;

        //jacobs stuff
        private int diff;
        private bool[,] shots = new bool[10, 10];
        private int strategy = 0;
        private bool wasAHit = false;
        private bool wasASink = false;
        private int[] lastShot = new int[2];
        private int[] firstHit = new int[2];
        private int[] hits = new int[4];
        private int totalHits;
        private int[] smartPlace = new int[2];



        public BattleShipAI(int diff)
        {
            setTheGameBoard();
            this.diff = diff; // 1 hard 0 easy
        }


        public int[,] GameBoard
        {
            get { return gameBoard; }

        }

        /*--------------------------------------------------------------------------------------------------------------------------------------------------------
         * 
         *          Methods below are in charge of setting the board for the AI  
         *          
         *  -----------------------------------------------------------------------------------------------------------------------------------------------------
         */

        /*
         * Will place all 5 boat on the field 
         * 
         */

        private void setTheGameBoard()
        {

            bool airCraftPlaced = false;
            bool battleshipPlaced = false;
            bool submarinePlaced = false;
            bool cruiserPlaced = false;
            bool destroyerPlaced = false;
            bool allPlaced = false;


            while (!allPlaced)
            {
                if (!airCraftPlaced)
                {
                    try
                    {
                        placeBoat(5);
                        airCraftPlaced = true;
                    }
                    catch (Exception e)
                    { }
                }// close if


                if (!battleshipPlaced)
                {
                    try
                    {
                        placeBoat(4);
                        battleshipPlaced = true;
                    }
                    catch (Exception e)
                    { }
                }// close if


                if (!submarinePlaced)
                {
                    try
                    {
                        placeBoat(3);
                        submarinePlaced = true;
                    }
                    catch (Exception e)
                    { }
                }// close if


                if (!cruiserPlaced)
                {
                    try
                    {
                        placeBoat(3);
                        cruiserPlaced = true;
                    }
                    catch (Exception e)
                    { }
                }// close if

                if (!destroyerPlaced)
                {
                    try
                    {
                        placeBoat(2);
                        destroyerPlaced = true;
                    }
                    catch (Exception e)
                    { }
                }// close if

                if (airCraftPlaced && battleshipPlaced && submarinePlaced && cruiserPlaced && destroyerPlaced)
                {
                    AssignUniqueValueForSubmarine();
                    allPlaced = true;
                }// close if


            }// close loop

        }



        /*
         * Places the boat on the board
         */
        private void placeBoat(int shipSize)
        {
            Random num = new Random();

            // 1 = horizontal 
            // 2 = vertical

            int orientation = num.Next(1, 3);
            Boolean valid = false;

            int validY = 0;
            int validX = 0;

            while (!valid)
            {
                //generate a point
                int y = num.Next(0, 10);
                int x = num.Next(0, 10);

                if (orientation == 1)
                {
                    //check the points that follow to see if their empty based on ori.
                    if (x + shipSize < 11)
                    {
                        for (int space = 0; space < shipSize; space++)
                        {

                            if (gameBoard[y, x + space] != 0)
                                throw new Exception("Doesn't fit");
                        }

                        valid = true;

                    }
                }
                else
                {
                    if (y + shipSize < 11)
                    {
                        for (int space = 0; space < shipSize; space++)
                        {
                            if (gameBoard[y + space, x] != 0)
                                throw new Exception("Doesn't fit");
                        }

                        valid = true;
                    }
                }

                validY = y;
                validX = x;

            }// close while


            if (orientation == 1)
            {
                //assign
                for (int space = 0; space < shipSize; space++)
                {
                    gameBoard[validY, validX + space] = shipSize;

                }
            }
            else
            {
                //assign
                for (int space = 0; space < shipSize; space++)
                {
                    gameBoard[validY + space, validX] = shipSize;

                }
            }

        }// close placeBoat


        /*
         * Will assign the unique value to the submarine piece in the gameboard   
         */
        private bool AssignUniqueValueForSubmarine()
        {
            bool verticalOri = false;
            for (int y = 0; y < gameBoard.Length; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    if (gameBoard[y, x] == 3)
                    {
                        if (x == 9) // near the edge therefore vertical orientation 
                        {
                            verticalOri = true;
                        }

                        if ((!verticalOri) && (gameBoard[y, x + 1] == 3) && (gameBoard[y, x + 2] == 3))
                        {
                            gameBoard[y, x] = 1;
                            gameBoard[y, x + 1] = 1;
                            gameBoard[y, x + 2] = 1;
                        }


                        else
                        {
                            gameBoard[y, x] = 1;
                            gameBoard[y + 1, x] = 1;
                            gameBoard[y + 2, x] = 1;
                        }

                        return true;

                    }
                }
            }

            return false;
        }




        /*--------------------------------------------------------------------------------------------------------------------------------------------------------
         * 
         *          Methods below are in charge of game functionalities, 
         * 
         *  -----------------------------------------------------------------------------------------------------------------------------------------------------
         */



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


        /*--------------------------------------------------------------------------------------------------------------------------------------------------------
         * 
         *          Methods are in charge of deciding the AI Moves according to the difficulty 
         * 
         *  -----------------------------------------------------------------------------------------------------------------------------------------------------
         */

        //************************************************* JACOBS STUFF


        /*
        * Sets true if the computers last move hit a piece  
        */
        public void didMoveHit(bool hit)
        {
            this.wasAHit = hit;
        }

        /*
         *  Informs the computer if he has sunk a ship
         */
        public void didMoveSinkBoat(bool hit)
        {
            Console.WriteLine("in didMoveSinkBoat hit is = " + hit);
            this.wasASink = hit;
        }


        public String aiMove()
        {
            Random r = new Random();
            int[] coords = new int[2];

            if (diff == 0)
            {
                bool invalid = true;
                do
                {
                    coords = randomMove();
                    if (!shots[coords[0], coords[1]])
                    {
                        invalid = false;
                    }
                } while (invalid);
            }

            else
            {

                //KILLER MOVES
                bool invalid = true;
                do
                {
                    coords = hardAI();
                    if (coords == null)
                    {
                        invalid = false;
                    }
                    else if (isPlaceValid(coords))
                    {
                        if (!shots[coords[0], coords[1]])
                            invalid = false;
                        Console.WriteLine("STATEGY\nCoords: " + coords[0] + "," + coords[1] + " Strat: " + strategy + " Hit: " + wasAHit);
                    }
                } while (invalid);

                //NORMAL MOVES
                invalid = true;
                if (coords == null)
                {
                    if(r.Next(0,2) > 0)
                    do
                    {
                        coords = smartMove();
                        if (coords == null)
                            invalid = false;
                        else if (isPlaceValid(coords))
                        {
                            if (!shots[coords[0], coords[1]])
                            {
                                invalid = false;
                            }
                            Console.WriteLine("SMART\nCoords: " + coords[0] + "," + coords[1] + " Hit: " + wasAHit);
                        }
                    } while (invalid);
                }

                //RANDOM MOVES
                invalid = true;
                if (coords == null)
                {
                    do
                    {
                        coords = randomMove();
                        if (!shots[coords[0], coords[1]])
                        {
                            invalid = false;
                        }
                        Console.WriteLine("RANDOM\nCoords: " + coords[0] + "," + coords[1] + " Hit: " + wasAHit);
                    } while (invalid);
                }
            }

            //Console.ReadLine();

            shotsFired++;
            lastShot[0] = coords[0];
            lastShot[1] = coords[1];
            shots[coords[0], coords[1]] = true;
            return coords[1] + "," + coords[0];//CHECK THIS!
        }

        private int shotsFired = 0;

        private bool overlap = false;

        private int[] hardAI()
        {
            int var = 1;
            if (overlap)
                var = 2;
            int[] coords = new int[2];
            if (!wasASink)
            {
                if (!wasAHit)
                {
                    if (totalHits == 0)
                        return null;
                    else
                        strategyChange();
                }
                else
                {
                    if (totalHits == 0)
                    {
                        firstHit[0] = lastShot[0];
                        firstHit[1] = lastShot[1];
                        strategy = 0;
                    }
                    else
                        hits[strategy]++;
                    totalHits++;
                    overlap = false;
                }

                //STRATEGIES WHEN IT IS A HIT
                if (strategy == 0)
                {
                    lastShot[1] -= var;
                    coords[0] = lastShot[0];
                    coords[1] = lastShot[1];
                    if (!isPlaceValid(coords))
                        strategyChange();
                }
                if (strategy == 1)
                {
                    lastShot[0] -= var;
                    coords[0] = lastShot[0];
                    coords[1] = lastShot[1];
                    if (!isPlaceValid(coords))
                        strategyChange();
                }
                if (strategy == 2)
                {
                    lastShot[1] += var;
                    coords[0] = lastShot[0];
                    coords[1] = lastShot[1];
                    if (!isPlaceValid(coords))
                        strategyChange();
                }
                if (strategy == 3)
                {
                    lastShot[0] += var;
                    coords[0] = lastShot[0];
                    coords[1] = lastShot[1];
                    if (!isPlaceValid(coords))
                        strategyChange();
                }
            }
            else
            {
                overlap = false;
                coords = null;
                wasASink = false;
                wasAHit = false;
                firstHit[0] = 10;
                firstHit[1] = 10;
                totalHits = 0;
                strategy = 0;
                hits = new int[4];
                return coords;
            }
            //Console.ReadLine();
            return coords;
        }

        private int[] lastRandom = new int[2];


        private int[] randomHardMove()
        {
            Random r = new Random();
            lastRandom[0]++;
            lastRandom[1]++;
            if (lastRandom[0] > 9)
                lastRandom[0] = r.Next(0, 9);
            if (lastRandom[1] > 9)
                lastRandom[1] = r.Next(0, 9);
            return lastRandom;

        }

        private int phase = 0;
        private int[] smartMove()
        {
            switch (phase)
            {
                case 0:
                    Console.WriteLine("+++++PHASE 1+++++");
                    if (smartPlace[0] > 9)
                    {
                        smartPlace[0] = 0;
                        smartPlace[1] = 9;
                        phase++;
                    }
                    else
                    {
                        smartPlace[0]++;
                        smartPlace[1]++;
                    }
                    break;

                case 1:
                    Console.WriteLine("+++++PHASE 2+++++");
                    if (!isPlaceValid(smartPlace))
                    {
                        smartPlace[0] = 0;
                        smartPlace[1] = 4;
                        phase++;
                    }
                    else
                    {
                        smartPlace[0]++;
                        smartPlace[1]--;
                    }
                    break;


                case 2:
                    Console.WriteLine("+++++PHASE 3+++++");
                    if (!isPlaceValid(smartPlace))
                    {
                        smartPlace[0] = 4;
                        smartPlace[1] = 0;
                        phase++;
                    }
                    else
                    {
                        smartPlace[0]++;
                        smartPlace[1]++;
                    }
                    break;
                case 3:
                    Console.WriteLine("+++++PHASE 4+++++");
                    if (!isPlaceValid(smartPlace))
                    {
                        smartPlace[0] = 0;
                        smartPlace[1] = 7;
                        phase++;
                    }
                    else
                    {
                        smartPlace[0]++;
                        smartPlace[1]++;
                    }
                    break;
                case 4:
                    Console.WriteLine("+++++PHASE 5+++++");
                    if (!isPlaceValid(smartPlace))
                    {
                        smartPlace[0] = 7;
                        smartPlace[1] = 0;
                        phase++;
                    }
                    else
                    {
                        smartPlace[0]++;
                        smartPlace[1]++;
                    }
                    break;
                case 5:
                    Console.WriteLine("+++++PHASE 6+++++");
                    if (!isPlaceValid(smartPlace))
                    {
                        smartPlace[0] = 0;
                        smartPlace[1] = 7;
                        phase++;
                    }
                    else
                    {
                        smartPlace[0]++;
                        smartPlace[1]++;
                    }
                    break;
                case 6:
                    Console.WriteLine("+++++PHASE 6+++++");
                    if (!isPlaceValid(smartPlace))
                    {
                        smartPlace[0] = 0;
                        smartPlace[1] = 7;
                        smartPlace = null;
                        phase++;
                    }
                    else
                    {
                        smartPlace[0]++;
                        smartPlace[1]++;
                    }
                    break;
                case 7:
                    smartPlace = null;
                    break;

            }
            return smartPlace;
        }

        private static bool isPlaceValid(int[] num)
        {
            if (num[0] > 9 || num[0] < 0)
                return false;
            if (num[1] > 9 || num[1] < 0)
                return false;
            return true;
        }

        private int[] randomMove()
        {
            int[] coords = new int[2];
            Random r = new Random();
            coords[0] = r.Next(0, 10);
            coords[1] = r.Next(0, 10);
            return coords;

        }

        private void strategyChange()
        {
            if (strategy >= 3)
            {
                strategy = 0;
                wasAHit = false;
                firstHit[0] = 10;
                firstHit[1] = 10;
                totalHits = 0;
                strategy = 0;
                hits = new int[4];
            }
            else if (strategy < 2)
            {
                if (hits[strategy] > 0)
                {
                    strategy += 2;
                    lastShot[0] = firstHit[0];
                    lastShot[1] = firstHit[1];
                }
                else
                {
                    strategy++;
                    lastShot[0] = firstHit[0];
                    lastShot[1] = firstHit[1];
                }
            }
            else
            {
                strategy++;
                lastShot[0] = firstHit[0];
                lastShot[1] = firstHit[1];
            }

            if (strategy > 3)
                Console.WriteLine("Error encountered: AI Strategies did not work");

           //To debug --Console.WriteLine("STRATEGY_CHANGE\nFirst Hit: " + firstHit[0] + "," + firstHit[1]);
            //Console.ReadLine();
        }


        //************************************************* JACOBS STUFF











        /* --------------------------------------------------------------
         *-------------------------------------------------------------
         */

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
