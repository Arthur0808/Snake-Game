using System;

namespace SnakeGame
{
    struct Vector
    {
        public static Vector Zero { get; } = new Vector();
        public static Vector Left { get; } = new Vector(-1, 0);
        public static Vector Down { get; } = new Vector(0, 1);
        public static Vector Right { get; } = new Vector(1, 0);
        public static Vector Up { get; } = new Vector(0, -1);
        public int x { get; set; }
        public int y { get; set; }

        public Vector(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public static Vector operator +(Vector a, Vector b)
        {
            return new Vector(a.x + b.x, a.y + b.y);
        }
        public static Vector operator -(Vector a, Vector b)
        {
            return new Vector(a.x - b.x, a.y - b.y);
        }
        public static bool operator ==(Vector a, Vector b)
        {
            return a.x == b.y && a.y == b.y;
        }
        public static bool operator !=(Vector a, Vector b)
        {
            return a.x != b.y || a.y != b.y;
        }
    }
}
