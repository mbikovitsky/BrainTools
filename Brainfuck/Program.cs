using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;
using System.IO;

namespace Brainfuck
{
    static class Program
    {
        static void Main(string[] args)
        {
            string code = ">+++++++++[<++++++++>-]<.>+++++++[<++++>-]<+.+++++++..+++.>>>++++++++[<++++>-]<.>>>++++++++++[<+++++++++>-]<---.<<<<.+++.------.--------.>>+.";
            //Bitmap bmp = new Bitmap("E:\\Desktop\\brainloller.png");
            //string code2 = DecryptBMP(ReduceBMP(bmp, 10, 10));

            Bitmap bmp = new Bitmap("E:\\Desktop\\Untitled.png");
            Bitmap newBmp = Braincopter.Encode(bmp, code);
            string newCode = Braincopter.Decode(newBmp);

            Brainfuck.Run(newCode);

            Console.ReadLine();
        }
    }
}
