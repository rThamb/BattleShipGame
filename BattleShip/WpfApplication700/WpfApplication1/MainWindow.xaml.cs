using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        System.Media.SoundPlayer mPlayer = new System.Media.SoundPlayer();
        Button[,] OPPOENENTSBOARD = new Button[10, 10];
        Button[,] PLAYERSBOARD = new Button[10, 10];

        bool[,] playersMoves = new bool[10, 10];

        //battleShip player

        BattleShipAI computerPlayer = null;

        BattleShipPlayer humanPlayer = null;

        Button currentBoat = null;
        int boatSize = 0;

        //counter for baot
        private int PlayersSinks = 0;
        private int ComputersSinks = 0;


        //keeps track of the number of boats placed
        private int boatsPlaced = 0;

        private String playerName = null;


        // Database and playerRecord variables

        private PlayerData PlayerInfo = null;     

        private List<PlayerData> database = null;
        String databaseFilePath = "Saves\\PlayerHistory.txt";


        //diff
        private int diff;

        //music
        MediaPlayer mplayer = new MediaPlayer();

   
        

        public MainWindow()
        {

            InitializeComponent();

            this.mplayer = new MediaPlayer();
            mplayer.Open(new Uri("../../Ocean.mp3", UriKind.RelativeOrAbsolute));
            mplayer.Position = TimeSpan.Zero;
            mplayer.Play();

            OpponentBoardSetUp();
            PlayerBoardArraySetUp();

            this.humanPlayer = new BattleShipPlayer();

            showBoatDock();
            hideComputersBoard();

            this.database = (List<PlayerData>)loadDatabase(databaseFilePath);

            
        }


        
        /* --------------------------------------------------------------------------------------------------------
         * 
         *  Methods are in charge of setting the game up
         *  
         * --------------------------------------------------------------------------------------------------------
         */

        public void setDiff(int diff)
        {
            this.diff = diff;
            this.computerPlayer = new BattleShipAI(diff);
        }

        public void setPlayerName(String name)
        {
            this.playerName = name;

            label.Content = name;

            CheckIfReturningPlayer();
        }

        


        /*
         *  Checks if the player is a returning player
         * 
         */
        private void CheckIfReturningPlayer()
        {
            //check if the name is in the database
            PlayerData person = new PlayerData(playerName);

            int index = checkIfInDatabase(person);

           
            if (index > 0)
            {
                this.PlayerInfo = database[index];

                MessageBox.Show("Welcome Back " + PlayerInfo.Name);

                PlayersWinsLbl.Content = PlayerInfo.Wins;
                PLayersLosesLbl.Content = PlayerInfo.Loses;
            }
            else
            {
              
                database.Add(person);

                this.PlayerInfo = person;

                PlayersWinsLbl.Content = 0;
                PLayersLosesLbl.Content = 0;

            }

        }

        /*
         * Wil search the database for the players name  
         */
        private int checkIfInDatabase(PlayerData aPlayer)
        {
            database.Sort();

            return database.BinarySearch(aPlayer);
        }




        /*
         * Displays the players ship dock 
         */
        private void showBoatDock()
        {
            boat5.Content = FindResource("airCraftCarrierImg");
            boat4.Content = FindResource("battleShipImg");
            boat3.Content = FindResource("submarineImg");
            boat3_2.Content = FindResource("cruiserImg");
            boat2.Content = FindResource("patrolBoatImg");
         

        
        }

        private void hideComputersBoard()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    OPPOENENTSBOARD[i, j].Visibility = System.Windows.Visibility.Collapsed; 
                }
            }
        }

        private void showComputersBoard()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    OPPOENENTSBOARD[i, j].Visibility = System.Windows.Visibility.Visible;
                 
                }
            }
            EBoat5.Content = FindResource("airCraftCarrierImg");
            EBoat4.Content = FindResource("battleShipImg");
            EBoat3.Content = FindResource("submarineImg");
            EBoat3_2.Content = FindResource("cruiserImg");
            EBoat2.Content = FindResource("patrolBoatImg");

            label1.Visibility = System.Windows.Visibility.Hidden;
            EBoat5.Visibility = System.Windows.Visibility.Visible;
            EBoat4.Visibility = System.Windows.Visibility.Visible;
            EBoat3.Visibility = System.Windows.Visibility.Visible;
            EBoat3_2.Visibility = System.Windows.Visibility.Visible;
            EBoat2.Visibility = System.Windows.Visibility.Visible;
            
        }





        /*
         * Assigns the opponent buttons into an array
         */
        private void OpponentBoardSetUp()
        {

            Button[,] opponentsButtons = new Button[10, 10]
            { 
                {OpponentTile00, OpponentTile01, OpponentTile02, OpponentTile03, OpponentTile04,  
                 OpponentTile05, OpponentTile06, OpponentTile07, OpponentTile08, OpponentTile09},

                 {OpponentTile10, OpponentTile11, OpponentTile12, OpponentTile13, OpponentTile14,  
                 OpponentTile15, OpponentTile16, OpponentTile17, OpponentTile18, OpponentTile19},

                 {OpponentTile20, OpponentTile21, OpponentTile22, OpponentTile23, OpponentTile24,  
                 OpponentTile25, OpponentTile26, OpponentTile27, OpponentTile28, OpponentTile29},

                 {OpponentTile30, OpponentTile31, OpponentTile32, OpponentTile33, OpponentTile34,  
                 OpponentTile35, OpponentTile36, OpponentTile37, OpponentTile38, OpponentTile39},

                 {OpponentTile40, OpponentTile41, OpponentTile42, OpponentTile43, OpponentTile44,  
                 OpponentTile45, OpponentTile46, OpponentTile47, OpponentTile48, OpponentTile49},

                 {OpponentTile50, OpponentTile51, OpponentTile52, OpponentTile53, OpponentTile54,  
                 OpponentTile55, OpponentTile56, OpponentTile57, OpponentTile58, OpponentTile59},

                 {OpponentTile60, OpponentTile61, OpponentTile62, OpponentTile63, OpponentTile64,  
                 OpponentTile65, OpponentTile66, OpponentTile67, OpponentTile68, OpponentTile69},

                 {OpponentTile70, OpponentTile71, OpponentTile72, OpponentTile73, OpponentTile74,  
                 OpponentTile75, OpponentTile76, OpponentTile77, OpponentTile78, OpponentTile79},

                 {OpponentTile80, OpponentTile81, OpponentTile82, OpponentTile83, OpponentTile84,  
                 OpponentTile85, OpponentTile86, OpponentTile87, OpponentTile88, OpponentTile89},

                 {OpponentTile90, OpponentTile91, OpponentTile92, OpponentTile93, OpponentTile94,  
                 OpponentTile95, OpponentTile96, OpponentTile97, OpponentTile98, OpponentTile99}    
                                  
            };

            this.OPPOENENTSBOARD = opponentsButtons;
        }// close Method

        /*
         * Assigns the opponent buttons into an array
         */
        private void PlayerBoardArraySetUp()
        {

            Button[,] board = new Button[10, 10]
            { 
                {PlayerTile00, PlayerTile01, PlayerTile02, PlayerTile03, PlayerTile04,  
                 PlayerTile05, PlayerTile06, PlayerTile07, PlayerTile08, PlayerTile09},

                 {PlayerTile10, PlayerTile11, PlayerTile12, PlayerTile13, PlayerTile14,  
                 PlayerTile15, PlayerTile16, PlayerTile17, PlayerTile18, PlayerTile19},

                 {PlayerTile20, PlayerTile21, PlayerTile22, PlayerTile23, PlayerTile24,  
                 PlayerTile25, PlayerTile26, PlayerTile27, PlayerTile28, PlayerTile29},

                 {PlayerTile30, PlayerTile31, PlayerTile32, PlayerTile33, PlayerTile34,  
                 PlayerTile35, PlayerTile36, PlayerTile37, PlayerTile38, PlayerTile39},

                 {PlayerTile40, PlayerTile41, PlayerTile42, PlayerTile43, PlayerTile44,  
                 PlayerTile45, PlayerTile46, PlayerTile47, PlayerTile48, PlayerTile49},

                 {PlayerTile50, PlayerTile51, PlayerTile52, PlayerTile53, PlayerTile54,  
                 PlayerTile55, PlayerTile56, PlayerTile57, PlayerTile58, PlayerTile59},

                 {PlayerTile60, PlayerTile61, PlayerTile62, PlayerTile63, PlayerTile64,  
                 PlayerTile65, PlayerTile66, PlayerTile67, PlayerTile68, PlayerTile69},

                 {PlayerTile70, PlayerTile71, PlayerTile72, PlayerTile73, PlayerTile74,  
                 PlayerTile75, PlayerTile76, PlayerTile77, PlayerTile78, PlayerTile79},

                 {PlayerTile80, PlayerTile81, PlayerTile82, PlayerTile83, PlayerTile84,  
                 PlayerTile85, PlayerTile86, PlayerTile87, PlayerTile88, PlayerTile89},

                 {PlayerTile90, PlayerTile91, PlayerTile92, PlayerTile93, PlayerTile94,  
                 PlayerTile95, PlayerTile96, PlayerTile97, PlayerTile98, PlayerTile99}    
                                  
            };

            this.PLAYERSBOARD = board;
        }// close Method







        /* --------------------------------------------------------------------------------------------------------
         * 
         *  Methods are used to interact with the game
         *  
         * --------------------------------------------------------------------------------------------------------
         */ 

        private void PlayersMove(object sender, RoutedEventArgs e)
        {
            Button aButton = (Button)sender;

            String coor = aButton.Name;

            int xCoor = Convert.ToInt32(coor[13] + "");

            int yCoor = Convert.ToInt32(coor[12] + "");

            

            if (!playersMoves[yCoor, xCoor])
            {

                playersMoves[yCoor, xCoor] = true;



                bool hit = computerPlayer.isHit(xCoor, yCoor);

                if (hit)
                {
                    aButton.Content = FindResource("Hit");
                   
                }
                else
                {
                    aButton.Content = FindResource("Miss");
                }
                aButton.Style = FindResource("MyButton2") as Style;
                aButton.IsEnabled = false;


                //********* AIs move *************************************************

                String aiCoor = computerPlayer.aiMove();

                computerPlayer.didMoveSinkBoat(false);

                int xAI = Convert.ToInt32(aiCoor[0] + "");
                int yAI = Convert.ToInt32(aiCoor[2] + "");


                //change the boards image
                if (humanPlayer.isHit(xAI, yAI))
                {   

                    PLAYERSBOARD[yAI, xAI].Content = FindResource("Hit");
                    computerPlayer.didMoveHit(true);
                }
                else
                {
                    PLAYERSBOARD[yAI, xAI].Content = FindResource("Miss");
                    computerPlayer.didMoveHit(false);
                }


                // Check if someone has sunk a boat

                //check if player sunk
                int shipSank = computerPlayer.isBoatSunk();

                if (shipSank != 0) // return 5 air 4 battle 3 cruiser 2 patrol 1 sub
                {
                    MediaPlayer mplayer = new MediaPlayer();
                    mplayer.Open(new Uri("../../explosion.mp3", UriKind.RelativeOrAbsolute));
                    
                    switch (shipSank)
                    {
                   
                        case 1:
                    
                            EBoat3.Content = FindResource("submarineS");                          
                            break;

                        case 2:
                         
                            EBoat2.Content = FindResource("patrolBoatS");
                            break;

                        case 3:
                        
                            EBoat3_2.Content = FindResource("cruiserS");
                            break;

                        case 4:
                         
                            EBoat4.Content = FindResource("battleShipS");

                            break;

                        case 5:
                          
                            EBoat5.Content = FindResource("airCraftCarrierS");
                            break;
                    }
                    mplayer.Play();
                    this.PlayersSinks++;
                }
                
                //check if AI sunk
                if (humanPlayer.isBoatSunk() != 0)
                {
                  
                   computerPlayer.didMoveSinkBoat(true);
                    this.ComputersSinks++;
                    
                }

                // Check if someone has won the game
                bool gameEnd = false;

                if (PlayersSinks == 5)
                {
                    this.mplayer.Stop();
                    MediaPlayer mplayer2 = new MediaPlayer();
                    mplayer2.Open(new Uri("../../victory.mp3", UriKind.RelativeOrAbsolute));
                    mplayer2.Play();
                    MessageBox.Show("You win");
   

                    PlayerInfo.incPlayersWins();

                    gameEnd = true;
                   
                }

                else if (ComputersSinks == 5)
                {
                    this.mplayer.Stop();
                    MediaPlayer mplayer2 = new MediaPlayer();
                    mplayer2.Open(new Uri("../../Defeat.wav", UriKind.RelativeOrAbsolute));
                    mplayer2.Play();
                    MessageBox.Show("You Lose");
                  
                    PlayerInfo.incPlayersLoses();

                    gameEnd = true;
                }

                if (gameEnd)
                {
                    saveDatabase(databaseFilePath, database);
                    
                    if (MessageBox.Show("Would you like to play again?", "Game Over", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                    {
                       
                        MainMenu menu = new MainMenu();       
                        
                        
                        menu.Show();
                        this.Close();

                    }
                    else
                    {

                        //write players state before 

 
                        MainWindow newGame = new MainWindow();

                        newGame.setDiff(this.diff);
                        newGame.setPlayerName(this.playerName);

                        this.Close();
                        newGame.Show();

                       
                    }
                }
            }
            else
                MessageBox.Show("Move already made");


                  
            
        }


        /* ---------------------------------------------------------------------------------------------------------------------------------------------------
         * 
         *  Method to set up the player field
         * 
         * ---------------------------------------------------------------------------------------------------------------------------------------------------
         */



        private void SelectBoatToPlace(object sender, RoutedEventArgs e)
        {
         
            currentBoat = (Button)sender;

            this.boatSize = Convert.ToInt32(currentBoat.Name[4] + "");

        }

        private void PlayerPlaceBoats(object sender, RoutedEventArgs e)
        {
            Button coordinate = (Button)sender;
            String coor = coordinate.Name;
        
            int CommaIndex = coor.IndexOf(',');

            int xCoor = Convert.ToInt32(coor[11] + "");
            int yCoor = Convert.ToInt32(coor[10] + "");

            if (isBoatPlaced(boatSize))
            {


                int placement = 0;

                if (VerticalOrient.IsChecked == true)
                    placement = 2;

                else
                    placement = 1;


                try
                {
                    this.humanPlayer.placeBoat(boatSize, xCoor, yCoor, placement);

                    this.currentBoat.Visibility = Visibility.Hidden;
                    this.currentBoat.IsEnabled = false;
                    

                    showPlayersBoat(boatSize, xCoor, yCoor, placement);

                    boatsPlaced++;

                }
                catch (Exception iae)
                {
                    System.Media.SystemSounds.Asterisk.Play();
                }


            }
            

            
        }

        private bool isBoatPlaced(int size)
        {
            bool placed = false;

            switch (size)
            {
                case 2:
                    placed = boat2.IsEnabled;
                    break;

                case 3:
                    if (this.currentBoat.Name.Equals("boat3_2"))
                    {
                       
                        placed = boat3_2.IsEnabled;
                    }
                    else{ 

                        placed = boat3.IsEnabled;
            }
                    break;

                case 4:
                    placed = boat4.IsEnabled;
                    break;

                case 5:
                    placed = boat5.IsEnabled;
                    break;
            }

            return placed;

       
        }


        private void showPlayersBoat(int boatSize, int xCoor, int yCoor, int ori)
        {
            if (ori == 1)
            {
                for (int i = 0; i < boatSize; i++)
                {
                    if (i == 0)

                        PLAYERSBOARD[yCoor, xCoor + i].Background = new ImageBrush(new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "FrontHoriBoat.png")));
                    else if (i == boatSize - 1)
                        PLAYERSBOARD[yCoor, xCoor + i].Background = new ImageBrush(new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "rearHoriBoat.png")));

                    else
                        PLAYERSBOARD[yCoor, xCoor + i].Background = new ImageBrush(new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "middleHoriBoat.png")));

                }
            }
            else
            {
                for (int i = 0; i < boatSize; i++)
                {

                    if (i == 0)

                        PLAYERSBOARD[yCoor + i, xCoor ].Background = new ImageBrush(new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "FrontVertiBoat.png")));
                    else if (i == boatSize - 1)
                        PLAYERSBOARD[yCoor + i, xCoor ].Background = new ImageBrush(new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "rearVertiBoat.png")));

                    else
                        PLAYERSBOARD[yCoor + i, xCoor ].Background = new ImageBrush(new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "middleVertiBoat.png")));

                }
            }
        }


        /* ---------------------------------------------------------------------------------------------------
         * Methods used for File IO , will be written in bits 
         * 
         * --------------------------------------------------------------------------------------------------
         * */

        /*
          * Will get the list of playerData and write it in the file
          * 
          */
        private void saveDatabase(string filePath, Object objectToWrite, bool append = false)
        {
            if (File.Exists(filePath))
            {
                using (Stream stream = File.Open(filePath, append ? FileMode.Append : FileMode.Create))
                {
                    var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    binaryFormatter.Serialize(stream, objectToWrite);
                }

            }
            else
            {
                MessageBox.Show("file doesnt exist");
            }

        }

        /*
         *  Will load the player Data  
         * 
         */
        public static Object loadDatabase(string filePath)
        {
            if (File.Exists(filePath))
            {

                using (Stream stream = File.Open(filePath, FileMode.Open))
                {
                    var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    return binaryFormatter.Deserialize(stream);
                }

            }
            else
            {
                MessageBox.Show("No file found");
                return new List<PlayerData>();
            }
        }

        /*
         * Begins the game
         * */
        private void start_Click(object sender, RoutedEventArgs e)
        {
            if (boatsPlaced == 5)
            {
                showComputersBoard();

                MessageBox.Show("GOOD LUCK");
                
                start.Visibility = System.Windows.Visibility.Hidden;
            }
            else
            {
                MessageBox.Show("Place all the boats to start the game");
            }
        }

        /*
         * Resets the board 
         */
        private void RestartGame(object sender, RoutedEventArgs e)
        {
            MainWindow newGame = new MainWindow();


            this.mplayer.Stop();

            newGame.setDiff(this.diff);
            newGame.setPlayerName(this.playerName);

            this.Close();
            newGame.Show();
        }

        private void GoToMainMenu(object sender, RoutedEventArgs e)
        {
            MainMenu menu = new MainMenu();
            this.mplayer.Stop();

            menu.Show();
            this.Close();
        }
    }// close the class
}//close the namespace
