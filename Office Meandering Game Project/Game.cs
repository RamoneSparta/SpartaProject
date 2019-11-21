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



namespace Office_Meandering_Game_Project
{
    public partial class Game : Form
    {
        private SoundPlayer gameMusic = new SoundPlayer();
        private List<Panel> listOfPanels = new List<Panel>();
        private int player1Health = 20;
        private int player2Health = 20;
        private int playerSpeed = 10;
        private int bulletSpeed = 100;
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
        public int BulletSpeed { get => bulletSpeed; set => bulletSpeed = value; }
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
         //   PlayBackgroundMusic();
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
            {
                GoUp = true;
                e.Handled = e.SuppressKeyPress = true;
            }

            if (e.KeyCode == Keys.S)
            {
                GoDown = true;
                e.Handled = e.SuppressKeyPress = true;
            }

            if (e.KeyCode == Keys.B && !isPressed)
            {
                isPressed = true;
                e.Handled = e.SuppressKeyPress = true;
                MakeBullets();
            }

            if (e.KeyCode == Keys.NumPad8)
            {
                GoUp2 = true;
                e.Handled = e.SuppressKeyPress = true;
            }

            if (e.KeyCode == Keys.NumPad5)
            {
                GoDown2 = true;
                e.Handled = e.SuppressKeyPress = true;
            }

            if (e.KeyCode == Keys.Left && !isPressed2)
            {
                isPressed2 = true;
                e.Handled = e.SuppressKeyPress = true;
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
                    y.Left += BulletSpeed;
                    if (((PictureBox)y).Right < this.Width - 800)
                    {
                        this.Controls.Remove(y);
                        y.Dispose();
                    }
                }
            }

        }


        private void Player2Timer(object sender, EventArgs e)
        {
            label2.Text = "Health: " + player2Health.ToString();
            if (GoUp2 && Player2.Top > 0)
            {
                Player2.Top -= playerSpeed;

            }
            else if (GoDown2 && Player2.Bottom < 350)
            {
                Player2.Top += PlayerSpeed;
            }

            foreach (Control y in this.Controls)
            {
                if (y is PictureBox && y.Tag == "bullet2")
                {
                    y.Left -= BulletSpeed;
                    if (((PictureBox)y).Left < this.Width - 800)
                    {
                        
                        this.Controls.Remove(y);
                        y.Dispose();
                    }
                }

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
            PlayBulletSound();
            CalcDamage1(bullet);
            


        }
        public void CalcDamage1(PictureBox bullet)
        {
            if (bullet.Location.Y <= (Player2.Location.Y + Player2.Height) && bullet.Location.Y >= (Player2.Location.Y ))
            {
                if ( bullet.Location.X <= Player2.Location.X )
                {
                    Player2Health--;
                    if (Player2Health == 0)
                    {
                        label2.Text = "You ded now";
                       // StopMusic();
                        GameOver();
                    }
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
            PlayBulletSound();
            CalcDamage2(bullet2);
            
        }

        public void CalcDamage2(PictureBox bullet)
        {
            if (bullet.Location.Y >= Player1.Location.Y)
            {
                if (bullet.Location.Y <= (Player1.Location.Y + Player1.Height))
                {
                    Player1Health--;
                    if (Player1Health == 0)
                    {
                        label1.Text = "You ded now";
                      //  StopMusic();
                        GameOver();
                    }
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
            if (Player1Health <= 0)
            {
                MessageBox.Show("Player 2 Wins");
                this.Dispose();
                this.Close();
                
            }
            else if (Player2Health <= 0)
            {
                MessageBox.Show("Player 1 Wins!!!");
                this.Dispose();
                this.Close();
            }
            
        }

        public void PlayBackgroundMusic()
        {
            gameMusic.SoundLocation = "Resources/sounds/Lounge Game2.wav";
            gameMusic.PlayLooping();
        }

        public void PlayBulletSound()
        {
            var bulletSound = new System.Windows.Media.MediaPlayer();
            bulletSound.Open(new Uri("Resources/sounds/220611__senitiel__pistol3.wav", UriKind.Relative));
            bulletSound.Play();
        }

        public void StopMusic()
        {
            gameMusic.Stop();
        }

    }
}
