using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake_AB
{
    public partial class frmSnake : Form
    {
        Random rand;
        enum GameBoardFields
        {
            Free, 
            Snake,
            Bonus,
        };

        enum Directions
        {
            Up,
            Down,
            Left,
            Right
        };

        struct SnakeCoordinates
        {
            public int x;
            public int y;
        }

        GameBoardFields[,] gameBoardField;
        SnakeCoordinates[] snakeXY;
        int snakeLength;
        Directions directions;
        Graphics g;
        public frmSnake()
        {
            InitializeComponent();
            gameBoardField = new GameBoardFields[11, 11];
            snakeXY = new SnakeCoordinates[100];
            rand = new Random();
        }

        private void frmSnake_Load(object sender, EventArgs e)
        {
            picGameBoard.Image = new Bitmap(420, 420);
            g = Graphics.FromImage(picGameBoard.Image);
            g.Clear(Color.White);

            for (int i = 1; i <= 10; i++) 
            {
                g.DrawImage(imgList.Images[4], 0, i * 35);
                g.DrawImage(imgList.Images[4], 385, i * 35);
            }

            snakeXY[0].x = 5;
            snakeXY[0].y = 5;
            snakeXY[1].x = 5;
            snakeXY[1].y = 6;
            snakeXY[2].x = 5;
            snakeXY[2].y = 7;

            g.DrawImage(imgList.Images[6], 5 * 35, 5 * 35); //head
            g.DrawImage(imgList.Images[5], 5 * 35, 6 * 35); //first body part 
            g.DrawImage(imgList.Images[5], 5 * 35, 7 * 35); //second body part

            gameBoardField[5, 5] = GameBoardFields.Snake;
            gameBoardField[5, 6] = GameBoardFields.Snake;
            gameBoardField[5, 7] = GameBoardFields.Snake;

        }

    }
}
