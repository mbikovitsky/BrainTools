using System;
using System.IO;

using ManyConsole;

namespace BrainTools
{
    class RunCommand : ConsoleCommand
    {
        private Stream program = Console.OpenStandardInput();

        public RunCommand()
        {
            IsCommand("run", "Run the given Brainfuck program.");

            HasAdditionalArguments(1, " <program | ->");

            SkipsCommandSummaryBeforeRunning();
        }

        public override int Run(string[] remainingArguments)
        {
            if (remainingArguments[0] != "-")
                program = File.OpenRead(remainingArguments[0]);

            using (StreamReader read = new StreamReader(program))
            {
                Brainfuck.Run(read.ReadToEnd());
            }

            return 0;
        }
    }
}
