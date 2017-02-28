using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace SubmarineGame
{
    public partial class GameOverForm : Form
    {
        private readonly int _points;

        private List<AddHighScore> _highScores;
        private AddHighScore _score;


        public GameOverForm(int points)
        {
            _points = points;
            InitializeComponent();
            label3.Text = @"You recived: " + _points + @" point!";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //if user does not enter name. Store as Annonymous

            if (string.IsNullOrEmpty(textBox1.Text))
                textBox1.Text = @"Annonymous";


            if (!File.Exists("scores.dat"))
            {
                //If no scores.dat file --> create list --> add higscore --> serialize list to file 

                _score = new AddHighScore {Score = _points, PlayerName = textBox1.Text};
                _highScores = new List<AddHighScore>();
                _highScores.Add(_score);

                using (var fileStream = new FileStream("scores.dat", FileMode.Create, FileAccess.Write))
                {
                    var formatter = new BinaryFormatter();
                    formatter.Serialize(fileStream, _highScores);
                }
            }

            else
            {
                //scores.dat file --> deserialize --> add higscore --> serialize list to file 

                using (var fileStream = new FileStream("scores.dat", FileMode.Open, FileAccess.ReadWrite))
                {
                    var formatter = new BinaryFormatter();
                    _highScores = (List<AddHighScore>) formatter.Deserialize(fileStream);
                }
                _score = new AddHighScore {Score = _points, PlayerName = textBox1.Text};
                _highScores.Add(_score);

                using (var fileStream = new FileStream("scores.dat", FileMode.Create, FileAccess.Write))
                {
                    var formatter = new BinaryFormatter();
                    formatter.Serialize(fileStream, _highScores);
                }
            }

            //hides gameoverform and opens highscoreform
            Hide();
            using (Form highScore = new HighScoreForm())
            {
                highScore.ShowDialog();
            }
        }
    }
}