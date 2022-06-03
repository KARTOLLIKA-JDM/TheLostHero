using System;
using System.Collections.Generic;
using System.Drawing;

namespace TheLostHero
{
    internal class Coins
    {
        private readonly List<Point> locationsCoins;
        private readonly Point location;
        private readonly Image imageCoin;
        private bool isCollectCoin;
        private readonly Size sizeCoin;
        private readonly List<Coins> coins = new List<Coins>();
        private readonly int countCoins;

        public Coins(int _countCoins)
        {
            locationsCoins = new List<Point> { new Point(500, 500),
                new Point(500, 2000), new Point(500, 200),
                new Point(200, 1500), new Point(1000, 100),
                new Point(520, 800), new Point(1800,800),
                new Point(1800,1500), new Point(1000,1000),
                new Point(1050,1060),};
            countCoins = _countCoins;
            imageCoin = new Bitmap(@"D:\Игра по C#\Графика\Безымянный.png");
            isCollectCoin = false;
            sizeCoin = new Size(32, 32);
        }

        public Coins(Point _location)
        {
            location = _location;
            imageCoin = new Bitmap(@"D:\Игра по C#\Графика\Безымянный.png");
            isCollectCoin = false;
            sizeCoin = new Size(32, 32);
        }

        public void InitializationCoins()
        {
            for (int i = 0; i < countCoins; i++)
            {
                coins.Add(new Coins(locationsCoins[i]));
            }
        }

        public void PresentationImageCoin(Graphics gr)
        {
            for (int i = 0; i < coins.Count; i++)
            {
                if (!coins[i].isCollectCoin)
                {
                    coins[i].Collision();
                    gr.DrawImage(coins[i].imageCoin, coins[i].location.X + Map.delta.X, coins[i].location.Y + Map.delta.Y);
                }
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