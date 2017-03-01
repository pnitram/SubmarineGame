//Remove comment to start debug
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
        //Fields

        private int _points;
        private PictureBox _boat1;
        private int _boat1Speed;
        private PictureBox _boat2;
        private int _boat2Speed;
        private int _life;
        private string _level;
        private PictureBox _plane;
        private int _planeSpeed;
        private PictureBox _player;
        private Random _random;
        private PictureBox _torpedo;
        private bool _controlOnOff;
        private SoundPlayer _soundPlayer;
        private int _musicOnOffCount;

#if Debug

        private int _cursX;
        private int _cursY;

#endif

        public SubmarineMainForm()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            //Set main window size and sizing lock controls
            _random = new Random();
            var s = new Size(800, 600);
            ClientSize = s;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Oppsett();
        }

        private void Oppsett()
        {
            //Random values for boat startpos 
            var x = _random.Next(-200, -10);
            var x2 = _random.Next(800, 1200);

            //Initial values
            _controlOnOff = true;
            _points = 0;
            _life = 3;
            _level = "Noob";
            _boat1Speed = 5;
            _boat2Speed = 7;
            _planeSpeed = 20;
            _torpedo = new PictureBox();

            //Boat 1 object
            _boat1 = new PictureBox();
            _boat1.SetBounds(0, 25, 127, 83);
            _boat1.Image = Resources.boat1;
            _boat1.BackColor = Color.Transparent;
            _boat1.Left = x;
            Controls.Add(_boat1);

            //Boat 2 object
            _boat2 = new PictureBox();
            _boat2.SetBounds(0, 600, 38, 37);
            _boat2.Image = Resources.boat2;
            _boat2.BackColor = Color.Transparent;
            _boat2.Left = x2;
            Controls.Add(_boat2);

            //Plane object
            _plane = new PictureBox();
            _plane.SetBounds(0, 100, 93, 51);
            _plane.Image = Resources.plane;
            _plane.BackColor = Color.Transparent;
            _plane.Left = -1000;
            Controls.Add(_plane);

            //Player/submarine oibject
            _player = new PictureBox();
            _player.Image = Resources.sub;
            _player.BackColor = Color.Transparent;
            _player.SetBounds(350, 525, 93, 51);
            Controls.Add(_player);

            //Startpos of torpedo/shoot
            _torpedo.SetBounds(_player.Location.X + 28, -350, 9, 20);

            //Start music loop
            PlayBkMusic();
        }

        private void ChangeLevel()
        {
            //Change level method

            
                if (_points <= 5)
                    _level = Level("Noob");
                else if (_points > 5 && _points <= 15)
                    _level = Level("Normal");
                else if (_points > 15 && _points <=25)
                    _level = Level("Expert");
                else if (_points > 25)
                    _level = Level("Insane");
               }

        private void CheckAndRemoveLifeMinusOne()
        {
            //Method to remove life if no HIT
            if (_torpedo.Location.Y == 0)
            {
                _life--;
                if (_life == 0)
                {
                    _boat1.Dispose();
                    _boat2.Dispose();
                    _player.Dispose();
                    _plane.Dispose();
                    
                    //Opens game over and sends points as parameter
                    Form gameOverForm = new GameOverForm(_points);
                    _soundPlayer.Stop();
                    gameOverForm.ShowDialog();
                }
            }
        }

        private void PlayBkMusic()
        {
            //Method to start backgroundmusic
            _soundPlayer = new SoundPlayer(Resources.bkMusic);
            _soundPlayer.PlayLooping();
        }


        private void NewTorpedo()
        {
            //Reload torpedo
            _torpedo.Image = Resources.torpedo;
            _torpedo.BackColor = Color.Transparent;
            _torpedo.SetBounds(_player.Location.X + 35, 540, 9, 20);
            Controls.Add(_torpedo);
        }

        public string Level(string skill)
        {
            //Method to change level

            if (skill == "Noob")
                return "Noob";
            if (skill == "Normal")
            {
                _boat1Speed += 2;
                _boat2Speed += 4;
                _planeSpeed += 4;
                return "Normal";
            }

            if (skill == "Expert")
            {
                _boat1Speed += 3;
                _boat2Speed += 5;
                _planeSpeed += 5;
                return "Expert";
            }
            if (skill == "Insane")
            {
                _boat1Speed += 5;
                _boat2Speed += 7;
                _planeSpeed += 7;
                return "Insane";
            }
            return "Noob";
        }

        private void EnemyTimer1Tick(object sender, EventArgs e)
        {
            //Boat movment timer
            _boat1.Top = 350;
            _boat1.Left += _boat1Speed;

            //Moves boat to random location
            if (_boat1.Location.X >= 900)
                _boat1.Left = new Random().Next(-900, -300);

            if (_boat1.Location.X >= 1500)
                _boat1.Left = 800;

            //Remove one life if torpedo moves passed Y=0
            CheckAndRemoveLifeMinusOne();

            //Moves torpedo back if no hit and turns control on
            if (!_torpedo.Bounds.IntersectsWith(_boat1.Bounds))
            {
                if (_torpedo.Location.Y <= -200)
                {
                    _controlOnOff = true;

                    _torpedo.SetBounds(_player.Location.X + 28, -350, 20, 20);
                }
            }

            //HIT -> Points + 1, moves boat out of view, moves torpedo out of view
            else
            {
                _boat1.Left = 1500;
                _torpedo.Location = new Point(_torpedo.Location.X, -150);
                _controlOnOff = true;
                _points++;

                // If HIT --> change level tester
                ChangeLevel();
            }
#if Debug
            Console.WriteLine("boat1 X: " + _boat1.Location.X);
            Console.WriteLine("boat1 Y: " + _boat1.Location.Y);
#endif
            Refresh();
        }

        private void EnemyTimer2Tick(object sender, EventArgs e)
        {
            //Boat movment timer
            _boat2.Top = 290;
            _boat2.Left -= _boat2Speed;

            //Moves boat to random location
            if (_boat2.Location.X <= -100)
                _boat2.Left = new Random().Next(900, 1300);

            //Moves torpedo back if no hit and turns control on
            if (!_torpedo.Bounds.IntersectsWith(_boat2.Bounds))
            {
                if (_torpedo.Location.Y <= -200)
                {
                    _controlOnOff = true;
                    _torpedo.SetBounds(_player.Location.X + 28, -350, 20, 20);
                }
            }

            //HIT -> Points + 1, moves boat out of view, moves torpedo out of view
            else
            {
                
                _boat2.Left = 900;
                _torpedo.Location = new Point(_torpedo.Location.X, -150);
                _controlOnOff = true;
                _points += 3;

                // If HIT --> change level tester
                ChangeLevel();
            }

#if Debug
            Console.WriteLine("boat2 X: " + _boat2.Location.X);
            Console.WriteLine("boat2 Y: " + _boat2.Location.Y);
            Console.WriteLine("torpedo y: " + _torpedo.Location.Y);
#endif
            Refresh();
        }


        private void planeTimer_Tick(object sender, EventArgs e)
        {
            //Boat movment timer
            _plane.Top = 100;
            _plane.Left += _planeSpeed;
#if Debug
            Console.WriteLine("Plane X: " + _plane.Location.X);
#endif
            //Moves plane to random location
            if (_plane.Location.X >= 900)
                _plane.Left = new Random().Next(-1500, -300);

            if (_plane.Location.X >= 1500)
                _plane.Left = 800;

            //Moves torpedo back if no hit and turns control on
            if (!_torpedo.Bounds.IntersectsWith(_plane.Bounds))
            {
                if (_torpedo.Location.Y <= -200)
                {
                    _controlOnOff = true;

                    _torpedo.SetBounds(_player.Location.X + 28, -350, 20, 20);
                }
            }

            //HIT -> Points + 1, moves boat out of view, moves torpedo out of view
            else
            {
                _plane.Left = -1500;
                _torpedo.Location = new Point(_torpedo.Location.X, -150);
                _controlOnOff = true;
                _points += 5;

                // If HIT --> change level tester
                ChangeLevel();
            }
#if Debug
            Console.WriteLine("boat1 X: " + _boat1.Location.X);
            Console.WriteLine("boat1 Y: " + _boat1.Location.Y);
#endif
            Refresh();
        }

        private void skuddTimer_Tick(object sender, EventArgs e)
        {
            //Turns off control while torpedo is in motion
            _controlOnOff = false;
            _torpedo.Location = new Point(_torpedo.Location.X, _torpedo.Location.Y - 30);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //Enables keyboard control of submarine if _controlOnOff is true
            if (_controlOnOff)

                if (e.KeyCode == Keys.Left)
                {
                    _player.Left -= 10;
#if Debug
                    Console.WriteLine(_player.Left);
#endif
                    if (_player.Left <= 0)
                    {
                        _player.Left = 10;
#if Debug
                        Console.WriteLine("Player: " + _player.Left);
#endif
                    }
                    else if (_player.Left >= 750)
                    {
                        _player.Left = 745;
#if Debug
                        Console.WriteLine("Player: " + _player.Left);
#endif
                    }
                }

                else if (e.KeyCode == Keys.Right)
                {
                    _player.Left += 10;
#if Debug
                    Console.WriteLine("Player: " + _player.Left);
#endif

                    if (_player.Left <= 0)
                    {
                        _player.Left = 10;

#if Debug
                        Console.WriteLine("Player: " + _player.Left);
#endif
                    }
                    else if (_player.Left >= 720)
                    {
                        _player.Left = 715;
#if Debug
                        Console.WriteLine("Player: " + _player.Left);
#endif
                    }
                }
                else if (e.KeyCode == Keys.Space)
                {
                    //Fires torpedo
                    NewTorpedo();
                    _controlOnOff = false;
                }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //Displays game information
            var dc = e.Graphics;

            var flags = TextFormatFlags.Left | TextFormatFlags.EndEllipsis;
            var font = new Font("Stencil", 12, FontStyle.Regular);
            TextRenderer.DrawText(dc, "Points: " + _points + "\nLives left: " + _life + "\nLevel: " + _level, font,
                new Rectangle(5, 45, 200, 100), SystemColors.ControlText, flags);

            //Debug: Torpedo location
#if Debug
            TextRenderer.DrawText(dc,
                "X=" + _cursX + ":" + "Y=" + _cursY + "\nTorpedo X: " + _torpedo.Location.X + "\nTorpedo Y: " +
                _torpedo.Location.Y, font, new Rectangle(150, 45, 200, 100), SystemColors.ControlText, flags);
#endif
        }

        private void NewGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            //Debug: Shows cursor location
#if Debug
            _cursX = e.X;
            _cursY = e.Y;
            Refresh();
#endif
        }

        private void MusicOnOffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Toggels music On/Off

            _musicOnOffCount++;

            if (_musicOnOffCount % 2 == 0)
                _soundPlayer.PlayLooping();
            else
                _soundPlayer.Stop();
        }

        private void ExitMenuItem1_Click(object sender, EventArgs e)
        {
            //Close menuitem
            Close();
        }

        private void HighscordMenuItem1_Click(object sender, EventArgs e)
        {
            //Open highscore
            using (Form highScore = new HighScoreForm())
            {
                highScore.ShowDialog();
            }
        }
    }
}