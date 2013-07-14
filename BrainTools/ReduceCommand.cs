using System;
using System.Drawing;
using System.IO;

using ManyConsole;

namespace BrainTools
{
    class ReduceCommand : ConsoleCommand
    {
        private int factor;
        private Stream input = Console.OpenStandardInput();
        private Stream output = Console.OpenStandardOutput();

        public ReduceCommand()
        {
            IsCommand("reduce", "Shrink an image by a given factor.");

            HasOption("i|image=", "The input image. Defaults to stdin.", v => input = File.OpenRead(v));
            HasRequiredOption("f|factor=", "Shrink factor.", v => factor = int.Parse(v));
            HasOption("o|output=", "The output image. Defaults to stdout.", v => output = File.OpenWrite(v));

            SkipsCommandSummaryBeforeRunning();
        }

        public override int Run(string[] remainingArguments)
        {
            Brainloller.Reduce(new Bitmap(input), factor).Save(output, System.Drawing.Imaging.ImageFormat.Png);
            return 0;
        }
    }
}
