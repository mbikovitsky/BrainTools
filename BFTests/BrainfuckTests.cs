using BrainTools;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Text;

namespace BFTests
{
    [TestClass]
    public class BrainfuckTests
    {
        private string RunMemoryBFTest(string bfCode, string inputCode = null)
        {
            if (inputCode == null)
                inputCode = string.Empty;

            using (MemoryStream msOut = new MemoryStream())
            using (MemoryStream msIn = new MemoryStream(Encoding.ASCII.GetBytes(inputCode)))
            {
                Brainfuck.Run(bfCode, msIn, msOut);

                return Encoding.ASCII.GetString(msOut.ToArray());
            }
        }

        [TestMethod, Description("Validates the results of running a valid piece of BF code.")]
        public void BasicBrainfuckTest()
        {
            Assert.AreEqual("Hello World!", RunMemoryBFTest(">+++++++++[<++++++++>-]<.>+++++++[<++++>-]<+.+++++++..+++.>>>++++++++[<++++>-]<.>>>++++++++++[<+++++++++>-]<---.<<<<.+++.------.--------.>>+."));
        }

        [TestMethod, Description("Validates the results of running a BF code with cell-wrapping.")]
        public void CellWrappingTest()
        {
            Assert.AreEqual("H", RunMemoryBFTest("-[------->+<]>-."));
        }

        [TestMethod, Description("Validates that the loop-start operator jumps beyond the loop-end when cell value is zero.")]
        public void LoopSkippingTest()
        {
            // The second loop should be skipped entirely since the cell value is zero.
            Assert.AreEqual("F", RunMemoryBFTest("++++++++++[->+++++++<][>++++<]>."));
        }
    }
}