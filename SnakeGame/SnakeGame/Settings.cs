using System;
using System.IO;

namespace SnakeGame
{
    class Settings
    {
        private const string SETTINGSPATH = "SnakeGame.settings";
        public static ConsoleKey LeftKey { get; set; } = ConsoleKey.LeftArrow;
        public static ConsoleKey DownKey { get; set; } = ConsoleKey.DownArrow;
        public static ConsoleKey RightKey { get; set; } = ConsoleKey.RightArrow;
        public static ConsoleKey UpKey { get; set; } = ConsoleKey.UpArrow;

        public static int MainMapWidth { get; set; } = 20;
        public static int MainMapHeight { get; set; } = 20;
        public static int MobCount { get; set; } = 2;
        public static int FruitCount { get; set; } = 4;



        public static void Load()
        {
            if (!File.Exists(SETTINGSPATH))
            {
                Save();
                return;
            }
            string[] info;
            using (StreamReader sr = new StreamReader(SETTINGSPATH))
                info = sr.ReadToEnd().Split(';');
            if (info.Length != 8) return;
            LeftKey = (ConsoleKey)int.Parse(info[0]);
            DownKey = (ConsoleKey)int.Parse(info[1]);
            RightKey = (ConsoleKey)int.Parse(info[2]);
            UpKey = (ConsoleKey)int.Parse(info[3]);
            MainMapWidth = int.Parse(info[4]);
            MainMapHeight = int.Parse(info[5]);
            MobCount = int.Parse(info[6]);
            FruitCount = int.Parse(info[7]);
        }

        public static void Save()
        {
            string info = "";
            info += (int)LeftKey + ";";
            info += (int)DownKey + ";";
            info += (int)RightKey + ";";
            info += (int)UpKey + ";";
            info += MainMapWidth + ";";
            info += MainMapHeight + ";";
            info += MobCount + ";";
            info += FruitCount;
            using (StreamWriter sw = new StreamWriter(SETTINGSPATH))
                sw.Write(info);
        }
    }
}
