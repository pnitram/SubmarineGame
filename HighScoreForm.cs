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
        private int _count;
        private readonly List<AddHighScore> _highScores;
        private string _scoresString1;
        private string _scoresString2;
        private StringBuilder _stringBuilder1;
        private StringBuilder _stringBuilder2;


        public HighScoreForm()
        {
            InitializeComponent();

            try
            {
                //Deserialiserer highscore liste objektet fra fil for å hente tilbake lagrede highscores
                using (var fileStream = new FileStream("scores.dat", FileMode.Open, FileAccess.Read))
                {
                    var formatter = new BinaryFormatter();
                    _highScores = (List<AddHighScore>) formatter.Deserialize(fileStream);
                }
                //sorterer higscores
                _highScores.Sort();
            }
            catch (Exception e)
            {
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
                //Bygger to stringbuider objekter
                foreach (var score in _highScores.Take(10))
                    if (++_count <= 5)
                        _stringBuilder1.Append("\n" + _count + ": " + score.PlayerName + ", " + score.Score + " poeng" +
                                               "\n");

                    else
                        _stringBuilder2.Append("\n" + _count + ": " + score.PlayerName + ", " + score.Score + " poeng" +
                                               "\n");
                //Omgjør objektene til strenger
                _scoresString1 = _stringBuilder1.ToString();
                _scoresString2 = _stringBuilder2.ToString();

                var dc = e.Graphics;

                var flags = TextFormatFlags.Left | TextFormatFlags.NoPadding;
                var font = new Font("Stencil", 12, FontStyle.Regular);

                //Tegner/viser to kolloner med highscore
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

        //Knapp for å starte applikasjon på nytt
        private void button1_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        //Knapp for å avslutte applikasjon
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}