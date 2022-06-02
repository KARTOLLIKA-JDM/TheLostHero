using System.Drawing;

namespace TheLostHero
{
    internal class Player
    {
        public readonly Image playerImage;
        public static Point location;
        public readonly Size sizeHero;
        public int currAnimation = 4;
        public int currFrame = 0;
        public const int speedPlayer = 3;
        private static Point delta;

        public Player(Size _sizeHero, int _x, int _y)
        {
            playerImage = new Bitmap(@"D:\Игра по C#\Графика\Heroe (2).png");
            sizeHero = _sizeHero;
            location.X = _x;
            location.Y = _y;
            delta = new Point(0, 0);
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
