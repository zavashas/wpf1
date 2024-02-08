using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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

namespace WpfApp3
{
    public partial class MainWindow : Window
    {
        private string nowPlayer;
        private Button[] buttons;
        private int[] gameState;
        private bool gameOver;

        public MainWindow()
        {
            InitializeComponent();
            InitializeGame();
            BlockButtons();
        }

        private void InitializeGame()
        {

            nowPlayer = "Крестики";
            buttons = new Button[] { Button1, Button2, Button3, Button4, Button5, Button6, Button7, Button8, Button9 };
            gameState = new int[9];
            gameOver = false;

            foreach (var button in buttons)
            {
                button.Content = "";
                button.IsEnabled = true;
            }

            if (nowPlayer == "Крестики")
                HodRobot();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!gameOver)
            {
                Button clickedButton = (Button)sender;
                int index = Array.IndexOf(buttons, clickedButton);

                if (gameState[index] == 0)
                {
                    Hod(index);
                    WhoWin();

                    if (!gameOver)
                        HodRobot();
                }
            }
        }


        private void Hod(int index)
        {
            if (nowPlayer == "Крестики")
            {
                buttons[index].Content = "X";
                gameState[index] = 1;
                nowPlayer = "Нолики";
            }
            else
            {
                buttons[index].Content = "O";
                gameState[index] = 2;
                nowPlayer = "Крестики";
            }

            buttons[index].IsEnabled = false;

            HodRobot();
        }


        private void HodRobot()
        {
            if (nowPlayer == "Нолики")
            {
                List<int> availableIndexes = new List<int>();
                for (int i = 0; i < 9; i++)
                {
                    if (gameState[i] == 0)
                    {
                        availableIndexes.Add(i);
                    }
                }

                if (availableIndexes.Count > 0)
                {
                    Random random = new Random();
                    int randomIndex = availableIndexes[random.Next(availableIndexes.Count)];
                    Hod(randomIndex);
                }
            }
        }



        private void WhoWin()
        {
            bool gameWon = false;

            for (int i = 0; i < 3; i++)
            {
                if (CheckLine(i * 3, i * 3 + 1, i * 3 + 2))
                {
                    gameWon = true;
                    break;
                }
                if (CheckLine(i, i + 3, i + 6))
                {
                    gameWon = true;
                    break;
                }
            }

            if (!gameWon && (CheckLine(0, 4, 8) || CheckLine(2, 4, 6)))
            {
                gameWon = true;
            }

            if (gameWon)
            {
                return;
            }

            if (!gameState.Contains(0))
            {
                MessageBox.Show("Ничья :0");
                gameOver = true;
                BlockButtons();
            }
        }



        private bool CheckLine(int a, int b, int c)
        {
            if (gameState[a] != 0 && gameState[a] == gameState[b] && gameState[b] == gameState[c])
            {
                MessageWinner(gameState[a]);
                return true;
            }
            return false;
        }

        private void MessageWinner(int player)
        {
            string winner = (player == 1) ? "Крестики" : "Нолики";
            MessageBox.Show($"Победили {winner}!!!");
            gameOver = true;
            BlockButtons();


            nowPlayer = (player == 1) ? "Нолики" : "Крестики";
        }



        private void BlockButtons()
        {
            foreach (var button in buttons)
            {
                button.IsEnabled = false;
            }
        }

        private void button10_Click(object sender, RoutedEventArgs e)
        {
            InitializeGame();

        }
    }
}