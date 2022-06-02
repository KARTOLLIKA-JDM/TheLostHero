using System.Drawing;

namespace TheLostHero
{
    internal class Coins
    {
        private readonly Point location;
        private readonly Image imageCoin;
        private bool isCollectCoin;
        private readonly Size sizeCoin;

        public Coins()
        {
            location = new Point(500, 200);
            imageCoin = new Bitmap(@"D:\Игра по C#\Графика\Безымянный.png");
            isCollectCoin = false;
            sizeCoin = new Size(32, 32);
        }

        public void PresentationImageCoin(Graphics gr)
        {
            if (!isCollectCoin)
            {
                Collision();
                gr.DrawImage(imageCoin, location);
            }
        }

        private void Collision()
        {
            if (Player.location.X + Player.sizeHero.Width / 4 > location.X &&
                Player.location.X + Player.sizeHero.Width / 4 < location.X + sizeCoin.Width &&
                Player.location.Y + Player.sizeHero.Height / 4 > location.Y &&
                Player.location.Y + Player.sizeHero.Height / 4 < location.Y + sizeCoin.Height)
            {
                Player.CountCoins++;
                isCollectCoin = true;
            }
        }
    }
}