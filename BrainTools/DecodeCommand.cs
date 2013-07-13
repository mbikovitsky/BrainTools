using System;
using System.Drawing;
using System.IO;

using ManyConsole;

namespace BrainTools
{
    class DecodeCommand : ConsoleCommand
    {
        private string lang;
        private Bitmap img;
        private Stream output = Console.OpenStandardOutput();

        public DecodeCommand()
        {
            IsCommand("decode", "Decode input image using one of the languages.");

            HasRequiredOption("l|lang=",
                "The language to use for decoding:\nbrainloller\nbraincopter",
                v => lang = v);

            HasRequiredOption("i|image=", "The input image.", v => img = new Bitmap(v));

            HasOption("o|output=", "Output file. Defaults to stdout.", v => output = new FileStream(v, FileMode.Create));

            SkipsCommandSummaryBeforeRunning();
        }

        public override int Run(string[] remainingArguments)
        {
            switch (lang)
            {
                case "brainloller":
                    using (StreamWriter writer = new StreamWriter(output))
                    {
                        writer.Write(Brainloller.Decode(img));
                    }
                    return 0;
                case "braincopter":
                    using (StreamWriter writer=new StreamWriter(output))
                    {
                        writer.Write(Braincopter.Decode(img));
                    }
                    return 0;
                default:
                    throw new ConsoleHelpAsException("Unrecognized language.");
            }
        }
    }
}
