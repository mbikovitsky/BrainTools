using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;

namespace BrainTools
{
    public static class Braincopter
    {
        private enum Direction
        {
            east, west, north, south
        };

        public static Bitmap Encode(Bitmap bmp, string code)
        {
            if ((bmp.Width - 2) * bmp.Height + 2 < code.Length)
                return null;

            Bitmap newBmp = (Bitmap)bmp.Clone();
            Direction dir = Direction.east;
            int curX = 0, curY = 0;

            for (int i = 0; i < code.Length; i++)
            {
                Color clr = newBmp.GetPixel(curX, curY);
                int cmd = 10;

                if (curX == newBmp.Width - 1 && dir == Direction.east)
                {
                    newBmp.SetPixel(curX, curY, GetClosest(clr, 8));    //Rotating to the right
                    newBmp.SetPixel(curX, curY + 1, GetClosest(newBmp.GetPixel(curX, curY + 1), 8));    //And a line below
                    curX--;     //Setting new coordinates
                    curY++;
                    clr = newBmp.GetPixel(curX, curY);
                    dir = Direction.west;
                }
                else if (curX == 0 && dir == Direction.west)
                {
                    newBmp.SetPixel(curX, curY, GetClosest(clr, 9));    //Rotating to the left
                    newBmp.SetPixel(curX, curY + 1, GetClosest(newBmp.GetPixel(curX, curY + 1), 9));    //And a line below
                    curX++;     //Setting new coordinates
                    curY++;
                    clr = newBmp.GetPixel(curX, curY);
                    dir = Direction.east;
                }

                switch (code[i])
                {
                    case '>':
                        cmd = 0;
                        break;
                    case '<':
                        cmd = 1;
                        break;
                    case '+':
                        cmd = 2;
                        break;
                    case '-':
                        cmd = 3;
                        break;
                    case '.':
                        cmd = 4;
                        break;
                    case ',':
                        cmd = 5;
                        break;
                    case '[':
                        cmd = 6;
                        break;
                    case ']':
                        cmd = 7;
                        break;
                    default:
                        continue;
                }

                newBmp.SetPixel(curX, curY, GetClosest(clr, cmd));

                if (dir == Direction.east)
                    curX++;
                else if (dir == Direction.west)
                    curX--;
            }

            if (dir == Direction.east)
                for (int i = curX; i < newBmp.Width; i++)
                    newBmp.SetPixel(i, curY, GetClosest(newBmp.GetPixel(i, curY), 10));
            else if (dir == Direction.west)
                for (int i = curX; i > -1; i--)
                    newBmp.SetPixel(i, curY, GetClosest(newBmp.GetPixel(i, curY), 10));

            return newBmp;
        }

        public static string Decode(Bitmap bmp)
        {
            string code = "";
            Direction dir = Direction.east;
            int curX = 0, curY = 0;

            while ((curX >= 0 && curX < bmp.Width) && (curY >= 0 && curY < bmp.Height))
            {
                Color clr = bmp.GetPixel(curX, curY);
                int cmd = (65536 * clr.R + 256 * clr.G + clr.B) % 11;

                if (cmd == 0)
                    code += ">";
                if (cmd == 1)
                    code += "<";
                if (cmd == 2)
                    code += "+";
                if (cmd == 3)
                    code += "-";
                if (cmd == 4)
                    code += ".";
                if (cmd == 5)
                    code += ",";
                if (cmd == 6)
                    code += "[";
                if (cmd == 7)
                    code += "]";

                if (cmd == 8)
                {
                    switch (dir)
                    {
                        case Direction.east:
                            dir = Direction.south;
                            break;
                        case Direction.west:
                            dir = Direction.north;
                            break;
                        case Direction.north:
                            dir = Direction.east;
                            break;
                        case Direction.south:
                            dir = Direction.west;
                            break;
                        default:
                            break;
                    }
                }
                if (cmd == 9)
                {
                    switch (dir)
                    {
                        case Direction.east:
                            dir = Direction.north;
                            break;
                        case Direction.west:
                            dir = Direction.south;
                            break;
                        case Direction.north:
                            dir = Direction.west;
                            break;
                        case Direction.south:
                            dir = Direction.east;
                            break;
                        default:
                            break;
                    }
                }

                switch (dir)
                {
                    case Direction.east:
                        curX++;
                        break;
                    case Direction.west:
                        curX--;
                        break;
                    case Direction.north:
                        curY--;
                        break;
                    case Direction.south:
                        curY++;
                        break;
                    default:
                        break;
                }
            }

            return code;
        }

        private static Color GetClosest(Color origin, int cmd)
        {
            int value = (65536 * origin.R + 256 * origin.G + origin.B) % 11;
            int diff = cmd - value;

            if (diff > 0)
            {
                if (origin.B + diff > 255)
                    return Color.FromArgb(origin.R, origin.G, origin.B - (11 - diff));
                if (origin.B - (11 - diff) < 0)
                    return Color.FromArgb(origin.R, origin.G, origin.B + diff);

                if (11 - diff < diff)
                    return Color.FromArgb(origin.R, origin.G, origin.B - (11 - diff));
                else
                    return Color.FromArgb(origin.R, origin.G, origin.B + diff);
            }

            if (diff < 0)
            {
                diff = Math.Abs(diff);
                if (origin.B - diff < 0)
                    return Color.FromArgb(origin.R, origin.G, origin.B + (11 - diff));
                if (origin.B + (11 - diff) > 255)
                    return Color.FromArgb(origin.R, origin.G, origin.B - diff);

                if (11 - diff < diff)
                    return Color.FromArgb(origin.R, origin.G, origin.B + (11 - diff));
                else
                    return Color.FromArgb(origin.R, origin.G, origin.B - diff);
            }

            return origin;
        }
    }
}
