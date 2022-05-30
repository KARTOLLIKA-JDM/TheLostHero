using System;
using System.Drawing;
using System.IO;

namespace TheLostHero
{
    // сделать массив, где будут храниться положения ботов на каждом уровне
    internal class Bot
    {
        private Image botImage;
        private int speedBot;
        private int currAnimation = 4;
        private int currFrame = 0;
        private Point location;
        private bool isBotMove;
        private bool isEndMove;
        private readonly Random randomChoosePath;
        private readonly Size botSize;
        private readonly string[] dirImagesBot;
        public static Point delta;

        public Bot(bool _isAngryBot, int _numberBot)
        {
            dirImagesBot = Directory.GetFiles(@"D:\Игра по C#\botsImages", @"*.png");
            ChooseSpeedBot(_isAngryBot);
            ChooseImageBot(_numberBot);
            location.X = 500;
            location.Y = 500;
            randomChoosePath = new Random();
            botSize = new Size(256, 256);
            isBotMove = true;
            isEndMove = true;
            delta = new Point(0, 0);
        }

        private void ChooseSpeedBot(bool isAngryBot)
        {
            if (isAngryBot) speedBot = 3;
            speedBot = 1;
        }

        public void Move()
        {
            if (isEndMove)
                ChoosePath();
        }

        private void Left()
        {
            currAnimation = 2;
            for (int i = 0; i < 10; i++)
            {
                if (currFrame == 3) currFrame = 0;
                currFrame++;
                location.X -= speedBot;
            }
            isEndMove = true;
            StopBot();
        }

        private void Right()
        {
            currAnimation = 3;
            for (int i = 0; i < 10; i++)
            {
                if (currFrame == 3) currFrame = 0;
                currFrame++;
                location.X += speedBot;
            }
            isEndMove = true;
            StopBot();
        }

        private void Down()
        {
            currAnimation = 0;
            for (int i = 0; i < 10; i++)
            {
                if (currFrame == 3) currFrame = 0;
                currFrame++;
                location.Y += speedBot;
            }
            isEndMove = true;
            StopBot();
        }

        private void Up()
        {
            currAnimation = 1;
            for (int i = 0; i < 10; i++)
            {
                if (currFrame == 3) currFrame = 0;
                currFrame++;
                location.Y -= speedBot;
            }
            isEndMove = true;
            StopBot();
        }

        private void StopBot()
        {
            //isBotMove = false;
        }

        public void PlayAnimation(Graphics gr)
        {
            if (isBotMove)
            {
                if (currAnimation <= 3 && currAnimation != -1)
                {
                    gr.DrawImage(botImage, location.X + delta.X, location.Y + delta.Y,
                        new Rectangle(new Point(256 * currFrame, 256 * currAnimation), botSize),
                        GraphicsUnit.Pixel);
                }
            }
            else
            {
                currFrame = 0;
                if (currAnimation > 3)
                {
                    gr.DrawImage(botImage, location.X + delta.X, location.Y + delta.Y,
                        new Rectangle(new Point(256 * currFrame, 256 * (currAnimation - 4)), botSize),
                        GraphicsUnit.Pixel);
                }
            }
        }

        private void ChoosePath()
        {
            isBotMove = true;
            isEndMove = true;
            int path = randomChoosePath.Next(0, 4);
            switch (path)
            {
                case 0:
                    Left();
                    break;
                case 1:
                    Up();
                    break;
                case 2:
                    Down();
                    break;
                case 3:
                    Right();
                    break;
                case 4:
                    isBotMove = false;
                    StopBot();
                    break;
            }
        }

        private void ChooseImageBot(int numberBot)
        {
            switch (numberBot)
            {
                case 0:
                    botImage = new Bitmap(dirImagesBot[0]);
                    break;
                case 1:
                    botImage = new Bitmap(dirImagesBot[1]);
                    break;
                case 2:
                    botImage = new Bitmap(dirImagesBot[2]);
                    break;
            }
        }

        //private void Move(char direction)
        //{
        //    switch(direction)
        //    {
        //        case 'L':
        //            currAnimation = 2;
        //            break;
        //        case 'R':
        //            currAnimation = 3;
        //            break;
        //        case 'U':
        //            currAnimation = 1;
        //            break;
        //        case 'D':
        //            currAnimation = 0;
        //            break;
        //    }

        //    for (int i = 0; i < 10; i++)
        //    {
        //        if (currFrame == 3) currFrame = 0;
        //        currFrame++;

        //        location.X -= speedBot;
        //    }

        //    ChoosePath();
        //}
    }
}