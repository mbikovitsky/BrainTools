using System;
using System.IO;

using ManyConsole;

namespace BrainTools
{
    class RunCommand : ConsoleCommand
    {
        private string path;

        public RunCommand()
        {
            IsCommand("run", "Run the given Brainfuck program.");

            HasRequiredOption("f|file=", "The program to run.", v => path = v);

            SkipsCommandSummaryBeforeRunning();
        }

        public override int Run(string[] remainingArguments)
        {
            using (StreamReader read=new StreamReader(path))
            {
                Brainfuck.Run(read.ReadToEnd());
            }
            return 0;
        }
    }
}
