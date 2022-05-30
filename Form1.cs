using System;
using System.Drawing;
using System.Windows.Forms;

namespace TheLostHero
{
    public partial class Form1 : Form
    {
        private bool isPressedAnyKey = false;
        private readonly int sizeOfMapObject = 32;
        private readonly Player player;
        private readonly Map map;
        private readonly Bot bot;

        public Form1()
        {
            InitializeComponent();
            player = new Player(new Size(256, 256), 100, 100);
            map = new Map();
            bot = new Bot(false, 0);

            timerForPlayAnimation.Interval = 70;
            timerForPlayAnimation.Tick += new EventHandler(Update);
            timerForPlayAnimation.Start();

            timerForUpdateMove.Interval = 1;
            timerForUpdateMove.Tick += new EventHandler(UpdateMove);
            timerForUpdateMove.Start();

            KeyDown += new KeyEventHandler(KeyBoard);
            KeyUp += new KeyEventHandler(FreeKey);

            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        private void Update(object sender, EventArgs e)
        {
            if (player.currFrame == 3) player.currFrame = 0;
            player.currFrame++;
            bot.Move();
            Invalidate(); // Переотрисовка
        }

        private void FreeKey(object sender, KeyEventArgs e)
        {
            isPressedAnyKey = false;
            CheckStopOrMove(e);
        }

        private void KeyBoard(object sender, KeyEventArgs e)
        {
            isPressedAnyKey = true;
            CheckStopOrMove(e);
        }

        private void UpdateMove(object sender, EventArgs e)
        {
            switch (player.currAnimation)
            {
                case 0:
                    player.Down(Height, sizeOfMapObject, map.Height);
                    break;
                case 1:
                    player.Up(Height, sizeOfMapObject, map.Height);
                    break;
                case 2:
                    player.Left(Width, sizeOfMapObject, map.Width);
                    break;
                case 3:
                    player.Right(Width, sizeOfMapObject, map.Width);
                    break;
            }
            Invalidate(); // Переотрисовка
        }

        private void CheckStopOrMove(KeyEventArgs e)
        {
            switch (e.KeyCode.ToString())
            {
                case "S":
                    if (isPressedAnyKey) player.currAnimation = 0;
                    else player.currAnimation = 4;
                    break;
                case "W":
                    if (isPressedAnyKey) player.currAnimation = 1;
                    else player.currAnimation = 5;
                    break;
                case "A":
                    if (isPressedAnyKey) player.currAnimation = 2;
                    else player.currAnimation = 6;
                    break;
                case "D":
                    if (isPressedAnyKey) player.currAnimation = 3;
                    else player.currAnimation = 7;
                    break;
                case "F":
                    map.ChangeMap();
                    Invalidate();
                    break;
            }
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            Graphics grPaint = e.Graphics;

            map.PresentationMap(grPaint);
            player.PlayAnimation(grPaint, isPressedAnyKey);
            bot.PlayAnimation(grPaint);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timerForPlayAnimation = new System.Windows.Forms.Timer(this.components);
            this.timerForUpdateMove = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // Form1
            // 
            this.Location = new Point(0, 0);
            this.ClientSize = new System.Drawing.Size(1920, 1080);
            this.DoubleBuffered = true;
            this.Name = "The Lost Hero";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.OnPaint);
            this.ResumeLayout(false);
        }
    }
}