using System;
using System.Collections.Generic;

namespace SnakeGame
{
    class Snake
    {
        protected ConsoleColor color;
        protected List<Vector> body;
        protected Vector dir, lastDir;
        public bool isLive { get; protected set; }

        public Snake(Vector pos, byte length, ConsoleColor color)
        {
            isLive = true;
            body = new List<Vector>(length);
            for (int i = 0; i < length; i++)
                body.Add(pos);
            this.color = color;
            Draw(body[0]);
            GameManager.Map[pos.x, pos.y] = (byte)PointType.Blcok;
            dir = new Vector(1, 0);
            lastDir = Vector.Zero;
        }

        public virtual void Move()
        {
            lastDir = dir;
            Vector lastOfBody = body[body.Count - 1];
            for (int i = body.Count - 1; i > 0; i--)
                body[i] = body[i - 1];
            body[0] += dir;
            if (GameManager.Map[body[0].x, body[0].y] == (byte)PointType.Blcok)
            {
                isLive = false;
                for (int i = 1; i < body.Count; i++)
                {
                    GameManager.Map[body[i].x, body[i].y] = (byte)PointType.None;
                    Clear(body[i]);
                }
                GameManager.Map[lastOfBody.x, lastOfBody.y] = (byte)PointType.None;
                Clear(lastOfBody);
                body = null;
                return;
            }
            if (GameManager.Map[body[0].x, body[0].y] == (byte)PointType.Fruit)
            {
                body.Add(lastOfBody);
                GameManager.GenerateFruit();
            }
            else
            {
                Clear(lastOfBody);
                GameManager.Map[lastOfBody.x, lastOfBody.y] = (byte)PointType.None;
            }
            Draw(body[0]);
            GameManager.Map[body[0].x, body[0].y] = (byte)PointType.Blcok;
        }

        public void ChangeDirection(Vector newDir)
        {
            if(newDir.x != -lastDir.x || newDir.y != -lastDir.y)
                dir = newDir;
        }

        protected void Draw(Vector pos)
        {
            Console.ForegroundColor = color;
            Console.SetCursorPosition(pos.x * 2, pos.y);
            Console.Write("::");
        }
        protected void Clear(Vector pos)
        {
            Console.SetCursorPosition(pos.x * 2, pos.y);
            Console.Write("  ");
        }
    }

    class Mob : Snake
    {
        public Mob(Vector pos, byte length, ConsoleColor color) : base(pos, length, color)
        {

        }
        private void GetDir()
        {
            int[] dirsMotivation = new int[4];
            for (int x = Math.Max(0, body[0].x - 5); x < Math.Min(Settings.MainMapWidth, body[0].x + 6); x++)
            {
                for (int y = Math.Max(0, body[0].y - 5); y < Math.Min(Settings.MainMapHeight, body[0].y + 6); y++)
                {
                    if (GameManager.Map[x, y] == (byte)PointType.Fruit)
                    {
                        int xx = x - body[0].x;
                        int yy = y - body[0].y;
                        if (xx < 0)
                        {
                            if (yy < 0)
                            {
                                dirsMotivation[0] += (6 + xx) * (6 + xx) + (6 + yy) * (6 + yy);
                                dirsMotivation[3] += (6 + xx) * (6 + xx) + (6 + yy) * (6 + yy);
                            }
                            else if (yy == 0)
                            {
                                dirsMotivation[0] += (6 + xx) * (6 + xx) + (6 + xx) * (6 + xx);
                            }
                            else
                            {
                                dirsMotivation[0] += (6 + xx) * (6 + xx) + (6 - yy) * (6 - yy);
                                dirsMotivation[1] += (6 - yy) * (6 - yy) + (6 + xx) * (6 + xx);
                            }
                        }
                        else if (xx == 0)
                        {
                            if (yy < 0)
                            {
                                dirsMotivation[3] += (6 + yy) * (6 + yy) + (6 + yy) * (6 + yy);
                            }
                            else if (yy > 0)
                            {
                                dirsMotivation[1] += (6 - yy) * (6 - yy) + (6 - yy) * (6 - yy);
                            }
                        }
                        else
                        {
                            if (yy < 0)
                            {
                                dirsMotivation[2] += (6 - xx) * (6 - xx)+ (6 + yy) * (6 + yy);
                                dirsMotivation[3] += (6 + yy) * (6 + yy)+ (6 - xx) * (6 - xx);
                            }
                            else if (yy == 0)
                            {
                                dirsMotivation[2] += (6 - xx) * (6 - xx)+ (6 - xx) * (6 - xx);
                            }
                            else
                            {
                                dirsMotivation[2] += (6 - xx) * (6 - xx)+(6 - yy) * (6 - yy);
                                dirsMotivation[1] += (6 - yy) * (6 - yy)+ (6 - xx) * (6 - xx);
                            }
                        }
                    }
                    else if (GameManager.Map[x, y] == (byte)PointType.Blcok)
                    {
                        int xx = x - body[0].x;
                        int yy = y - body[0].y;
                        if (xx < 0)
                        {
                            if (yy < 0)
                            {
                                dirsMotivation[0] -= (6 + xx) * (6 + xx)+ (6 + yy) * (6 + yy);
                                dirsMotivation[3] -= (6 + yy) * (6 + yy)+ (6 + xx) * (6 + xx);
                            }
                            else if (yy == 0)
                            {
                                dirsMotivation[0] -= (6 + xx) * (6 + xx)+ (6 + xx) * (6 + xx);
                            }
                            else
                            {
                                dirsMotivation[0] -= (6 + xx) * (6 + xx)+(6 - yy) * (6 - yy);
                                dirsMotivation[1] -= (6 - yy) * (6 - yy)+ (6 + xx) * (6 + xx);
                            }
                        }
                        else if (xx == 0)
                        {
                            if (yy < 0)
                            {
                                dirsMotivation[3] -= (6 + yy) * (6 + yy)+ (6 + yy) * (6 + yy);
                            }
                            else if (yy > 0)
                            {
                                dirsMotivation[1] -= (6 - yy) * (6 - yy)+ (6 - yy) * (6 - yy);
                            }
                        }
                        else
                        {
                            if (yy < 0)
                            {
                                dirsMotivation[2] -= (6 - xx) * (6 - xx)+ (6 + yy) * (6 + yy);
                                dirsMotivation[3] -= (6 + yy) * (6 + yy)+ (6 - xx) * (6 - xx);
                            }
                            else if (yy == 0)
                            {
                                dirsMotivation[2] -= (6 - xx) * (6 - xx)+ (6 - xx) * (6 - xx);
                            }
                            else
                            {
                                dirsMotivation[2] -= (6 - xx) * (6 - xx)+ (6 - yy) * (6 - yy);
                                dirsMotivation[1] -= (6 - yy) * (6 - yy)+ (6 - xx) * (6 - xx);
                            }
                        }
                    }
                }
            }
            List<int> maxs = new List<int>() { 0 };
            int maxI = 0;
            for (int i = 1; i < 4; i++)
            {
                if (dirsMotivation[maxI] == dirsMotivation[i])
                {
                    maxs.Add(i);
                    continue;
                }
                if (dirsMotivation[maxI] < dirsMotivation[i])
                {
                    maxI = i;
                    maxs = new List<int>() { i };
                }
            }
            int dir = maxs[Random.Range(0, maxs.Count)];
            switch (dir)
            {
                case 0:
                    ChangeDirection(Vector.Left);
                    break;
                case 1:
                    ChangeDirection(Vector.Down);
                    break;
                case 2:
                    ChangeDirection(Vector.Right);
                    break;
                case 3:
                    ChangeDirection(Vector.Up);
                    break;
            }
        }
        public override void Move()
        {
            GetDir();
            base.Move();
        }
    }
}
