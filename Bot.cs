using System;
using System.Drawing;
using System.IO;

namespace TheLostHero
{
    // сделать массив, где будут храниться положения ботов на каждом уровне
    internal class Bot
    {
        private const int sizeOfMapObject = 32;
        private int rightPath;
        private int countSteps;
        private Image botImage;
        private int speedBot;
        private int currAnimation = 4;
        private int currFrame = 0;
        private Point location;
        private bool isPlayerNear;
        private bool isBotMove;
        private bool isStopBotEnd;
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
            isPlayerNear = false;
            isStopBotEnd = false;
            delta = new Point(0, 0);
            countSteps = 0;
            rightPath = 0;
        }

        private void DefinitionLocationPlayer()
        {
            double distance = Math.Sqrt(((Player.location.X - location.X)* (Player.location.X - location.X) +
                                        (Player.location.Y - location.Y)* (Player.location.Y - location.Y)));
            if (distance < 200)
                isPlayerNear = true;
            else isPlayerNear = false;
        }

        private void ChooseSpeedBot(bool isAngryBot)
        {
            if (isAngryBot) speedBot = 3;
            speedBot = 1;
        }

        public void Move()
        {
            DefinitionLocationPlayer();
            if (isPlayerNear)
                currAnimation = 4;
            else ChoosePath();
        }

        private void Left()
        {
            countSteps++;
            currAnimation = 2;
            for (int i = 0; i < 5; i++)
            {
                if (location.X + botSize.Width / 8 > 0)
                {
                    if (currFrame == 3) currFrame = 0;
                    currFrame++;
                    location.X -= speedBot;
                }
            }
        }

        private void Right()
        {
            currAnimation = 3;
            for (int i = 0; i < 5; i++)
            {
                if(location.X < sizeOfMapObject * Map.Width - botSize.Width / 2)
                {
                    if (currFrame == 3) currFrame = 0;
                    currFrame++;
                    location.X += speedBot;
                }
            }
            countSteps++;
        }

        private void Down()
        {
            currAnimation = 0;
            for (int i = 0; i < 5; i++)
            {
                if (location.Y < sizeOfMapObject * Map.Height - botSize.Height / 2)
                {
                    if (currFrame == 3) currFrame = 0;
                    currFrame++;
                    location.Y += speedBot;
                }
            }
            countSteps++;
        }

        private void Up()
        {
            currAnimation = 1;
            for (int i = 0; i < 5; i++)
            {
                if (location.Y > 0)
                {
                    if (currFrame == 3) currFrame = 0;
                    currFrame++;
                    location.Y -= speedBot;
                }
            }
            countSteps++;
        }

        private void StopBot()
        {
            if (countSteps + 1 == 20) isStopBotEnd = true;
            countSteps++;
            currAnimation = 4;
        }

        private void ChoosePath()
        {
            isBotMove = true;
            if(countSteps == 10 || countSteps == 20)
            {
                rightPath = randomChoosePath.Next(0, 4);
                if (isStopBotEnd)
                {
                    countSteps = 0;
                    isStopBotEnd = false;
                }
                else
                {
                    StopBot();
                    rightPath = 4;
                }
            }

            switch (rightPath)
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
                        new Rectangle(new Point(0, 256 * (currAnimation - 4)), botSize),
                        GraphicsUnit.Pixel);
                }
            }
        }
    }
}