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
            string code2 = "++++++++++[>+>++>+++>++++>+++++>++++++>+++++++>++++++++>+++++++++>++++++++++>+++++++++++>++++++++++++>+++++++++++++<<<<<<<<<<<<<-]>>>>>>>>--------.>>---.>>--------..>---------.<<<<<<<<<--------.>>>----.>>>>-----.>++.++.<-.----.<.>>>.<<<<<<<<<+.";

            //Bitmap bmp = new Bitmap("E:\\Desktop\\Untitled.png");
            //Bitmap newBmp = Braincopter.Encode(bmp, code);
            //string newCode = Braincopter.Decode(newBmp);

            Bitmap bmp = Brainloller.Encode(code2, 22, Color.Firebrick);
            bmp.Save("E:\\Desktop\\brain.png");

            //Bitmap bmp = new Bitmap("E:\\Desktop\\brain.png");
            //string code3 = Brainloller.Decode(bmp);
            //Brainfuck.Run(code3);

            //Brainfuck.Run(newCode);

            Console.ReadLine();
        }
    }
}
