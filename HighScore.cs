using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace SubmarineGame
{
    public partial class HighScore : Form
    {
        private List<AddHighScore> _highScores;


        public HighScore()
        {
            InitializeComponent();

            using (var fileStream = new FileStream("scores.dat", FileMode.Open, FileAccess.Read))
            {
                var formatter = new BinaryFormatter();
                _highScores = (List<AddHighScore>)formatter.Deserialize(fileStream);
            }
            _highScores.Sort();

            foreach (var scores in _highScores)
            {
                MessageBox.Show(scores.PlayerName + " " + scores.Score + " " + _highScores.Count);
            }
        }

        private void scoreLabel6_Click(object sender, EventArgs e)
        {
            //ikke i bruk
        }
    }
}
