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
using System.IO;

namespace Office_Meandering_Game_Project
{
    public partial class Menu : Form
    {
        SoundPlayer mainMenu = new SoundPlayer();

        public Menu()
        {
            InitializeComponent();
            PlayMusic();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch (GetButton(sender))
            {
                case "Play":
                    if (Application.OpenForms.OfType<Game>().Count() == 1)
                    {
                        MessageBox.Show("Game already in progress!");
                    }
                    else
                    {
                       // StopMusic();
                        Game form = new Game();
                        form.Show();
                    }
                    break;

                case "How To Play":
                    
                    if (Application.OpenForms.OfType<HTP>().Count() == 1)
                    {
                        Application.OpenForms.OfType<HTP>().First().Close();
                    }
                    HTP htp = new HTP();
                    htp.Show();


                    break;

                case "Exit":
                    this.Close();
                    break;
                    
            }
        }

        public string GetButton(object o)
        {
            Button btn = (Button)o;
            return btn.Text;
        }

        public void PlayMusic ()
        {
            mainMenu.SoundLocation = "Resources/sounds/Blazer Rail.wav";
            mainMenu.PlayLooping();
        }

        public void StopMusic ()
        {
            mainMenu.Stop();
        }

    }
}
