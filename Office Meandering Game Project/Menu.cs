﻿using System;
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
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
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
                        Game form = new Game();
                        form.Show();
                        
                    }
                    break;

                case "Options":
                    
                    if (Application.OpenForms.OfType<Options>().Count() == 1)
                    {
                        Application.OpenForms.OfType<Options>().First().Close();
                    }
                    Options op = new Options();
                    op.Show();


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

    }
}
