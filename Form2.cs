using System;
using System.Windows.Forms;

namespace SubmarineGame
{
    public partial class GameOverForm : Form
    {
        public GameOverForm()
        {
            InitializeComponent();
            label3.Text = "Du oppnådde: " + Form1.Poeng + " poeng!";

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
