using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Car_Race
{
    public partial class Form1 : Form
    {

        List<PictureBox> pictureBoxes = new List<PictureBox>();
        List<PictureBox> enemyBoxes = new List<PictureBox>();
        List<PictureBox> coinBoxes = new List<PictureBox>();

        public Form1()
        {

            InitializeComponent();

            labelGameOver.Visible = false;
           
            //FIND PICTUREBOXES
            for (int column = 1; column <= 6; column++)
            {
                for (int row = 1; row <= 4; row++)
                {
                    string pBoxName = $"pictureBox{column}_{row}";
                    if (findPictureBox(pBoxName) != null)
                    {
                       pictureBoxes.Add(findPictureBox(pBoxName));
                    }
                }
            }

            //FIND ENEMYBOXES
            for (int row = 1; row <= 4; row++)
            {
                string pBoxName = $"pictureEnemy{row}";

                if (findPictureBox(pBoxName) != null)
                {
                   enemyBoxes.Add(findPictureBox(pBoxName));
                }
              

            }

            //FIND COINBOXES

            for (int row = 1; row <= 5; row++)
            {
                string pBoxName = $"pictureCoin{row}";

                if (findPictureBox(pBoxName) != null)
                {
                    coinBoxes.Add(findPictureBox(pBoxName));
                }
            }
        }


        PictureBox findPictureBox(string key)
        {

                PictureBox foundPictureBox = this.Controls.Find(key, true)[0] as PictureBox;
                //foreach index in
                if (foundPictureBox != null)
                {
                    return (foundPictureBox);
                }
                else return null;
            //PictureBox foundPictureBox = (PictureBox)this.Controls.Find(Name, true)[0];
        }

        void gameIsOver() 
        {

            foreach (PictureBox enemyBox in enemyBoxes) 
            {
                if (pictureCar.Bounds.IntersectsWith(enemyBox.Bounds)) { timer1.Enabled = false; labelGameOver.Visible = true; coinCount = 0; }
                
            }
        }

        void gameIsRestarted()
        {
            //make coins and enemies move before restarting game

            foreach (PictureBox enemyBox in enemyBoxes)
            {
                if (pictureCar.Bounds.IntersectsWith(enemyBox.Bounds)) 
                {
                    reloadPictureBoxRandom(enemyBox);
                }

            }
            movePictureBoxes(1);
        }


        void coinIsCollected()
        {

            foreach (PictureBox coinBox in coinBoxes)
            {
                if (pictureCar.Bounds.IntersectsWith(coinBox.Bounds)) 
                { 
                    coinCount++; 
                    coinBox.Visible = false; coinSound.Play();
                    reloadPictureBoxRandom(coinBox);
                }
                else { coinBox.Visible = true; }

            }
        }


        void moveLine(int speed) 
        {
            
               
            foreach (PictureBox pictureBox in pictureBoxes) 
            {
                if (speed > 0)
                {
                    if (pictureBox.Top >= this.Height) { pictureBox.Top = 0; } else { pictureBox.Top += speed; }
                }
                else 
                {
                    if (pictureBox.Top <= 0) { pictureBox.Top = this.Height; } else { pictureBox.Top += speed; }
                }
                
            }

            LabelSpeed.Text = gameSpeed.ToString();
            LabelSteer.Text = steerSpeed.ToString();
            LabelCoin.Text = "★ " + coinCount.ToString();

        }

        int count = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            
            moveLine(gameSpeed);
            movePictureBoxes(6);
            gameIsOver();
            coinIsCollected();

            count++;

            if (count == 10) 
            {
                if (gameSpeed > 2)
                {
                    gameSpeed -= 2+ (gameSpeed/25 );

                }
                if (gameSpeed < (-2))
                {
                    gameSpeed += 2 + (-gameSpeed / 25);

                }
                count = 0;
            }
            if (count == 1 || count == 3 || count == 5 || count == 7 || count == 9) 
            {
                if (steerSpeed > 1)
                {
                    steerSpeed -= (steerSpeed / 8);

                }
            }

            
        }

        int gameSpeed = 1;
        int steerSpeed = 1;
        int coinCount = 0;
        System.Media.SoundPlayer coinSound = new System.Media.SoundPlayer(@"G:\IT Projects\Car Racing Game\Assets\coinSound.wav");
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Left) 
            { if (pictureCar.Left > (0 + steerSpeed))
                { pictureCar.Left += -steerSpeed; }
                { steerSpeed++; }
            }

            if (e.KeyCode == Keys.Right)
            {
                if (pictureCar.Left < (this.Width - (steerSpeed + pictureCar.Width)))
                { pictureCar.Left += steerSpeed; }
                { steerSpeed++; }
            }

            if (e.KeyCode == Keys.Up)
            {
                if ((gameSpeed >= 0) && pictureCar.Top > (0 + gameSpeed))
                { pictureCar.Top += -gameSpeed; }
                { gameSpeed++; }
            }
            
            if (e.KeyCode == Keys.Down)
            {
                if ((gameSpeed >= 0)&&(pictureCar.Top < (this.Height - (gameSpeed + pictureCar.Height))))
                { pictureCar.Top += gameSpeed; }
                { gameSpeed--; }
            }

            //LabelSpeed.Text = gameSpeed.ToString();

        }


        Random randomValue = new Random();
        void reloadPictureBoxRandom(PictureBox pictureBoxToMove) 
        {
            int xAxis;
            xAxis = randomValue.Next(0, this.Width);
            pictureBoxToMove.Location = new Point(xAxis, 0);
        }

        void movePictureBoxes(int speed) 
        {
            foreach (PictureBox enemyBox in enemyBoxes) 
            {
                if ((enemyBox.Top +speed) >= this.Height)
                {
                    reloadPictureBoxRandom(enemyBox);

                }
                else { enemyBox.Top += speed; }

            }


            foreach (PictureBox coinBox in coinBoxes)
            {
                if ((coinBox.Top + speed) >= this.Height)
                {
                    reloadPictureBoxRandom(coinBox);
                }
                else { coinBox.Top += speed; }

            }


        }

        private void labelGameOver_Click(object sender, EventArgs e)
        {
            labelGameOver.Visible = false;
            gameIsRestarted();
            timer1.Enabled = true;
        }
    }



}
