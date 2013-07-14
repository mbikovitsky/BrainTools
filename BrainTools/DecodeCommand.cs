using System;
using System.Drawing;
using System.IO;

using ManyConsole;

namespace BrainTools
{
    class DecodeCommand : ConsoleCommand
    {
        private Stream input = Console.OpenStandardInput();
        private Stream output = Console.OpenStandardOutput();

        public DecodeCommand()
        {
            IsCommand("decode", "Decode input image using one of the languages.");

            HasOption("o|output=", "OPTIONAL. Output file. Defaults to stdout.", v => output = File.OpenWrite(v));

            HasAdditionalArguments(2, "<brainloller | braincopter> <image | ->");

            SkipsCommandSummaryBeforeRunning();
        }

        public override int Run(string[] remainingArguments)
        {
            if (remainingArguments[1] != "-")
                input = File.OpenRead(remainingArguments[1]);

            switch (remainingArguments[0])
            {
                case "brainloller":
                    using (StreamWriter writer = new StreamWriter(output))
                    {
                        writer.Write(Brainloller.Decode(new Bitmap(input)));
                    }
                    return 0;
                case "braincopter":
                    using (StreamWriter writer=new StreamWriter(output))
                    {
                        writer.Write(Braincopter.Decode(new Bitmap(input)));
                    }
                    return 0;
                default:
                    throw new ConsoleHelpAsException("Unrecognized language.");
            }
        }
    }
}
