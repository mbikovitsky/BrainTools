using System;
using System.Drawing;
using System.IO;

using ManyConsole;

namespace BrainTools
{
    static class Program
    {
        static int Main(string[] args)
        {
            //string code = ">+++++++++[<++++++++>-]<.>+++++++[<++++>-]<+.+++++++..+++.>>>++++++++[<++++>-]<.>>>++++++++++[<+++++++++>-]<---.<<<<.+++.------.--------.>>+.";
            //string code2 = "++++++++++[>+>++>+++>++++>+++++>++++++>+++++++>++++++++>+++++++++>++++++++++>+++++++++++>++++++++++++>+++++++++++++<<<<<<<<<<<<<-]>>>>>>>>--------.>>---.>>--------..>---------.<<<<<<<<<--------.>>>----.>>>>-----.>++.++.<-.----.<.>>>.<<<<<<<<<+.";

            var commands = ConsoleCommandDispatcher.FindCommandsInSameAssemblyAs(typeof(Program));
            return ConsoleCommandDispatcher.DispatchCommand(commands, args, Console.Out);
        }
    }
}
