using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SnakeGame
{
    class GameManager
    {
        public static byte[,] Map { get; private set; }
        public static Snake Player { get; private set; }
        public static List<Snake> Snakes { get; private set; }
        public static void Start()
        {
            Console.Clear();
            Console.SetWindowSize(Settings.MainMapWidth * 2, Settings.MainMapHeight+1);
            Console.SetBufferSize(Settings.MainMapWidth * 2, Settings.MainMapHeight+1);
            Console.ForegroundColor = ConsoleColor.White;
            Map = new byte[Settings.MainMapWidth, Settings.MainMapHeight];
            Snakes = new List<Snake>(Settings.MobCount+1);
            for (int x = 0; x < Settings.MainMapWidth; x++)
            {
                Console.SetCursorPosition(x * 2, 0);
                Console.Write("::");
                Map[x, 0] = (byte)PointType.Blcok;
                Console.SetCursorPosition(x * 2, Settings.MainMapHeight - 1);
                Console.Write("::");
                Map[x, Settings.MainMapHeight - 1] = (byte)PointType.Blcok;
            }
            for (int y = 1; y < Settings.MainMapWidth - 1; y++)
            {
                Console.SetCursorPosition(0, y);
                Console.Write("::");
                Map[0, y] = (byte)PointType.Blcok;
                Console.SetCursorPosition(Settings.MainMapWidth * 2 - 2, y);
                Console.Write("::");
                Map[Settings.MainMapWidth-1, y] = (byte)PointType.Blcok;
            }
            Player = new Snake(GetFreePoint(), 4, ConsoleColor.DarkGreen);
            Snakes.Add(Player);
            for (int i = 0; i < Settings.MobCount; i++)
                Snakes.Add(new Mob(GetFreePoint(), 4, ConsoleColor.DarkYellow));
            for (int i = 0; i < Settings.FruitCount; i++)
                GenerateFruit();
            MoveSnakes();
            while (Player.isLive)
            {
                ConsoleKey pressedKey = Console.ReadKey(true).Key;
                if (pressedKey == Settings.LeftKey) Player.ChangeDirection(Vector.Left);
                else if (pressedKey == Settings.DownKey) Player.ChangeDirection(Vector.Down);
                else if (pressedKey == Settings.RightKey) Player.ChangeDirection(Vector.Right);
                else if (pressedKey == Settings.UpKey) Player.ChangeDirection(Vector.Up);
            }
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.SetCursorPosition(Settings.MainMapWidth - 5, Settings.MainMapHeight / 2);
            Console.WriteLine("Game Over");
            Thread.Sleep(3000);
            Program.MainMenu();
        }

        public static async void MoveSnakes()
        {
            await Task.Delay(2000);
            while (Player.isLive)
            {
                for (int i = 0; i < Snakes.Count; i++)
                {
                    if (!Snakes[i].isLive)
                        Snakes.RemoveAt(i--);
                    else
                        Snakes[i].Move();
                }
                await Task.Delay(80);
            }
        }

        public static void GenerateFruit()
        {
            Vector pos = GetFreePoint();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.SetCursorPosition(pos.x * 2, pos.y);
            Console.Write("::");
            Map[pos.x, pos.y] = (byte)PointType.Fruit;
        }

        public static Vector GetFreePoint()
        {
            Vector pos = new Vector();
            do
            {
                pos.x = Random.Range(1, Settings.MainMapWidth);
                pos.y = Random.Range(1, Settings.MainMapHeight);
            } while (Map[pos.x, pos.y] != (byte)PointType.None);
            return pos;
        }
    }

    enum PointType : byte
    {
        None = 0,
        Blcok = 1,
        Fruit = 2
    }
}
