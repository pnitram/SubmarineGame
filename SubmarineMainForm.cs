//Kommenter ut linjen under for å slå av debug 

//#define Debug

using System;
using System.Drawing;
using System.Media;
using System.Windows.Forms;
using SubmarineGame.Properties;

namespace SubmarineGame
{
    public partial class SubmarineMainForm : Form
    {
        public static int Poeng;
        private PictureBox _boat1;
        private int _boat1Speed;
        private PictureBox _boat2;
        private int _boat2Speed;
        private int _liv;
        private string _nivaa;
        private PictureBox _plane;
        private int _planeSpeed;
        private PictureBox _player;
        private Random _random;
        private PictureBox _skudd;
        private bool _styring;
        private SoundPlayer sp;
        private int _mOffOnCount;


        public SubmarineMainForm()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            _random = new Random();
            var s = new Size(800, 600);
            ClientSize = s;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Oppsett();
        }

        private void Oppsett()
        {
            var x = _random.Next(-200, -10);
            var x2 = _random.Next(800, 1200);


            _styring = true;
            Poeng = 0;
            _liv = 3;
            _nivaa = "Noob";
            _boat1Speed = 5;
            _boat2Speed = 7;
            _planeSpeed = 20;
            _skudd = new PictureBox();

            _boat1 = new PictureBox();
            _boat1.SetBounds(0, 25, 127, 83);
            _boat1.Image = Resources.boat1;
            _boat1.BackColor = Color.Transparent;
            _boat1.Left = x;
            Controls.Add(_boat1);

            _boat2 = new PictureBox();
            _boat2.SetBounds(0, 600, 38, 37);
            _boat2.Image = Resources.boat2;
            _boat2.BackColor = Color.Transparent;
            _boat2.Left = x2;
            Controls.Add(_boat2);

            _plane = new PictureBox();
            _plane.SetBounds(0, 100, 93, 51);
            _plane.Image = Resources.plane;
            _plane.BackColor = Color.Transparent;
            _plane.Left = -1000;
            Controls.Add(_plane);

            _player = new PictureBox();
            _player.Image = Resources.sub;
            _player.BackColor = Color.Transparent;
            _player.SetBounds(350, 525, 93, 51);
            Controls.Add(_player);

            _skudd.SetBounds(_player.Location.X + 28, -350, 9, 20);

            PlayBkMusic();
        }

        private void PlayBkMusic()
        {
            sp = new SoundPlayer(Resources.bkMusic);
            sp.PlayLooping();
        }


        private void NyttSkudd()
        {
            _skudd.Image = Resources.torpedo;
            _skudd.BackColor = Color.Transparent;
            _skudd.SetBounds(_player.Location.X + 28, 540, 9, 20);
            Controls.Add(_skudd);
        }

        private void exitMenuItem1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void highscordMenuItem1_Click(object sender, EventArgs e)
        {
            Form highScore = new HighScoreForm();
            highScore.ShowDialog();
        }


        private void EndreNivaa()
        {
            if (_nivaa == "Noob")

            {
                if (Poeng <= 5)
                    _nivaa = Nivaa("Noob");
                if (Poeng > 5)
                    _nivaa = Nivaa("Normal");
            }

            else if (_nivaa == "Normal")
            {
                if (Poeng <= 5)
                    _nivaa = Nivaa("Normal");
                else if (Poeng > 5 && Poeng < 10)
                    _nivaa = Nivaa("Normal");
                else if (Poeng >= 10 && Poeng < 20)
                    _nivaa = Nivaa("Ekspert");
            }
            else if (_nivaa == "Ekspert")
            {
                if (Poeng <= 5)
                    _nivaa = Nivaa("Ekspert");
                else if (Poeng > 5 && Poeng < 10)
                    _nivaa = Nivaa("Ekspert");
                else if (Poeng >= 10 && Poeng < 20)
                    _nivaa = Nivaa("Ekspert");
                else if (Poeng > 20)
                    _nivaa = Nivaa("Insane");
            }
        }

        private void EnemyTimer1Tick(object sender, EventArgs e)
        {
            _boat1.Top = 350;
            _boat1.Left += _boat1Speed;

            if (_boat1.Location.X >= 900)
                _boat1.Left = new Random().Next(-900, -300);

            if (_boat1.Location.X >= 1500)
                _boat1.Left = 800;
            if (_skudd.Location.Y == 0)
            {
                _liv--;
                if (_liv == 0)
                {
                    _boat1.Dispose();
                    _boat2.Dispose();
                    _player.Dispose();
                    _plane.Dispose();
                    Form gameOverForm = new GameOverForm();
                    gameOverForm.ShowDialog();
                }
            }


            if (!_skudd.Bounds.IntersectsWith(_boat1.Bounds))
            {
                if (_skudd.Location.Y <= -200)
                {
                    _styring = true;

                    _skudd.SetBounds(_player.Location.X + 28, -350, 20, 20);
                }
            }
            else
            {
                _boat1.Left = 1500;
                _skudd.Location = new Point(_skudd.Location.X, -150);
                _styring = true;
                Poeng++;

                EndreNivaa();
            }
#if Debug
            Console.WriteLine("boat1 X: " + _boat1.Location.X);
            Console.WriteLine("boat1 Y: " + _boat1.Location.Y);
#endif
            Refresh();
        }

        private void EnemyTimer2Tick(object sender, EventArgs e)
        {
            _boat2.Top = 290;
            _boat2.Left -= _boat2Speed;


            if (_boat2.Location.X <= -100)
                _boat2.Left = new Random().Next(900, 1300);

            if (!_skudd.Bounds.IntersectsWith(_boat2.Bounds))
            {
                if (_skudd.Location.Y <= -200)
                {
                    _styring = true;
                    _skudd.SetBounds(_player.Location.X + 28, -350, 20, 20);
                }
            }

            else
            {
                _boat2.Left = 900;
                _skudd.Location = new Point(_skudd.Location.X, -150);
                _styring = true;
                Poeng += 3;
                EndreNivaa();
            }

#if Debug
            Console.WriteLine("boat2 X: " + _boat2.Location.X);
            Console.WriteLine("boat2 Y: " + _boat2.Location.Y);
            Console.WriteLine("Skudd y: " + _skudd.Location.Y);
#endif
            Refresh();
        }


        private void planeTimer_Tick(object sender, EventArgs e)
        {
            _plane.Top = 100;
            _plane.Left += _planeSpeed;
#if Debug
            Console.WriteLine("Plane X: " + _plane.Location.X);
#endif
            if (_plane.Location.X >= 900)
                _plane.Left = new Random().Next(-1500, -300);

            if (_plane.Location.X >= 1500)
                _plane.Left = 800;

            if (!_skudd.Bounds.IntersectsWith(_plane.Bounds))
            {
                if (_skudd.Location.Y <= -200)
                {
                    _styring = true;

                    _skudd.SetBounds(_player.Location.X + 28, -350, 20, 20);
                }
            }
            else
            {
                _plane.Left = -1500;
                _skudd.Location = new Point(_skudd.Location.X, -150);
                _styring = true;
                Poeng += 5;

                EndreNivaa();
            }
#if Debug
            Console.WriteLine("boat1 X: " + _boat1.Location.X);
            Console.WriteLine("boat1 Y: " + _boat1.Location.Y);
#endif
            Refresh();
        }

        private void playerTimer_Tick(object sender, EventArgs e)
        {
        }

        private void skuddTimer_Tick(object sender, EventArgs e)
        {
            _styring = false;

            _skudd.Location = new Point(_skudd.Location.X, _skudd.Location.Y - 30);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (_styring)

                if (e.KeyCode == Keys.Left)
                {
                    _player.Left -= 10;
                    //skudd.Left -= 5;
#if Debug
                    Console.WriteLine(_player.Left);
#endif
                    if (_player.Left <= 0)
                    {
                        _player.Left = 10;
                        //skudd.Left = 25;
#if Debug
                        Console.WriteLine("Player: " + _player.Left);
#endif
                    }
                    else if (_player.Left >= 750)
                    {
                        _player.Left = 745;
                        //skudd.Left = 765;
#if Debug
                        Console.WriteLine("Player: " + _player.Left);
#endif
                    }
                }
                else if (e.KeyCode == Keys.Right)
                {
                    _player.Left += 10;
                    //skudd.Left += 5;
#if Debug
                    Console.WriteLine("Player: " + _player.Left);
#endif

                    if (_player.Left <= 0)
                    {
                        _player.Left = 10;
                        //skudd.Left = 10;
#if Debug
                        Console.WriteLine("Player: " + _player.Left);
#endif
                    }
                    else if (_player.Left >= 720)
                    {
                        _player.Left = 715;
                        //skudd.Left = 765;
#if Debug
                        Console.WriteLine("Player: " + _player.Left);
#endif
                    }
                }
                else if (e.KeyCode == Keys.Space)
                {
                    NyttSkudd();
                    _styring = false;
                }
        }


        public string Nivaa(string skill)
        {
            if (skill == "Noob")
                return "Noob";
            if (skill == "Normal")
            {
                /*                Enemy1.Interval = 25;
                                Enemy2.Interval = 15;*/
                _boat1Speed += 2;
                _boat2Speed += 4;
                _planeSpeed += 4;
                return "Normal";
            }

            if (skill == "Ekspert")
            {
                /*                Enemy1.Interval = 20;
                                Enemy2.Interval = 10;*/
                _boat1Speed += 3;
                _boat2Speed += 5;
                _planeSpeed += 5;
                return "Ekspert";
            }
            if (skill == "Insane")
            {
                /*                Enemy1.Interval = 1;
                                Enemy2.Interval = 1;*/
                _boat1Speed += 5;
                _boat2Speed += 7;
                _planeSpeed += 7;
                return "Insane";
            }
            return "Noob";
        }


        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            var dc = e.Graphics;

            var flags = TextFormatFlags.Left | TextFormatFlags.EndEllipsis;
            var font = new Font("Stencil", 12, FontStyle.Regular);
            TextRenderer.DrawText(dc, "Poeng: " + Poeng + "\nLiv: " + _liv + "\nNivå: " + _nivaa, font,
                new Rectangle(5, 45, 200, 100), SystemColors.ControlText, flags);
#if Debug
            TextRenderer.DrawText(dc,
                "X=" + _cursX + ":" + "Y=" + _cursY + "\nSkudd X: " + _skudd.Location.X + "\nSkudd Y: " +
                _skudd.Location.Y, font, new Rectangle(150, 45, 200, 100), SystemColors.ControlText, flags);

#endif
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
#if Debug
            _cursX = e.X;
            _cursY = e.Y;
            Refresh();
#endif
        }

        private void musicOnOffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _mOffOnCount++;

            if (_mOffOnCount % 2 == 0)
                sp.PlayLooping();
            else
                sp.Stop();
        }


#if Debug

        private int _cursX;
        private int _cursY;

#endif
    }
}