using System;
using System.IO;

using ManyConsole;

namespace BrainTools
{
    class RunCommand : ConsoleCommand
    {
        //private string path;
        private Stream program = Console.OpenStandardInput();

        public RunCommand()
        {
            IsCommand("run", "Run the given Brainfuck program.");

            HasOption("f|file=", "The program to run. Defaults to stdin.", v => program = File.OpenRead(v));

            SkipsCommandSummaryBeforeRunning();
        }

        public override int Run(string[] remainingArguments)
        {
            using (StreamReader read=new StreamReader(program))
            {
                Brainfuck.Run(read.ReadToEnd());
            }
            return 0;
        }
    }
}
