using System;
using System.Drawing;
using System.IO;

using ManyConsole;

namespace BrainTools
{
    class EncodeCommand : ConsoleCommand
    {
        private string lang;
        private int width;
        private Stream input = Console.OpenStandardInput();
        private Stream output = Console.OpenStandardOutput();
        private string orig_image;

        public EncodeCommand()
        {
            IsCommand("encode", "Encode input file using one of the languages.");
            
            HasRequiredOption("l|lang=",
                "The language to use for encoding:\nbrainfuck\nbrainloller\nbraincopter",
                v => lang = v);

            HasOption("f|file=",
                "The file to encode. Defaults to stdin.",
                v => input = File.OpenRead(v));

            HasOption("w|width=", "Resulting image width when encoding with Brainloller.", v => width = int.Parse(v));
            HasOption("o|output=", "Output file. Defaults to stdout.", v => output = File.OpenWrite(v));
            HasOption("i|original=", "Original image for encoding with Braincopter.", v => orig_image = v);

            SkipsCommandSummaryBeforeRunning();
        }

        public override int Run(string[] remainingArguments)
        {
            switch (lang)
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
