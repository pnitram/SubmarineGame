namespace SubmarineGame
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.hovedmenyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.highscoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Enemy1 = new System.Windows.Forms.Timer(this.components);
            this.Enemy2 = new System.Windows.Forms.Timer(this.components);
            this.playerTimer = new System.Windows.Forms.Timer(this.components);
            this.skuddTimer = new System.Windows.Forms.Timer(this.components);
            this.planeTimer = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hovedmenyToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1463, 49);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // hovedmenyToolStripMenuItem
            // 
            this.hovedmenyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newGameToolStripMenuItem,
            this.highscoreToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.hovedmenyToolStripMenuItem.Name = "hovedmenyToolStripMenuItem";
            this.hovedmenyToolStripMenuItem.Size = new System.Drawing.Size(191, 45);
            this.hovedmenyToolStripMenuItem.Text = "Hovedmeny";
            // 
            // newGameToolStripMenuItem
            // 
            this.newGameToolStripMenuItem.Name = "newGameToolStripMenuItem";
            this.newGameToolStripMenuItem.Size = new System.Drawing.Size(279, 46);
            this.newGameToolStripMenuItem.Text = "New Game";
            this.newGameToolStripMenuItem.Click += new System.EventHandler(this.newGameToolStripMenuItem_Click);
            // 
            // highscoreToolStripMenuItem
            // 
            this.highscoreToolStripMenuItem.Name = "highscoreToolStripMenuItem";
            this.highscoreToolStripMenuItem.Size = new System.Drawing.Size(279, 46);
            this.highscoreToolStripMenuItem.Text = "Highscore";
            this.highscoreToolStripMenuItem.Click += new System.EventHandler(this.highscordMenuItem1_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(279, 46);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitMenuItem1_Click);
            // 
            // Enemy1
            // 
            this.Enemy1.Enabled = true;
            this.Enemy1.Interval = 20;
            this.Enemy1.Tick += new System.EventHandler(this.EnemyTimer1Tick);
            // 
            // Enemy2
            // 
            this.Enemy2.Enabled = true;
            this.Enemy2.Interval = 20;
            this.Enemy2.Tick += new System.EventHandler(this.EnemyTimer2Tick);
            // 
            // playerTimer
            // 
            this.playerTimer.Interval = 20;
            this.playerTimer.Tick += new System.EventHandler(this.playerTimer_Tick);
            // 
            // skuddTimer
            // 
            this.skuddTimer.Enabled = true;
            this.skuddTimer.Interval = 20;
            this.skuddTimer.Tick += new System.EventHandler(this.skuddTimer_Tick);
            // 
            // planeTimer
            // 
            this.planeTimer.Enabled = true;
            this.planeTimer.Interval = 20;
            this.planeTimer.Tick += new System.EventHandler(this.planeTimer_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::SubmarineGame.Properties.Resources.vannBk3;
            this.ClientSize = new System.Drawing.Size(1463, 974);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(1000, 600);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem hovedmenyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem highscoreToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Timer Enemy1;
        private System.Windows.Forms.Timer Enemy2;
        private System.Windows.Forms.Timer playerTimer;
        private System.Windows.Forms.Timer skuddTimer;
        private System.Windows.Forms.Timer planeTimer;
    }
}

