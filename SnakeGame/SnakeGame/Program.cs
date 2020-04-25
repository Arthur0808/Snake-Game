using System;
using System.Runtime.InteropServices;

namespace SnakeGame
{
    public class Program
    {
        static void Main(string[] args)
        {
            IntPtr sysMenu = GetSystemMenu(GetConsoleWindow(), false);
            DeleteMenu(sysMenu, 0xF020, 0x00000000);
            DeleteMenu(sysMenu, 0xF030, 0x00000000);
            DeleteMenu(sysMenu, 0xF000, 0x00000000);
            Settings.Load();
            MainMenu();
        }

        public static void MainMenu()
        {
            Console.Title = "Snake Game";
            Console.SetWindowSize(40, 13);
            Console.SetBufferSize(40, 13);
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(15, 0);
            Console.Write("Snake Game");
            Console.SetCursorPosition(17, 12);
            Console.Write("By Arthur Baghdasaryan");
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(0, 2);
            Console.Write(">> Play");
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, 4);
            Console.Write("   Settings");
            Console.SetCursorPosition(0, 6);
            Console.Write("   Quit");
            sbyte choise = 0;
            ConsoleKey pressedKey;
            do
            {
                pressedKey = Console.ReadKey(true).Key;
                if (pressedKey == ConsoleKey.UpArrow) ChangeChoise(-1);
                else if (pressedKey == ConsoleKey.DownArrow) ChangeChoise(1);
            } while (pressedKey != ConsoleKey.Enter);
            switch (choise)
            {
                case 0:
                    GameManager.Start();
                    break;
                case 1:
                    MainSettings();
                    break;
                case 2:
                    Environment.Exit(0);
                    break;
            }

            void ChangeChoise(sbyte step)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(0, choise * 2 + 2);
                if (choise == 0) Console.Write("   Play");
                else if (choise == 1) Console.Write("   Settings");
                else Console.Write("   Quit");
                choise += step;
                if (choise > 2) choise = 0;
                else if (choise < 0) choise = 2;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(0, choise * 2 + 2);
                if (choise == 0) Console.Write(">> Play");
                else if (choise == 1) Console.Write(">> Settings");
                else Console.Write(">> Quit");
            }
        }

        static void MainSettings()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(15, 0);
            Console.Write("Snake Game");
            Console.SetCursorPosition(17, 12);
            Console.Write("By Arthur Baghdasaryan");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(0, 2);
            Console.Write(">> Control");
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, 4);
            Console.Write("   Map");
            Console.SetCursorPosition(0, 6);
            Console.Write("   Exit");
            sbyte choise = 0;
            ConsoleKey pressedKey;
            do
            {
                pressedKey = Console.ReadKey(true).Key;
                if (pressedKey == ConsoleKey.Escape)
                {
                    choise = 2;
                    break;
                }
                if (pressedKey == ConsoleKey.UpArrow) ChangeChoise(-1);
                else if (pressedKey == ConsoleKey.DownArrow) ChangeChoise(1);
            } while (pressedKey != ConsoleKey.Enter);
            switch (choise)
            {
                case 0:
                    ControlSettings();
                    break;
                case 1:
                    MapSettings();
                    break;
                case 2:
                    MainMenu();
                    break;
            }

            void ChangeChoise(sbyte step)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(0, choise * 2 + 2);
                if (choise == 0) Console.Write("   Control");
                else if (choise == 1) Console.Write("   Map");
                else Console.Write("   Exit");
                choise += step;
                if (choise > 2) choise = 0;
                else if (choise < 0) choise = 2;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(0, choise * 2 + 2);
                if (choise == 0) Console.Write(">> Control");
                else if (choise == 1) Console.Write(">> Map");
                else Console.Write(">> Exit");
            }
        }

        static void ControlSettings()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(15, 0);
            Console.Write("Snake Game");
            Console.SetCursorPosition(17, 12);
            Console.Write("By Arthur Baghdasaryan");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(0, 2);
            Console.Write(">> LeftKey");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($": {Settings.LeftKey}");
            Console.SetCursorPosition(0, 4);
            Console.Write($"   DownKey: {Settings.DownKey}");
            Console.SetCursorPosition(0, 6);
            Console.Write($"   RightKey: {Settings.RightKey}");
            Console.SetCursorPosition(0, 8);
            Console.Write($"   UpKey: {Settings.UpKey}");
            Console.SetCursorPosition(0, 10);
            Console.Write("   Exit");
            sbyte choise = 0;
            ConsoleKey pressedKey;
            Loop();

            void Loop()
            {
                do
                {
                    pressedKey = Console.ReadKey(true).Key;
                    if (pressedKey == ConsoleKey.Escape)
                    {
                        choise = 4;
                        break;
                    }
                    if (pressedKey == ConsoleKey.UpArrow) ChangeChoise(-1);
                    else if (pressedKey == ConsoleKey.DownArrow) ChangeChoise(1);
                } while (pressedKey != ConsoleKey.Enter);
                switch (choise)
                {
                    case 0:
                        Console.SetCursorPosition(12, 2);
                        Console.Write("                 ");
                        Settings.LeftKey = Console.ReadKey(true).Key;
                        Console.SetCursorPosition(12, 2);
                        Console.Write(Settings.LeftKey);
                        Loop();
                        break;
                    case 1:
                        Console.SetCursorPosition(12, 4);
                        Console.Write("                 ");
                        Settings.DownKey = Console.ReadKey(true).Key;
                        Console.SetCursorPosition(12, 4);
                        Console.Write(Settings.DownKey);
                        Loop();
                        break;
                    case 2:
                        Console.SetCursorPosition(13, 6);
                        Console.Write("                 ");
                        Settings.RightKey = Console.ReadKey(true).Key;
                        Console.SetCursorPosition(13, 6);
                        Console.Write(Settings.RightKey);
                        Loop();
                        break;
                    case 3:
                        Console.SetCursorPosition(10, 8);
                        Console.Write("                 ");
                        Settings.UpKey = Console.ReadKey(true).Key;
                        Console.SetCursorPosition(10, 8);
                        Console.Write(Settings.UpKey);
                        Loop();
                        break;
                    case 4:
                        Settings.Save();
                        MainSettings();
                        break;
                }
            }

            void ChangeChoise(sbyte step)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(0, choise * 2 + 2);
                if (choise == 0) Console.Write($"   LeftKey: {Settings.LeftKey}");
                else if (choise == 1) Console.Write($"   DownKey: {Settings.DownKey}");
                else if (choise == 2) Console.Write($"   RightKey: {Settings.RightKey}");
                else if (choise == 3) Console.Write($"   UpKey: {Settings.UpKey}");
                else Console.Write("   Exit");
                choise += step;
                if (choise > 4) choise = 0;
                else if (choise < 0) choise = 4;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(0, choise * 2 + 2);
                if (choise == 0)
                {
                    Console.Write(">> LeftKey");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($": {Settings.LeftKey}");
                }
                else if (choise == 1)
                {
                    Console.Write(">> DownKey");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($": {Settings.DownKey}");
                }
                else if (choise == 2)
                {
                    Console.Write(">> RightKey");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($": {Settings.RightKey}");
                }
                else if (choise == 3)
                {
                    Console.Write(">> UpKey");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($": {Settings.UpKey}");
                }
                else Console.Write(">> Exit");
            }
        }

        static void MapSettings()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(15, 0);
            Console.Write("Snake Game");
            Console.SetCursorPosition(17, 12);
            Console.Write("By Arthur Baghdasaryan");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(0, 2);
            Console.Write(">> MapWidth");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($": {Settings.MainMapWidth}");
            Console.SetCursorPosition(0, 4);
            Console.Write($"   MapHeight: {Settings.MainMapHeight}");
            Console.SetCursorPosition(0, 6);
            Console.Write($"   MobCount: {Settings.MobCount}");
            Console.SetCursorPosition(0, 8);
            Console.Write($"   FruitCount: {Settings.FruitCount}");
            Console.SetCursorPosition(0, 10);
            Console.Write("   Exit");
            sbyte choise = 0;
            ConsoleKey pressedKey;
            Loop();

            void Loop()
            {
                do
                {
                    pressedKey = Console.ReadKey(true).Key;
                    if (pressedKey == ConsoleKey.Escape)
                    {
                        choise = 4;
                        break;
                    }
                    if (pressedKey == ConsoleKey.UpArrow) ChangeChoise(-1);
                    else if (pressedKey == ConsoleKey.DownArrow) ChangeChoise(1);
                } while (pressedKey != ConsoleKey.Enter);
                int readed;
                switch (choise)
                {
                    case 0:
                        Console.SetCursorPosition(13, 2);
                        Console.Write("                 ");
                        Console.SetCursorPosition(13, 2);
                        int.TryParse(Console.ReadLine(), out readed);
                        Settings.MainMapWidth = readed;
                        Loop();
                        break;
                    case 1:
                        Console.SetCursorPosition(14, 4);
                        Console.Write("                 ");
                        Console.SetCursorPosition(14, 4);
                        int.TryParse(Console.ReadLine(), out readed);
                        Settings.MainMapHeight = readed;
                        Loop();
                        break;
                    case 2:
                        Console.SetCursorPosition(13, 6);
                        Console.Write("                 ");
                        Console.SetCursorPosition(13, 6);
                        int.TryParse(Console.ReadLine(), out readed);
                        Settings.MobCount = readed;
                        Loop();
                        break;
                    case 3:
                        Console.SetCursorPosition(15, 8);
                        Console.Write("                 ");
                        Console.SetCursorPosition(15, 8);
                        int.TryParse(Console.ReadLine(), out readed);
                        Settings.FruitCount = readed;
                        Loop();
                        break;
                    case 4:
                        Settings.Save();
                        MainSettings();
                        break;
                }
            }

            void ChangeChoise(sbyte step)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(0, choise * 2 + 2);
                if (choise == 0) Console.Write($"   MapWidth: {Settings.MainMapWidth}");
                else if (choise == 1) Console.Write($"   MapHeight: {Settings.MainMapHeight}");
                else if (choise == 2) Console.Write($"   MobCount: {Settings.MobCount}");
                else if (choise == 3) Console.Write($"   FruitCount: {Settings.FruitCount}");
                else Console.Write("   Exit");
                choise += step;
                if (choise > 4) choise = 0;
                else if (choise < 0) choise = 4;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(0, choise * 2 + 2);
                if (choise == 0)
                {
                    Console.Write(">> MapWidth");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($": {Settings.MainMapWidth}");
                }
                else if (choise == 1)
                {
                    Console.Write(">> MapHeight");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($": {Settings.MainMapHeight}");
                }
                else if (choise == 2)
                {
                    Console.Write(">> MobCount");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($": {Settings.MobCount}");
                }
                else if (choise == 3)
                {
                    Console.Write(">> FruitCount");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($": {Settings.FruitCount}");
                }
                else Console.Write(">> Exit");
            }
        }
        [DllImport("user32.dll")]
        public static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();
    }
}