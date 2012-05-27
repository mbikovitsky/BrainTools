using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brainfuck
{
    static class Brainfuck
    {
        public static void Run(string code)
        {
            Tape tape = new Tape();

            char[] program = code.ToCharArray();
            int brackets = 0;
            int codePointer = 0;

            while (codePointer < program.Length)
            {
                switch (program[codePointer])
                {
                    case '+':
                        tape.Cell++;
                        codePointer++;
                        break;
                    case '-':
                        tape.Cell--;
                        codePointer++;
                        break;
                    case '>':
                        tape.Right();
                        codePointer++;
                        break;
                    case '<':
                        tape.Left();
                        codePointer++;
                        break;
                    case '[':
                        if (tape.Cell == 0)
                        {
                            brackets++;
                            while (brackets != 0)
                            {
                                codePointer++;
                                if (program[codePointer] == '[')
                                    brackets++;
                                else if (program[codePointer] == ']')
                                    brackets--;
                            }
                            codePointer++;
                            break;
                        }
                        codePointer++;
                        break;
                    case ']':
                        brackets++;
                        while (brackets != 0)
                        {
                            codePointer--;
                            if (program[codePointer] == ']')
                                brackets++;
                            else if (program[codePointer] == '[')
                                brackets--;
                        }
                        break;
                    case '.':
                        Console.Write((char)tape.Cell);
                        codePointer++;
                        break;
                    case ',':
                        tape.Cell = Console.Read();
                        codePointer++;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
