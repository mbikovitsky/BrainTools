using System;
using System.Drawing;
using System.IO;

using ManyConsole;

namespace BrainTools
{
    class EnlargeCommand : ConsoleCommand
    {
        private Bitmap img;
        private int factor;
        private Stream output = Console.OpenStandardOutput();

        public EnlargeCommand()
        {
            IsCommand("enlarge", "Enlarge an image by a given factor.");

            HasRequiredOption("i|image=", "The input image", v => img = new Bitmap(v));
            HasRequiredOption("f|factor=", "Enlargement factor.", v => factor = int.Parse(v));
            HasOption("o|output=", "Output file. Defaults to stdout.", v => output = new FileStream(v, FileMode.Create));

            SkipsCommandSummaryBeforeRunning();
        }

        public override int Run(string[] remainingArguments)
        {
            Brainloller.Enlarge(img, factor).Save(output, System.Drawing.Imaging.ImageFormat.Png);
            return 0;
        }
    }
}
