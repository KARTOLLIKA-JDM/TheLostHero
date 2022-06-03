using System;
using System.Collections.Generic;
using System.Drawing;

namespace TheLostHero
{
    internal class Coins
    {
        private readonly Point location;
        private readonly Image imageCoin;
        private bool isCollectCoin;
        private readonly Size sizeCoin;
        private readonly List<Coins> obj = new List<Coins>();
        private readonly int countCoins;
        private readonly Random rndY4;
        private readonly Random rndX4;

        public Coins(int _countCoins)
        {
            countCoins = _countCoins;
            imageCoin = new Bitmap(@"D:\Игра по C#\Графика\Безымянный.png");
            isCollectCoin = false;
            sizeCoin = new Size(32, 32);
            rndX4 = new Random();
            rndY4= new Random();
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
                var x4 = rndX4.Next(100, 1000);
                var y4 = rndY4.Next(100, 1000);
                obj.Add(new Coins(new Point(x4, y4)));
            }
        }

        public void PresentationImageCoin(Graphics gr)
        {
            for (int i = 0; i < obj.Count; i++)
            {
                if (!obj[i].isCollectCoin)
                {
                    obj[i].Collision();
                    gr.DrawImage(obj[i].imageCoin, obj[i].location.X + Map.delta.X, obj[i].location.Y + Map.delta.Y);
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