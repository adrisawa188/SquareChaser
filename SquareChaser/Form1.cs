using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace SquareChaser
{
    public partial class Form1 : Form
    {
        //global variables
        Rectangle player1 = new Rectangle(10, 250, 20, 20);
        Rectangle player2 = new Rectangle(470, 250, 20, 20);
        Rectangle pointSquare = new Rectangle(250, 250, 10, 10);
        Rectangle speedBoost = new Rectangle(250, 50, 10, 10);
        Rectangle enemy = new Rectangle(250, 450, 20, 20);

        int p1Score = 0;
        int p2Score = 0;
        int player1Speed = 4;
        int player2Speed = 4;
        int enemyXSpeed = 4;
        int enemyYSpeed = 6;      
       
        bool wDown = false;
        bool sDown = false;
        bool upArrowDown = false;
        bool downArrowDown = false;
        bool aDown = false;
        bool dDown = false;
        bool leftDown = false;
        bool rightDown = false;

        SolidBrush blueBrush = new SolidBrush(Color.DodgerBlue);
        SolidBrush greenBrush = new SolidBrush(Color.Green);
        SolidBrush yellowBrush = new SolidBrush(Color.Yellow);
        Pen greenPen = new Pen(Color.Green, 2);
        Pen redPen = new Pen(Color.Red, 2);
        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = true;
                    break;
                case Keys.S:
                    sDown = true;
                    break;
                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;
                case Keys.D:
                    dDown = true;
                    break;
                case Keys.A:
                    aDown = true;
                    break;
                case Keys.Left:
                    leftDown = true;
                    break;
                case Keys.Right:
                    rightDown = true;
                    break;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = false;
                    break;
                case Keys.S:
                    sDown = false;
                    break;
                case Keys.Up:
                    upArrowDown = false;
                    break;
                case Keys.Down:
                    downArrowDown = false;
                    break;
                case Keys.D:
                    dDown = false;
                    break;
                case Keys.A:
                    aDown = false;
                    break;
                case Keys.Left:
                    leftDown = false;
                    break;
                case Keys.Right:
                    rightDown = false;
                    break;
            }
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            //select random value for point square to appear
            Random randGen1 = new Random();          
            int randomXValue = randGen1.Next(0, 480);
            int randomYValue = randGen1.Next(0, 480);
            SoundPlayer sound1 = new SoundPlayer(Properties.Resources._270303__littlerobotsoundfactory__collect_point_01);
            SoundPlayer sound2 = new SoundPlayer(Properties.Resources._455857__tissman__simple_power_up);
            SoundPlayer sound3 = new SoundPlayer(Properties.Resources._1401__sleep__muted_f_5th);

            //move player
            if (wDown == true && player1.Y > 0)
            {
                player1.Y -= player1Speed;
            }

            if (sDown == true && player1.Y < this.Height - player1.Height)
            {
                player1.Y += player1Speed;
            }

            if (upArrowDown == true && player2.Y > 0)
            {
                player2.Y -= player2Speed;
            }

            if (downArrowDown == true && player2.Y < this.Height - player2.Height)
            {
                player2.Y += player2Speed;
            }

            if (aDown == true && player1.X > 0)
            {
                player1.X -= player1Speed;
            }

            if (dDown == true && player1.X < this.Width - player2.Width)
            {
                player1.X += player1Speed;
            }

            if (leftDown == true && player2.X > 0)
            {
                player2.X -= player2Speed;
            }

            if (rightDown == true && player2.X < this.Width - player2.Width)
            {
                player2.X += player2Speed;
            }

            //collision with point square
            if (player1.IntersectsWith(pointSquare))
            {
                p1Score++;
                p1ScoreLabel.Text = $"{p1Score}";
                pointSquare.X = randomXValue;
                pointSquare.Y = randomYValue;
                sound1.Play();
            }
           else if (player2.IntersectsWith(pointSquare))
            {
                p2Score++;
                p2ScoreLabel.Text = $"{p2Score}";
                pointSquare.X = randomXValue;
                pointSquare.Y = randomYValue;
                sound1.Play();                               
            }       
            
            //collision with speed boost  
            if (player1.IntersectsWith(speedBoost))
            {
                player1Speed++;
                speedBoost.X = randomXValue;
                speedBoost.Y = randomYValue;
                sound2.Play();
            }
            else if (player2.IntersectsWith(speedBoost))
            {
                player2Speed++;
                speedBoost.X = randomXValue;
                speedBoost.Y = randomYValue;
                sound2.Play();
            }

            //move enemy
            enemy.X += enemyXSpeed;
            enemy.Y += enemyYSpeed;

            //enemy collsion with walls
            if (enemy.Y < 0 || enemy.Y > this.Height - enemy.Height)
            {
                enemyYSpeed *= -1;  // or: ballYSpeed = -ballYSpeed; 
            }
            else if (enemy.X < 0 || enemy.X > this.Height - enemy.Height)
            {
                enemyXSpeed *= -1;
            }

            //collision with enemy
            if (player1.IntersectsWith(enemy) && player1Speed > 2)
            {
                player1Speed--;
                sound3.Play();
                enemy.X = randomXValue;
                enemy.Y = randomYValue;
                p1Score--;
                p1ScoreLabel.Text = $"{p1Score}";
            }
            else if (player2.IntersectsWith(enemy) && player2Speed > 2)
            {
                player2Speed--;
                sound3.Play();
                enemy.X = randomXValue;
                enemy.Y = randomYValue;
                p2Score--;
                p2ScoreLabel.Text = $"{p2Score}";
            }
            //diplay winner
            if (p1Score == 15)
            {
                gameTimer.Enabled = false;
                winLabel.Visible = true;
                winLabel.Text = "Player 1 Wins!!";
            }
            else if (p2Score == 15)
            {
                gameTimer.Enabled = false;
                winLabel.Visible = true;
                winLabel.Text = "Player 2 Wins!!";
            }

            Refresh();
        }


        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(blueBrush, player1);
            e.Graphics.FillRectangle(greenBrush, player2);
            e.Graphics.FillRectangle(yellowBrush, pointSquare);
            e.Graphics.DrawRectangle(greenPen, speedBoost);
            e.Graphics.DrawRectangle(redPen, enemy);

        }

        private void startButton_Click(object sender, EventArgs e)
        {
            SoundPlayer sound1 = new SoundPlayer(Properties.Resources._515823__matrixxx__select_granted_04);
            sound1.Play();
            gameTimer.Enabled = true;
            startButton.Enabled = false;
            startButton.Visible = false;
        }
    }
}
