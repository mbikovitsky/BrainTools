using System;
using System.IO;

namespace BrainTools
{
    /// <summary>
    /// Methods for working with Brainfuck.
    /// </summary>
    public static class Brainfuck
    {
        /// <summary>
        /// Runs the provided Brainfuck code.
        /// </summary>
        /// <param name="code">The code to run.</param>
        public static void Run(string code)
        {
            Tape tape = new Tape();

            char[] program = code.ToCharArray();
            int brackets = 0;
            int codePointer = 0;

            Stream stdin = Console.OpenStandardInput();
            Stream stdout = Console.OpenStandardOutput();

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
                        stdout.WriteByte((byte)tape.Cell);
                        codePointer++;
                        break;
                    case ',':
                        tape.Cell = stdin.ReadByte();
                        codePointer++;
                        break;
                    default:
                        codePointer++;
                        break;
                }
            }
        }

        /// <summary>
        /// Encodes the specified file into Brainfuck code.
        /// </summary>
        /// <param name="input">FileStream for the file to read.</param>
        /// <returns>String with the generated code.</returns>
        public static string Encode(FileStream input)
        {
            /* The following code was adapted from https://github.com/splitbrain/ook/blob/master/util.php
             * Licensed under the GNU General Public License Version 2.
             */

            int value = 0;          // Value of current pointer
            string result = "";

            for (int i = 0; i < input.Length; i++)
            {
                int temp = input.ReadByte();
                int diff = temp - value;        // Difference between current value and target value

                value = temp;

                // Repeat current character
                if (diff == 0)
                {
                    result += ">.<";
                    continue;
                }

                // Is it worth making a loop?

                // No. A bunch of + or - consume less space than the loop.
                if (Math.Abs(diff) < 10)
                {
                    if (diff > 0)
                        result += ">" + new string('+', diff);
                    else if (diff < 0)
                        result += ">" + new string('-', Math.Abs(diff));
                }
                // Yes, create a loop. This will make the resulting code more compact.
                else
                {
                    int loop = (int)Math.Sqrt(Math.Abs(diff));

                    // Set loop counter
                    result += new string('+', loop);

                    // Execute loop, then add remainder
                    if (diff > 0)
                    {
                        result += "[->" + new string('+', loop) + "<]";
                        result += ">" + new string('+', diff - (int)Math.Pow(loop, 2));
                    }
                    else if (diff < 0)
                    {
                        result += "[->" + new string('-', loop) + "<]";
                        result += ">" + new string('-', Math.Abs(diff) - (int)Math.Pow(loop, 2));
                    }
                }

                result += ".<";
            }

            // Cleanup
            input.Close();
            return result.Replace("<>", "");
        }
    }
}
