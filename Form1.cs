using System;
using System.Drawing;
using System.Windows.Forms;

namespace TheLostHero
{
    public partial class Form1 : Form
    {
        private bool isStartGame = false;
        private bool isPressedAnyKey = false;
        private readonly int sizeOfMapObject = 32;
        private readonly Player player;
        private readonly Map map;
        private readonly Bot bot;
        private readonly Bitmap menu;
        private Button startButton;
        private Button exitButton;

        public Form1()
        {
            InitializeComponent();
            player = new Player(new Size(256, 256), 100, 100);
            map = new Map();
            bot = new Bot(false, 0);
            menu = new Bitmap(@"D:\Игра по C#\Графика\menu.png");

            timerForPlayAnimation.Interval = 70;
            timerForPlayAnimation.Tick += new EventHandler(Update);
            timerForPlayAnimation.Start();

            timerForUpdateMove.Interval = 1;
            timerForUpdateMove.Tick += new EventHandler(UpdateMove);
            timerForUpdateMove.Start();

            CreateButton();

            KeyDown += new KeyEventHandler(KeyBoard);
            KeyUp += new KeyEventHandler(FreeKey);

            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            Controls.Add(startButton);
            Controls.Add(exitButton);
        }

        private void CreateButton()
        {
            startButton = new Button
            {
                Location = new Point(660, 300),
                Size = new Size(235, 55),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent
            };
            startButton.Click += new EventHandler(StartGame);
            startButton.FlatAppearance.BorderSize = 0;
            startButton.FlatAppearance.MouseDownBackColor = Color.Transparent;
            startButton.FlatAppearance.MouseOverBackColor = Color.Transparent;

            exitButton = new Button
            {
                Location = new Point(660, 380),
                Size = new Size(235, 55),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent
            };
            exitButton.Click += new EventHandler(ExitGame);
            exitButton.FlatAppearance.BorderSize = 0;
            exitButton.FlatAppearance.MouseDownBackColor = Color.Transparent;
            exitButton.FlatAppearance.MouseOverBackColor = Color.Transparent;
        }

        private void ExitGame(object sender, EventArgs e)
        {
            Close();
        }

        private void StartGame(object sender, EventArgs e)
        {
            isStartGame = true;
            Controls.Clear(); // если не почистить кнопки,
                              // то нажатий на клавиатуру нет
        }

        private void Update(object sender, EventArgs e)
        {
            if (isStartGame)
            {
                if (player.currFrame == 3) player.currFrame = 0;
                player.currFrame++;
                bot.Move();
                Invalidate(); // Переотрисовка
            }
        }

        private void FreeKey(object sender, KeyEventArgs e)
        {
            isPressedAnyKey = false;
            CheckStopOrMove(e);
        }

        private void KeyBoard(object sender, KeyEventArgs e)
        {
            isPressedAnyKey = true;
            if (e.KeyCode == Keys.Escape) // При нажатии esc выходит в меню
            {
                Controls.Add(startButton);
                Controls.Add(exitButton); // нужно их обратно добавить,
                                          // иначе их не будет
                isStartGame = false;
            }
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

            if (isStartGame)
            {
                map.PresentationMap(grPaint);
                bot.PlayAnimation(grPaint);
                player.PlayAnimation(grPaint, isPressedAnyKey);
            }
            else grPaint.DrawImage(menu, new PointF(0, 0));
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timerForPlayAnimation = new Timer(components);
            this.timerForUpdateMove = new Timer(components);
            this.SuspendLayout();
            // 
            // Form1
            // 
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.Location = new Point(0, 0);
            this.DoubleBuffered = true;
            this.Name = "The Lost Hero";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.OnPaint);
            this.ResumeLayout(false);
        }
    }
}