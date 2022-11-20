using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Corners_WinForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Init();
        }

        Image blackChecker;
        Image whiteChecker;
        
        int player;
        bool mayJump;
        bool checkerJump;
        int[,] increment = {
            {0, -1, 1, 1 },
            {-1, 1, 1, -1 }
        };
        Button previousButton;
        Button currentButton;
        Color tempColor;
        Image tempImage;

        const int sizeOfButton = 50;
        const int fieldSize = 8;
        static int[,] field = new int[fieldSize, fieldSize] {
            {1,1,1,1,2,2,2,2},
            {1,1,1,1,2,2,2,2},
            {1,1,1,1,2,2,2,2},
            {2,2,2,2,2,2,2,2},
            {2,2,2,2,2,2,2,2},
            {2,2,2,2,0,0,0,0},
            {2,2,2,2,0,0,0,0},
            {2,2,2,2,0,0,0,0},
        };
        Button[,] buttonField = new Button[fieldSize, fieldSize];

        private void Init()
        {
            blackChecker = new Bitmap(new Bitmap(@"C:\Users\sub7z\source\repos\Corners_WinForms\Corners_WinForms\png\black.png"), new Size(sizeOfButton - 8, sizeOfButton - 8));
            whiteChecker = new Bitmap(new Bitmap(@"C:\Users\sub7z\source\repos\Corners_WinForms\Corners_WinForms\png\white.png"), new Size(sizeOfButton - 8, sizeOfButton - 8));
            CreateField();
            previousButton = buttonField[0, 0];
            player = 0;
            mayJump = false;
        }

        private void CreateField()
        {
            int top = 10;
            int left = 25;

            for (int i = 0; i < fieldSize; i++)
            {
                for (int j = 0; j < fieldSize; j++)
                {
                    Button button = new Button();
                    button.Left = left;
                    button.Top = top;
                    button.Width = sizeOfButton;
                    button.Height = sizeOfButton;
                    button.Name = Convert.ToString(i) + Convert.ToString(j);
                    
                    if ((i % 2 == 1 && j % 2 == 0) || (i % 2 == 0 && j % 2 == 1))
                        button.BackColor = Color.DarkRed;
                    else
                        button.BackColor = Color.Ivory;

                    if (field[i, j] == 1)
                        button.Image = blackChecker;
                    else if (field[i, j] == 0)
                        button.Image = whiteChecker;

                    button.Click += ButtonOnClick;

                    Controls.Add(button);
                    buttonField[i, j] = button;

                    left += button.Height + 2;
                }
                top += sizeOfButton + 2;
                left = 25;
            }
        }
        
        private void DrawField()
        {
            for (int i = 0; i < fieldSize; i++)
            {
                for (int j = 0; j < fieldSize; j++)
                {
                    if ((i % 2 == 1 && j % 2 == 0) || (i % 2 == 0 && j % 2 == 1))
                        buttonField[i, j].BackColor = Color.DarkRed;
                    else
                        buttonField[i, j].BackColor = Color.Ivory;
                }
            }
        }

        private void ButtonOnClick(object sender, EventArgs eventArgs)
        {
            DrawField();

            checkerJump = false;

            currentButton = (sender as Button);

            int curr_i = currentButton.Name[0] - '0';
            int curr_j = currentButton.Name[1] - '0';

            int prev_i = previousButton.Name[0] - '0';
            int prev_j = previousButton.Name[1] - '0';

            previousButton.BackColor = tempColor;
            tempColor = currentButton.BackColor;

            if (field[curr_i, curr_j] != 2 && field[curr_i, curr_j] == player)
            {
                currentButton.BackColor = Color.RoyalBlue;
                CheckPossibleMoves(curr_i, curr_j);
                mayJump = true;
            }
            else if (field[curr_i, curr_j] == 2 && mayJump && field[prev_i, prev_j] == player)
            {
                tempImage = currentButton.Image;
                currentButton.Image = previousButton.Image;
                previousButton.Image = tempImage;

                field[curr_i, curr_j] = player;
                field[prev_i, prev_j] = 2;

                mayJump = false;

                // field checks
                label1.Text = "";
                for (int ik = 0; ik < fieldSize; ik++)
                {
                    for (int jk = 0; jk < fieldSize; jk++)
                    {
                        label1.Text += field[ik, jk] + " ";
                    }
                    label1.Text += '\n';
                }
                player = (player == 0) ? 1 : 0; 
            }
            previousButton = currentButton;
        }

        private void CheckPossibleMoves(int curr_i, int curr_j)
        {
            checkerJump = !checkerJump;
            int neighbor_i = curr_i;
            int neighbor_j = curr_j;
            label1.Text = "";
            for (int i = 0; i < 4; i++)
            {
                neighbor_i += increment[0, i];
                neighbor_j += increment[1, i];

                try
                {
                    if (field[neighbor_i, neighbor_j] == 2)
                        buttonField[neighbor_i, neighbor_j].BackColor = Color.LimeGreen;
                    else if (field[neighbor_i, neighbor_j] != 2 && checkerJump)
                    {
                        if (i == 0 || i == 2)
                            CheckPossibleJumps(neighbor_i, neighbor_j + increment[1, i]);
                        else
                            CheckPossibleJumps(neighbor_i + increment[0, i], neighbor_j);
                    }
                }
                catch { }
            }/*
            {0, -1, 1, 1 },
            {-1, 1, 1, -1 }
            */
        }

        

        private void CheckPossibleJumps(int curr_i, int curr_j)
        {
            label1.Text += Convert.ToString(curr_i) + Convert.ToString(curr_j) + " ";
            if (field[curr_i, curr_j] == 2)
                buttonField[curr_i, curr_j].BackColor = Color.LimeGreen;
        }

    }
}
 