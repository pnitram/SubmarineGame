using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;

namespace SubmarineGame
{
    public partial class HighScoreForm : Form
    {
        private readonly List<AddHighScore> _highScores;
        private int _count;
        private string _scoresString1;
        private string _scoresString2;
        private StringBuilder _stringBuilder1;
        private StringBuilder _stringBuilder2;


        public HighScoreForm()
        {
            InitializeComponent();

            try
            {
                //Deserialize highscore list object --> gets the stored hiscore list from binary file
                using (var fileStream = new FileStream("scores.dat", FileMode.Open, FileAccess.Read))
                {
                    var formatter = new BinaryFormatter();
                    _highScores = (List<AddHighScore>) formatter.Deserialize(fileStream);
                }
                //sorts the higscores
                _highScores.Sort();
            }
            catch (Exception e)
            {
                //Writes error code to console if no highscore file
                Console.WriteLine(e);
            }
        }


        private void HighScore_Paint(object sender, PaintEventArgs e)
        {
            _stringBuilder1 = new StringBuilder();
            _stringBuilder2 = new StringBuilder();
            _count = 0;

            try
            {
                //Builds two stringbuider objects each containing five highscores 
                foreach (var score in _highScores.Take(10))
                    if (++_count <= 5)
                        _stringBuilder1.Append("\n" + _count + ": " + score.PlayerName + ", " + score.Score + " points" +
                                               "\n");

                    else
                        _stringBuilder2.Append("\n" + _count + ": " + score.PlayerName + ", " + score.Score + " points" +
                                               "\n");
                //Stringbuilder objects ->  string objects
                _scoresString1 = _stringBuilder1.ToString();
                _scoresString2 = _stringBuilder2.ToString();

                var dc = e.Graphics;
                var flags = TextFormatFlags.Left | TextFormatFlags.NoPadding;
                var font = new Font("Stencil", 12, FontStyle.Regular);

                //Renders two highscore colums
                TextRenderer.DrawText(dc, _scoresString1, font, new Rectangle(100, 60, 700, 350),
                    SystemColors.ControlText, flags);
                TextRenderer.DrawText(dc, _scoresString2, font, new Rectangle(300, 60, 700, 350),
                    SystemColors.ControlText, flags);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        //Button to restart the application
        private void button1_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        //Button to close the application
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}