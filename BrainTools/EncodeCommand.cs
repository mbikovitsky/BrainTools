using System;
using System.Drawing;
using System.IO;

using ManyConsole;

namespace BrainTools
{
    class EncodeCommand : ConsoleCommand
    {
        private int width;
        private Stream input = Console.OpenStandardInput();
        private Stream output = Console.OpenStandardOutput();
        private string orig_image;

        public EncodeCommand()
        {
            IsCommand("encode", "Encode input file using one of the languages.");
            
            HasOption("w|width=", "Resulting image width when encoding with Brainloller.", v => width = int.Parse(v));
            HasOption("i|original=", "Original image for encoding with Braincopter.", v => orig_image = v);
            HasOption("o|output=", "OPTIONAL. Output file. Defaults to stdout.", v => output = File.OpenWrite(v));

            HasAdditionalArguments(2, "<brainfuck | brainloller | braincopter> <file | ->");

            SkipsCommandSummaryBeforeRunning();
        }

        public override int Run(string[] remainingArguments)
        {
            if (remainingArguments[1] != "-")
                input = File.OpenRead(remainingArguments[1]);

            switch (remainingArguments[0])
            {
                case "brainfuck":
                    using (StreamWriter sw = new StreamWriter(output))
                        sw.Write(Brainfuck.Encode(input));
                    return 0;
                case "brainloller":
                    if (width == 0)
                        throw new ConsoleHelpAsException("Width is a required option when encoding with Brainloller.");
                    using (StreamReader reader = new StreamReader(input))
                    {
                        string code = reader.ReadToEnd();
                        Bitmap newBmp = Brainloller.Encode(code, width, Color.Firebrick);
                        newBmp.Save(output, System.Drawing.Imaging.ImageFormat.Png);
                    }
                    return 0;
                case "braincopter":
                    if (orig_image == null)
                        throw new ConsoleHelpAsException("Original image is required when encoding with Braincopter.");
                    using (StreamReader reader = new StreamReader(input))
                    {
                        string code = reader.ReadToEnd();
                        Bitmap original = new Bitmap(orig_image);
                        Bitmap newBmp = Braincopter.Encode(original, code);
                        newBmp.Save(output,System.Drawing.Imaging.ImageFormat.Png);
                    }
                    return 0;
                default:
                    throw new ConsoleHelpAsException("Unrecognized language.");
            }
        }
    }
}
