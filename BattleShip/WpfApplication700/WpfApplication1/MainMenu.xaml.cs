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
using System.Windows.Shapes;
using System.IO;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {

        MediaPlayer mplayer = new MediaPlayer();

        public MainMenu()
        {
            InitializeComponent();

            mplayer.Open(new Uri("../../menuM.mp3", UriKind.RelativeOrAbsolute));
            mplayer.Play();
            
        }

        private void beginGame(object sender, RoutedEventArgs e)
        {
            Button mode = (Button)sender;

            String name = NameInputBox.Text;

            if (name.Equals("Please enter your name") || name.Equals("") || (name.IndexOf('*') != -1))
                MessageBox.Show("Please Enter a Valid Name");
            else
            {

                if (mode.Name.Equals("EasyGame"))
                {
                    MainWindow newGame = new MainWindow();

                    newGame.setDiff(0);

                    newGame.setPlayerName(name);

                    newGame.Show();
                    mplayer.Stop();

                    this.Hide();
                    MessageBox.Show("Welcome to BattleShips\n\nInstructions\n" +
                                      "Please click on a boat and proceed by clicking the board to place it");

                }
                else
                {
                    
                    MainWindow newGame = new MainWindow();

                    newGame.setDiff(1);

                    newGame.setPlayerName(name);

                    newGame.Show();
                    mplayer.Stop();

                    this.Hide();
                    MessageBox.Show("Welcome to BattleShips\n\nInstructions\n" +
                                      "Please click on a boat and proceed by clicking the board to place it");
                }

            }

        }

    }
}
