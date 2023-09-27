using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake_AB
{
    public partial class frmSnake : Form
    {
        Random rand;
        enum GameBoardFields  //Énumération des cases du jeu
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

        struct SnakeCoordinates //Structure qui stocke les coordonées du serpent
        {
            public int x;
            public int y;
        }

        GameBoardFields[,] gameBoardField; //Tableau qui represente le plateau de jeu
        SnakeCoordinates[] snakeXY; //Coordonées du serpent
        int snakeLength;  //Taille du serpent
        Directions direction; // Direction actuelle du serpent
        Graphics g;  // Objet Graphics pour dessiner sur la fenêtre

        public frmSnake() 
        {
            InitializeComponent();
            gameBoardField = new GameBoardFields[11, 11];
            snakeXY = new SnakeCoordinates[100];
            rand = new Random();
        }

        private void frmSnake_Load(object sender, EventArgs e) //Initialise fenetre du jeu/plateau et dessine le serpent et les murs 
        {
            picGameBoard.Image = new Bitmap(420, 420);
            g = Graphics.FromImage(picGameBoard.Image);
            g.Clear(Color.White);

            for (int i = 1; i <= 10; i++) //Murs du hauts et du bas
            {
                g.DrawImage(imgList.Images[4], i * 35, 0);
                g.DrawImage(imgList.Images[4], i * 35, 385);
            }

            for (int i = 1; i <= 11; i++) //Murs de droite et de gauche
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

            g.DrawImage(imgList.Images[6], 5 * 35, 5 * 35); //Tête
            g.DrawImage(imgList.Images[5], 5 * 35, 6 * 35); //Premiere partie du corps 
            g.DrawImage(imgList.Images[5], 5 * 35, 7 * 35); //Seconde partie du corps

            gameBoardField[5, 5] = GameBoardFields.Snake;
            gameBoardField[5, 6] = GameBoardFields.Snake;
            gameBoardField[5, 7] = GameBoardFields.Snake;


            direction = Directions.Up;
            snakeLength = 3;

            for (int i = 0; i < 4; i++) 
            {
                Bonus();
            }
        }

        private void Bonus()  //fonction qui genere aleatoirement un bonus sur le plateau
        {
            int x, y;
            var imgIndex = rand.Next(0, 4);

            do
            {
                x = rand.Next(1, 10);
                y = rand.Next(1, 10);
            }
            while (gameBoardField[x, y] != GameBoardFields.Free);

            gameBoardField[x, y] = GameBoardFields.Bonus;
            g.DrawImage(imgList.Images[imgIndex], x * 35, y * 35);
        }

        private void frmSnake_KeyDown(object sender, KeyEventArgs e)  //Gestion des deplacements du serpent
        {
            switch(e.KeyCode)
            {
                case Keys.Up:
                    direction = Directions.Up;
                    break;
                case Keys.Down:
                    direction = Directions.Down;
                    break;
                case Keys.Left:
                    direction = Directions.Left;
                    break;
                case Keys.Right:
                    direction = Directions.Right;
                    break;
            }

        }

        private void GameOver()  //Gestion du message lors de defaite
        {
            timer.Enabled= false;
            MessageBox.Show("GAME OVER LOOSER");
        }

        private void Timer_Tick(object sender, EventArgs e) //Logique du jeu


        {
            //Efface la derniere position du serpent
            g.FillRectangle(Brushes.White, snakeXY[snakeLength - 1].x * 35, snakeXY[snakeLength - 1].y * 35, 35, 35);
            gameBoardField[snakeXY[snakeLength - 1].x, snakeXY[snakeLength - 1].y] = GameBoardFields.Free;

            //MaJ du corps du serpent suivant la tete
            for(int i = snakeLength; i>= 1; i--) 
            {
                snakeXY[i].x = snakeXY[i-1].x;
                snakeXY[i].y = snakeXY[i-1].y;
            }

            g.DrawImage(imgList.Images[5], snakeXY[0].x * 35, snakeXY[0].y * 35);

            //MaJ tete du serpent par rapport a la direction actuel
            switch (direction)
            {
                case Directions.Up:
                    snakeXY[0].y = snakeXY[0].y - 1;
                    break;
                case Directions.Down:
                    snakeXY[0].y = snakeXY[0].y+ 1;
                    break;
                case Directions.Left:
                    snakeXY[0].x = snakeXY[0].x - 1;
                    break;
                case Directions.Right:
                    snakeXY[0].x = snakeXY[0].x + 1;
                    break;
            }


            //Check si le serpent a fais une collision, et si oui affiche GameOver
            if (snakeXY[0].x < 1 || snakeXY[0].x > 10 || snakeXY[0].y < 0 || snakeXY[0].y > 10)
            {
                GameOver();
                picGameBoard.Refresh();
                return;
            }

            //Check si le serpent a fais une collision, et si oui affiche GameOver
            if (gameBoardField[snakeXY[0].x,snakeXY[0].y] == GameBoardFields.Snake) 
            {
                GameOver();
                picGameBoard.Refresh();
                return;
            }


            //Check si le serpent a manger un bonus
            if (gameBoardField[snakeXY[0].x, snakeXY[0].y] == GameBoardFields.Bonus)
            {
                //nouvelle tete
                g.DrawImage(imgList.Images[5], snakeXY[snakeLength].x * 35, snakeXY[snakeLength].y * 35);

                //MaJ du tableau pour les coordonées du serpent
                gameBoardField[snakeXY[snakeLength].x, snakeXY[snakeLength].y] = GameBoardFields.Snake;
                snakeLength++;

                //Check si il peut ajouter un bonus par rapport a la place restante
                if (snakeLength < 96)
                    Bonus();


                this.Text = "Snake - score : " + snakeLength;
            }

            //Dessine une nouvelle tete, change les coordonées du serpent et refresh la page pour montrer les changements
            g.DrawImage(imgList.Images[6], snakeXY[0].x * 35, snakeXY[0].y * 35);

            gameBoardField[snakeXY[0].x, snakeXY[0].y] = GameBoardFields.Snake;

            picGameBoard.Refresh();
        }
    }
}
