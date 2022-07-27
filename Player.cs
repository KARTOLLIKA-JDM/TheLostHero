using System.Drawing;

namespace TheLostHero
{
    internal class Player
    {
        private readonly Font font;
        private readonly SolidBrush brush;
        public readonly Image playerImage;
        public static Point location;
        public static readonly Size sizeHero = new Size(256, 256);
        public int currAnimation = 4;
        public int currFrame = 0;
        public const int speedPlayer = 3;
        private static Point delta;
        public static int CountCoins;

        public Player(int _x, int _y)
        {
            font = new Font("Arial", 16);
            brush = new SolidBrush(Color.Black);
            CountCoins = 0;
            playerImage = new Bitmap(@"D:\Игра по C#\Графика\Heroe (2).png");
            location.X = _x;
            location.Y = _y;
            delta = new Point(0, 0);
        }

        public void UpdateCurrFrame(Player player)
        {
            if (player.currFrame == 3) player.currFrame = 0;
            player.currFrame++;
        }

        public void UpdatePosition(Player player, int sizeOfMapObject, int Height, int Width)
        {
            switch (player.currAnimation)
            {
                case 0:
                    player.Down(Height, sizeOfMapObject, Map.Height);
                    break;
                case 1:
                    player.Up(Height, sizeOfMapObject, Map.Height);
                    break;
                case 2:
                    player.Left(Width, sizeOfMapObject, Map.Width);
                    break;
                case 3:
                    player.Right(Width, sizeOfMapObject, Map.Width);
                    break;
            }
        }

        public void ChoosingAction(Player player,string buttonpressed, bool isPressedAnyKey)
        {
            switch (buttonpressed)
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
            }
        }

        public void PresentationCountCoins(Graphics gr)
        {
            gr.DrawString(CountCoins.ToString(), font, brush, new Point(10, 10));
        }

        public void Left(int widthWindow, int sizeOfMapObject, int widthMap)
        {
            if (location.X + sizeHero.Width / 8 > 0)
                location.X -= speedPlayer;
            if (location.X > widthWindow / 2 && location.X < sizeOfMapObject * widthMap - widthWindow / 2)
            {
                delta.X += speedPlayer;
                Map.delta.X += speedPlayer;
                Bot.delta.X += speedPlayer;
            }
        }

        public void Right(int widthWindow, int sizeOfMapObject, int widthMap)
        {
            if (location.X < sizeOfMapObject * widthMap - sizeHero.Width / 2)
                location.X += speedPlayer;
            if (location.X > widthWindow / 2 && location.X < sizeOfMapObject * widthMap - widthWindow / 2)
            {
                delta.X -= speedPlayer;
                Map.delta.X -= speedPlayer;
                Bot.delta.X -= speedPlayer;
            }
        }

        public void Down(int heightWindow, int sizeOfMapObject, int heightMap)
        {
            if (location.Y < sizeOfMapObject * heightMap - sizeHero.Height / 2)
                location.Y += speedPlayer;
            if (location.Y > heightWindow / 2 && location.Y < sizeOfMapObject * heightMap - heightWindow / 2)
            {
                delta.Y -= speedPlayer;
                Map.delta.Y -= speedPlayer;
                Bot.delta.Y -= speedPlayer;
            }
        }

        public void Up(int heightWindow, int sizeOfMapObject, int heightMap)
        {
            if (location.Y > 0)
                location.Y -= speedPlayer;
            if (location.Y > heightWindow / 2 && location.Y < sizeOfMapObject * heightMap - heightWindow / 2)
            {
                delta.Y += speedPlayer;
                Map.delta.Y += speedPlayer;
                Bot.delta.Y += speedPlayer;
            }
        }

        public void PlayAnimation(Graphics gr, bool isPressedAnyKey)
        {
            if (isPressedAnyKey)
            {
                if (currAnimation <= 3 && currAnimation != -1)
                {
                    gr.DrawImage(playerImage, location.X + delta.X, location.Y + delta.Y,
                        new Rectangle(new Point(256 * currFrame, 256 * currAnimation), sizeHero),
                        GraphicsUnit.Pixel);
                }
            }
            else
            {
                currFrame = 0;
                if (currAnimation > 3)
                {
                    gr.DrawImage(playerImage, location.X + delta.X, location.Y + delta.Y,
                        new Rectangle(new Point(256 * currFrame, 256 * (currAnimation - 4)), sizeHero),
                        GraphicsUnit.Pixel);
                }
            }
        }
    }
}