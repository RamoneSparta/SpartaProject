using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Office_Meandering_Game_Project
{
    public partial class Game : Form
    {
        private List<Panel> listOfPanels = new List<Panel>();
        private int player1Health = 10;
        private int player2Health = 10;
        private int playerSpeed = 10;
        private int panelIndex = 0;
        private bool goUp;
        private bool goUp2;
        private bool goDown;
        private bool goDown2;
        private bool isPressed;
        private bool isPressed2;

        public int Player1Health { get => player1Health; set => player1Health = value; }
        public int Player2Health { get => player2Health; set => player2Health = value; }
        public int PlayerSpeed { get => playerSpeed; set => playerSpeed = value; }
        public int PanelIndex { get => panelIndex; set => panelIndex = value; }
        public bool GoUp { get => goUp; set => goUp = value; }
        public bool GoUp2 { get => goUp2; set => goUp2 = value; }
        public bool GoDown { get => goDown; set => goDown = value; }
        public bool GoDown2 { get => goDown2; set => goDown2 = value; }
        public bool IsPressed { get => isPressed; set => isPressed = value; }
        public bool IsPressed2 { get => isPressed2; set => isPressed2 = value; }

        public Game()
        {
            InitializeComponent();
            listOfPanels.Add(panstart);
            listOfPanels.Add(gamePanel);
            EnablePannel();
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
            {
                GoUp = true;
            }

            if (e.KeyCode == Keys.S)
            {
                GoDown = true;
            }

            if (e.KeyCode == Keys.Space && !isPressed)
            {
                isPressed = true;
                MakeBullets();
            }

            if (e.KeyCode == Keys.NumPad8)
           {
                GoUp2 = true;
            }

            if (e.KeyCode == Keys.NumPad5)
            {
                GoDown2 = true;
            }

            if (e.KeyCode == Keys.Right && !isPressed2)
            {
                isPressed2 = true;
                MakeBullets2();
            }

        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
            {
                GoUp = false;
            }

            if (e.KeyCode == Keys.S)
            {
                GoDown = false;
            }

            if (isPressed)
            {
                isPressed = false;
            }

            if (isPressed2)
            {
                isPressed2 = false;
            }

            if (e.KeyCode == Keys.NumPad8)
            {
                GoUp2 = false;
            }

            if (e.KeyCode == Keys.NumPad5)
            {
                GoDown2 = false;
            }
        }

        private void Player1Timer(object sender, EventArgs e)
        {
            label1.Text = "Health: "+player1Health.ToString();
            if (GoUp && Player1.Top > 0)
            {
                Player1.Top -= PlayerSpeed;
  
            }
            else if (GoDown && Player1.Bottom < 350)
            {
                Player1.Top += PlayerSpeed;
            }

            foreach (Control y in this.Controls)
            {
                if (y is PictureBox && y.Tag == "bullet")
                {
                    y.Left += playerSpeed;
                    if (((PictureBox)y).Right < this.Width - 800)
                    {
                        this.Controls.Remove(y);
                    }
                }
            }
            //foreach (Control i in this.Controls)
            //{
            //    foreach (Control j in this.Controls)
            //    {
            //        if (i.Tag == "bullet2" && i is PictureBox)
            //        {
            //            if (j is PictureBox && j.Tag == "airplane")
            //            {
            //                if (i.Bounds.IntersectsWith(j.Bounds))
            //                {
            //                    player1Health--;
            //                    this.Controls.Remove(j);
            //                }
            //            }
            //        }
            //    }

            //}

        }


        private void Player2Timer(object sender, EventArgs e)
        {
            label2.Text = "Health: " + player2Health.ToString();
            if (GoUp2 && Player2.Top > 0)
            {
                Player2.Top -= PlayerSpeed;

            }
            else if (GoDown2 && Player2.Bottom < 350)
            {
                Player2.Top += PlayerSpeed;
            }

            foreach (Control y in this.Controls)
            {
                if (y is PictureBox && y.Tag == "bullet2")
                {
                    y.Left -= playerSpeed;
                    if (((PictureBox)y).Left < this.Width - 800)
                    {
                        this.Controls.Remove(y);
                    }
                }
                //foreach (Control i in this.Controls)
                //{
                //    foreach (Control j in this.Controls)
                //    {
                //        if (i.Tag == "bullet" && i is PictureBox)
                //        {
                //            if (j is PictureBox && j.Tag == "airplane2")
                //            {
                //                if (i.Bounds.IntersectsWith(j.Bounds))
                //                {
                //                    player1Health--;
                //                    this.Controls.Remove(j);
                //                }
                //            }
                //        }
                //    }
                //}


            }
        }

        private void MakeBullets()
        {
            PictureBox bullet = new PictureBox();
            bullet.Image = Properties.Resources.bullet;
            bullet.SizeMode = PictureBoxSizeMode.StretchImage;
            bullet.Size = new Size(15, 15);
            bullet.Tag = "bullet";
            bullet.Left = Player1.Left + Player1.Width;
            bullet.Top = Player1.Top + Player1.Height /2;
            this.Controls.Add(bullet);
            bullet.BringToFront();
            CalcDamage1(bullet);


        }
        public void CalcDamage1(PictureBox bullet)
        {
            if (bullet.Location.Y <= Player2.Location.X)
            {
                if (bullet.Location.Y >= (Player2.Location.Y - Player2.Height))
                {
                    Player2Health--;
                }
            }
        }

        private void MakeBullets2()
        {
            PictureBox bullet2 = new PictureBox();
            bullet2.Image = Properties.Resources.bullet2;
            bullet2.SizeMode = PictureBoxSizeMode.StretchImage;
            bullet2.Size = new Size(15, 15);
            bullet2.Tag = "bullet2";
            bullet2.Left = Player2.Left;
            bullet2.Top = Player2.Top + Player2.Height /2;
            this.Controls.Add(bullet2);
            bullet2.BringToFront();
            CalcDamage2(bullet2);
            
        }

        public void CalcDamage2(PictureBox bullet)
        {
            if (bullet.Location.Y >= Player1.Location.X)
            {
                if (bullet.Location.Y <= (Player1.Location.Y + Player1.Height))
                {
                    Player1Health--;
                }
            }
        }

        public string GetPannelIndex(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            return button.Text;
        }

        private void EnablePannel()
        {
            listOfPanels[PanelIndex].BringToFront();
            if (PanelIndex == 0)
            {
                pictureBox1.Visible = true;
                btnstart.Enabled = true;
                btnstart.Visible = true;
            }

        }


        private void DisablePannel()
        {
            listOfPanels[PanelIndex].SendToBack();
            if (PanelIndex == 0)
            {
                pictureBox1.Visible = false;
                btnstart.Enabled = false;
                btnstart.Visible = false;
            }

        }


        public void PannelChange(object sender, EventArgs e)
        {

            switch (GetPannelIndex(sender, e))
            {
                case "Start":
                    DisablePannel();
                    PanelIndex++;
                    EnablePannel();
                    break;
                case "Restart":
                    DisablePannel();
                    PanelIndex--;
                    EnablePannel();
                    break;
                default:
                    break;
            }

        }

        public void GameOver()
        {
            timer1.Stop();
            timer2.Stop();
            MessageBox.Show("Game Over Someone Wins");
        }
    }
}
