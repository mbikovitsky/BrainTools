using System;
using System.Drawing;
using System.IO;

namespace BrainTools
{
    static class Program
    {
        static void Main(string[] args)
        {
            //string code = ">+++++++++[<++++++++>-]<.>+++++++[<++++>-]<+.+++++++..+++.>>>++++++++[<++++>-]<.>>>++++++++++[<+++++++++>-]<---.<<<<.+++.------.--------.>>+.";
            //string code2 = "++++++++++[>+>++>+++>++++>+++++>++++++>+++++++>++++++++>+++++++++>++++++++++>+++++++++++>++++++++++++>+++++++++++++<<<<<<<<<<<<<-]>>>>>>>>--------.>>---.>>--------..>---------.<<<<<<<<<--------.>>>----.>>>>-----.>++.++.<-.----.<.>>>.<<<<<<<<<+.";

            if (args.Length == 0)
            {
                PrintHelp();
                return;
            }

            switch (args[0])
            {
                case "encode":
                    if (args.Length == 3 && args[1] == "brainfuck")
                    {
                        Console.Write(Brainfuck.Encode(new FileStream(args[2], FileMode.Open)));
                        return;
                    }

                    if (args.Length != 5)
                    {
                        Console.WriteLine("Wrong number of parameters.");
                        return;
                    }

                    if (args[1] == "brainloller")
                    {
                        StreamReader reader = new StreamReader(args[2]);
                        string code = reader.ReadToEnd();
                        reader.Close();
                        Bitmap newBmp = Brainloller.Encode(code, int.Parse(args[3]), Color.Firebrick);
                        newBmp.Save(args[4]);
                    }
                    else if (args[1] == "braincopter")
                    {
                        StreamReader reader = new StreamReader(args[2]);
                        string code = reader.ReadToEnd();
                        reader.Close();
                        Bitmap original = new Bitmap(args[3]);
                        Bitmap newBmp = Braincopter.Encode(original, code);
                        newBmp.Save(args[4]);
                    }
                    else
                    {
                        Console.WriteLine("Unrecognized language.");
                    }
                    break;
                case "decode":
                    if (args[1] == "brainloller")
                    {
                        Bitmap img = new Bitmap(args[2]);
                        string code = Brainloller.Decode(img);
                        StreamWriter writer = new StreamWriter(args[3]);
                        writer.WriteLine(code);
                        writer.Close();
                    }
                    else if (args[1] == "braincopter")
                    {
                        Bitmap img = new Bitmap(args[2]);
                        string code = Braincopter.Decode(img);
                        StreamWriter writer = new StreamWriter(args[3]);
                        writer.WriteLine(code);
                        writer.Close();
                    }
                    else
                    {
                        Console.WriteLine("Unrecognized language.");
                    }
                    break;
                case "reduce":
                    if (args.Length != 4)
                    {
                        Console.WriteLine("Wrong number of parameters.");
                        return;
                    }

                    Bitmap bmp = new Bitmap(args[1]);
                    Bitmap save = Brainloller.Reduce(bmp, int.Parse(args[2]));
                    save.Save(args[3]);
                    break;
                case "enlarge":
                    if (args.Length != 4)
                    {
                        Console.WriteLine("Wrong number of parameters.");
                        return;
                    }

                    Bitmap src = new Bitmap(args[1]);
                    Bitmap New = Brainloller.Enlarge(src, int.Parse(args[2]));
                    New.Save(args[3]);
                    break;
                case "run":
                    if (args.Length != 2)
                    {
                        Console.WriteLine("Wrong number of parameters.");
                        return;
                    }

                    StreamReader read = new StreamReader(args[1]);
                    Brainfuck.Run(read.ReadToEnd());
                    read.Close();
                    break;
                case "help":
                default:
                    PrintHelp();
                    break;
            }
        }

        static void PrintHelp()
        {
            Console.WriteLine("Usage: bftools <operation> <language> <params>");
            Console.WriteLine();
            Console.WriteLine("Intro");
            Console.WriteLine("======");
            Console.WriteLine("This utility SAVES images in PNG format, so watch your extensions.");
            Console.WriteLine("This utility READS images in all formats supported by the .NET framework.");
            Console.WriteLine();
            Console.WriteLine("Encoding");
            Console.WriteLine("=========");
            Console.WriteLine("bftools encode brainfuck   <input file>");
            Console.WriteLine("bftools encode brainloller <input file> <image width> <output image>");
            Console.WriteLine("bftools encode braincopter <input file> <original image> <output image>");
            Console.WriteLine();
            Console.WriteLine("Decoding");
            Console.WriteLine("=========");
            Console.WriteLine("bftools decode brainloller <input image> <output file>");
            Console.WriteLine("bftools decode braincopter <input image> <output file>");
            Console.WriteLine();
            Console.WriteLine("Running");
            Console.WriteLine("========");
            Console.WriteLine("bftools run <input file>");
            Console.WriteLine();
            Console.WriteLine("Image operations");
            Console.WriteLine("=================");
            Console.WriteLine("bftools enlarge <input image> <factor> <output image>");
            Console.WriteLine("bftools reduce <input image> <factor> <output image>");
            Console.WriteLine();
            Console.WriteLine("Getting help");
            Console.WriteLine("=============");
            Console.WriteLine("bftools help");
            Console.WriteLine();
        }
    }
}
