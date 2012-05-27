using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;

namespace Brainfuck
{
    static class Brainloller
    {
        private static enum Direction
        {
            east, west, north, south
        };

        public static string Decode(Bitmap bmp)
        {
            string code = "";
            Direction dir = Direction.east;
            int curX = 0, curY = 0;

            while ((curX >= 0 && curX < bmp.Width) && (curY >= 0 && curY < bmp.Height))
            {
                Color clr = bmp.GetPixel(curX, curY);

                if (clr.R == 255 && clr.G == 0 && clr.B == 0)
                    code += ">";
                if (clr.R == 128 && clr.G == 0 && clr.B == 0)
                    code += "<";
                if (clr.R == 0 && clr.G == 255 && clr.B == 0)
                    code += "+";
                if (clr.R == 0 && clr.G == 128 && clr.B == 0)
                    code += "-";
                if (clr.R == 0 && clr.G == 0 && clr.B == 255)
                    code += ".";
                if (clr.R == 0 && clr.G == 0 && clr.B == 128)
                    code += ",";
                if (clr.R == 255 && clr.G == 255 && clr.B == 0)
                    code += "[";
                if (clr.R == 128 && clr.G == 128 && clr.B == 0)
                    code += "]";

                if (clr.R == 0 && clr.G == 255 && clr.B == 255)
                {
                    switch (dir)
                    {
                        case Direction.east:
                            dir = Direction.south;
                            break;
                        case Direction.south:
                            dir = Direction.west;
                            break;
                        case Direction.west:
                            dir = Direction.north;
                            break;
                        case Direction.north:
                            dir = Direction.east;
                            break;
                        default:
                            break;
                    }
                }
                if (clr.R == 0 && clr.G == 128 && clr.B == 128)
                {
                    switch (dir)
                    {
                        case Direction.east:
                            dir = Direction.north;
                            break;
                        case Direction.south:
                            dir = Direction.east;
                            break;
                        case Direction.west:
                            dir = Direction.south;
                            break;
                        case Direction.north:
                            dir = Direction.west;
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

        public static Bitmap Reduce(Bitmap bmp, int pxWidth, int pxHeight)
        {
            Bitmap newBmp = new Bitmap(bmp.Width / pxWidth, bmp.Height / pxHeight);

            for (int row = 0; row < bmp.Height; row += pxHeight)
                for (int col = 0; col < bmp.Width; col += pxWidth)
                    newBmp.SetPixel(col / 10, row / 10, bmp.GetPixel(col, row));

            return newBmp;
        }
    }
}
