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
        bool chekerJump;
        Button previousButton;
        Button currentButton;
        Color temp;

        const int sizeOfButton = 50;
        const int fieldSize = 8;
        static int[,] field = new int[fieldSize, fieldSize]{
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
            blackChecker = new Bitmap(new Bitmap(@"C:\Users\sub7z\source\repos\Corners_WinForms\Corners_WinForms\png\black.png"), new Size(sizeOfButton-8, sizeOfButton-8));
            whiteChecker = new Bitmap(new Bitmap(@"C:\Users\sub7z\source\repos\Corners_WinForms\Corners_WinForms\png\white.png"), new Size(sizeOfButton - 8, sizeOfButton - 8));
            CreateField();
            previousButton = buttonField[0, 0];
            player = 0;
            chekerJump = false;

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
                    button.Click += ButtonOnClick;

                    if((i % 2 == 1 && j % 2 == 0) || (i % 2 == 0 && j % 2 == 1))
                        button.BackColor = Color.DarkRed;
                    else
                        button.BackColor = Color.Ivory;

                    if (field[i, j] == 1)
                        button.Image = blackChecker;
                    else if (field[i, j] == 0)
                        button.Image = whiteChecker;
                    
                    this.Controls.Add(button);
                    buttonField[i, j] = button;
                    left += button.Height + 2;
                }
                top += sizeOfButton + 2;
                left = 25;
            }
        }

        private void ButtonOnClick(object sender, EventArgs eventArgs)
        {
            currentButton = (sender as Button);
            int i = currentButton.Name[0] - '0';
            int j = currentButton.Name[1] - '0';
            previousButton.BackColor = temp;
            temp = currentButton.BackColor;

            if (field[i, j] != 2 && field[i, j] == player)
            {
                currentButton.BackColor = Color.RoyalBlue;
                chekerJump = true;
            }
            else if (field[i, j] == 2 && chekerJump)
            {
                //this.Text = previousButton.Name + " " + currentButton.Name + " ";
                this.Text = Convert.ToString(field[previousButton.Name[0] - '0', previousButton.Name[1] - '0']) + Convert.ToString(field[i, j]) + " ";

                Image temp = currentButton.Image;
                currentButton.Image = previousButton.Image;
                previousButton.Image = temp;

                field[i, j] = player;
                field[previousButton.Name[0] - '0', previousButton.Name[1] - '0'] = 2;

                currentButton.Name = previousButton.Name;
                previousButton.Name = Convert.ToString(i) + Convert.ToString(j);

                //this.Text += previousButton.Name + " " + currentButton.Name;
                this.Text += Convert.ToString(field[previousButton.Name[0] - '0', previousButton.Name[1] - '0']) + Convert.ToString(field[i, j]);
                chekerJump = false;
            }
            previousButton = currentButton;
        }
        
    }
}
