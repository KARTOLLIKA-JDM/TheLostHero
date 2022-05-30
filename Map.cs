using System.Drawing;
using System.IO;

namespace TheLostHero
{
    internal class Map
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public string[] CurrMap { get; private set; }

        public static Point delta;
        private readonly string[] maps;
        private int currMapIndex;
        private readonly Image spritesMap;
        private int currColumn; // столбец
        private int currRow; // строка
        private bool isSaveMap;
        private bool isChangeMap;
        private readonly Bitmap bitMAp;

        public Map()
        {
            spritesMap = new Bitmap(@"D:\Игра по C#\Графика\32x32_map_tile.png");
            maps = Directory.GetFiles(@"D:\Игра по C#\Maps", @"*.txt");
            CurrMap = File.ReadAllLines(@"D:\Игра по C#\Maps\level1.txt");
            Height = CurrMap.Length;
            Width = CurrMap[0].Length;
            currMapIndex = 0;
            isSaveMap = false;
            bitMAp = new Bitmap(3200,3200);
        }

        private void ChangeSizeMap()
        {
            Height = CurrMap.Length;
            Width = CurrMap[0].Length;
        }

        public void ChangeMap()
        {
            isChangeMap = true;
            currMapIndex++;
            if (maps.Length == currMapIndex) currMapIndex = 0;
            CurrMap = File.ReadAllLines(maps[currMapIndex]);
            ChangeSizeMap();
            CreateMap();
        }

        private void ChoosePartMap(char symbolMap)
        {
            switch (symbolMap)
            {
                case 'D': // доска
                    currRow = 1;
                    currColumn = 8;
                    break;
                case 'G': // трава
                    currRow = 2;
                    currColumn = 1;
                    break;
                case 'P': // пень
                    currRow = 8;
                    currColumn = 1;
                    break;
                case 'S': // песок
                    currRow = 6;
                    currColumn = 4;
                    break;
                case 'C': // камень
                    currRow = 7;
                    currColumn = 2;
                    break;
                case 'K': // куст
                    currRow = 12;
                    currColumn = 3;
                    break;
                case 'L': // лестница
                    currRow = 8;
                    currColumn = 13;
                    break;
                case 'W': // вода
                    currRow = 16;
                    currColumn = 7;
                    break;
            }
        }

        public void PresentationMap(Graphics gr)
        {
            if (!isSaveMap) CreateMap();
            if (isChangeMap)
            {
                gr.Clear(Color.White);
                gr.DrawImage(bitMAp, new Point(0, 0));
            }
            else gr.DrawImage(bitMAp, new Point(0 + delta.X, 0 + delta.Y));
            isChangeMap = false;
        }

        public void CreateMap()
        {
            var grFromImage = Graphics.FromImage(bitMAp);
            for (int i = 0; i < CurrMap.Length; i++)
            {
                var stringMap = CurrMap[i];
                for (int j = 0; j < stringMap.Length; j++)
                {
                    ChoosePartMap(stringMap[j]);
                    grFromImage.DrawImage(spritesMap, j * 32, i * 32,
                                 new Rectangle(new Point(32 * currColumn, 32 * currRow), new Size(32, 32)),
                                 GraphicsUnit.Pixel);
                }
            }
            isSaveMap = true;
            bitMAp.Save(@"D:\Игра по C#\Maps\Текущий уровень.png", System.Drawing.Imaging.ImageFormat.Png);
        }
    }
}