using System;

namespace SnakeGame
{
    class Random
    {
        static System.Random random = new System.Random();
        public static int Range(int min, int max)
        {
            return random.Next(min, max);
        }
    }
}
