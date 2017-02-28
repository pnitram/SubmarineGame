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
            label3.Text = "Du oppnådde: " + SubmarineMainForm.Poeng + " poeng!";

        }

        private void label1_Click(object sender, EventArgs e)
        {
            //Ikke i bruk
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                textBox1.Text = "Annonymous";
            }


            if (!File.Exists("scores.dat"))
            {

                _score = new AddHighScore() { Score = SubmarineMainForm.Poeng, PlayerName = textBox1.Text };
                _highScores = new List<AddHighScore>();
                _highScores.Add(_score);

                using (var fileStream = new FileStream("scores.dat", FileMode.Create, FileAccess.Write))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(fileStream, _highScores);
                }

            }

           else
            {
                using (var fileStream = new FileStream("scores.dat", FileMode.Open, FileAccess.ReadWrite))
                {
                    var formatter = new BinaryFormatter();
                    _highScores = (List<AddHighScore>)formatter.Deserialize(fileStream);
                }
                _score = new AddHighScore() { Score = SubmarineMainForm.Poeng, PlayerName = textBox1.Text };
                _highScores.Add(_score);

                using (var fileStream = new FileStream("scores.dat", FileMode.Create, FileAccess.Write))
                {
                    var formatter = new BinaryFormatter();
                    formatter.Serialize(fileStream, _highScores);
                }


            }

            Hide();
            Form HighScore = new HighScoreForm();
            HighScore.ShowDialog();

        }
    }
}
