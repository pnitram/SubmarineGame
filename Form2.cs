using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace SubmarineGame
{
    public partial class GameOverForm : Form
    {

        private List<AddHighScore> _highScores;
        private AddHighScore _score;

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

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                textBox1.Text = "Annonymous player";
            }


            if (!File.Exists("scores.dat"))
            {

                _score = new AddHighScore() { Score = Form1.Poeng, PlayerName = textBox1.Text };
                _highScores = new List<AddHighScore>();
                _highScores.Add(_score);

                using (var fileStream = new FileStream("scores.dat", FileMode.Create, FileAccess.Write))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(fileStream, _highScores);
                }
;
            }

           if (File.Exists("scores.dat") )
            {
                using (var fileStream = new FileStream("scores.dat", FileMode.Open, FileAccess.ReadWrite))
                {
                    var formatter = new BinaryFormatter();
                    _highScores = (List<AddHighScore>)formatter.Deserialize(fileStream);
                }
                _score = new AddHighScore() { Score = Form1.Poeng, PlayerName = textBox1.Text };
                _highScores.Add(_score);

                using (var fileStream = new FileStream("scores.dat", FileMode.Create, FileAccess.Write))
                {
                    var formatter = new BinaryFormatter();
                    formatter.Serialize(fileStream, _highScores);
                }
                this.Hide();
                Form HighScore = new HighScore();
                HighScore.ShowDialog();

            }
            
        }
    }
}
