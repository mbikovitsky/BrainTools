using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;
using System.Drawing.Imaging;

namespace BrainTools
{
    /// <summary>
    /// Methods for working with Brainloller.
    /// </summary>
    public static class Brainloller
    {
        private enum Direction
        {
            east, west, north, south
        };

        /// <summary>
        /// Encodes the given Brainfuck code into a bitmap.
        /// </summary>
        /// <param name="code">The Brainfuck code to insert.</param>
        /// <param name="width">The resulting bitmap width.</param>
        /// <param name="gapFiller">The color to use for NOPs.</param>
        /// <returns>The encoded bitmap.</returns>
        public static Bitmap Encode(string code, int width, Color gapFiller)
        {
            if (CommandColors.IsCommandColor(gapFiller))
                return null;

            double _height = (double)code.Length / (width - 2);
            int height = 0;
            if (_height == (int)_height)
                height = (int)_height;
            else
                height = (int)_height + 1;

            Bitmap newBmp = new Bitmap(width, height);
            int length = code.Length;

            int curX = 1, curY = 0;
            Direction dir = Direction.east;

            for (int i = 0; i < code.Length; i++)
            {
                Color clr = Color.FromArgb(0, 0, 0);

                if (curX == newBmp.Width - 1 && dir == Direction.east)
                {
                    newBmp.SetPixel(curX, curY, CommandColors.RotateCW);
                    newBmp.SetPixel(curX, curY + 1, CommandColors.RotateCW);
                    curX--;
                    curY++;
                    dir = Direction.west;
                }
                else if (curX == 0 && dir == Direction.west)
                {
                    newBmp.SetPixel(curX, curY, CommandColors.RotateCCW);
                    newBmp.SetPixel(curX, curY + 1, CommandColors.RotateCCW);
                    curX++;
                    curY++;
                    dir = Direction.east;
                }

                switch (code[i])
                {
                    case '>':
                        clr = CommandColors.Right;
                        break;
                    case '<':
                        clr = CommandColors.Left;
                        break;
                    case '+':
                        clr = CommandColors.Inc;
                        break;
                    case '-':
                        clr = CommandColors.Dec;
                        break;
                    case '.':
                        clr = CommandColors.Print;
                        break;
                    case ',':
                        clr = CommandColors.Read;
                        break;
                    case '[':
                        clr = CommandColors.LoopStart;
                        break;
                    case ']':
                        clr = CommandColors.LoopEnd;
                        break;
                    default:
                        continue;
                }

                newBmp.SetPixel(curX, curY, clr);

                if (dir == Direction.east)
                    curX++;
                if (dir == Direction.west)
                    curX--;
            }

            if (dir == Direction.east)
                for (int i = curX; i < newBmp.Width; i++)
                    newBmp.SetPixel(i, curY, gapFiller);
            else if (dir == Direction.west)
                for (int i = curX; i > -1; i--)
                    newBmp.SetPixel(i, curY, gapFiller);

            return newBmp;
        }

        /// <summary>
        /// Decodes the given Brainloller bitmap.
        /// </summary>
        /// <param name="bmp">The bitmap to decode.</param>
        /// <returns>The Brainfuck code contained in the bitmap.</returns>
        public static string Decode(Bitmap bmp)
        {
            string code = "";
            Direction dir = Direction.east;
            int curX = 0, curY = 0;

            while ((curX >= 0 && curX < bmp.Width) && (curY >= 0 && curY < bmp.Height))
            {
                Color clr = bmp.GetPixel(curX, curY);

                if (clr.Equals(CommandColors.Right))
                    code += ">";
                if (clr.Equals(CommandColors.Left))
                    code += "<";
                if (clr.Equals(CommandColors.Inc))
                    code += "+";
                if (clr.Equals(CommandColors.Dec))
                    code += "-";
                if (clr.Equals(CommandColors.Print))
                    code += ".";
                if (clr.Equals(CommandColors.Read))
                    code += ",";
                if (clr.Equals(CommandColors.LoopStart))
                    code += "[";
                if (clr.Equals(CommandColors.LoopEnd))
                    code += "]";

                if (clr.Equals(CommandColors.RotateCW))
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
                if (clr.Equals(CommandColors.RotateCCW))
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

        /// <summary>
        /// Shrinks the given bitmap.
        /// </summary>
        /// <param name="bmp">The bitmap to shrink.</param>
        /// <param name="pxDimension">The factor by which to shrink the bitmap.</param>
        /// <returns>The reduced bitmap.</returns>
        public static Bitmap Reduce(Bitmap bmp, int pxDimension)
        {
            Bitmap newBmp = new Bitmap(bmp.Width / pxDimension, bmp.Height / pxDimension);

            for (int row = 0; row < bmp.Height; row += pxDimension)
                for (int col = 0; col < bmp.Width; col += pxDimension)
                    newBmp.SetPixel(col / 10, row / 10, bmp.GetPixel(col, row));

            return newBmp;
        }

        /// <summary>
        /// Enlarges the given bitmap.
        /// </summary>
        /// <param name="bmp">The bitmap to enlarge.</param>
        /// <param name="pxDimension">The factor by which to enlarge the bitmap.</param>
        /// <returns>The enlarged bitmap.</returns>
        public static Bitmap Enlarge(Bitmap bmp, int pxDimension)
        {
            Bitmap newBmp = new Bitmap(bmp.Width * pxDimension, bmp.Height * pxDimension);
            Graphics g = Graphics.FromImage(newBmp);

            for (int row = 0; row < bmp.Height; row++)
            {
                for (int col = 0; col < bmp.Width; col++)
                {
                    Rectangle rect = new Rectangle(col * pxDimension, row * pxDimension, pxDimension, pxDimension);
                    Pen pen = new Pen(bmp.GetPixel(col, row));
                    SolidBrush brush = new SolidBrush(bmp.GetPixel(col, row));
                    g.DrawRectangle(pen, rect);
                    g.FillRectangle(brush, rect);
                }
            }

            g.Dispose();

            return newBmp;
        }
    }
}
