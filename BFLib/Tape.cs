using System.Collections.Generic;

namespace BrainTools
{
    /// <summary>
    /// A class for managing a Brainfuck tape.
    /// </summary>
    internal class Tape
    {
        private List<int> tapeList;
        private int index;

        public int Cell
        {
            get
            {
                return this.tapeList[this.index];
            }
            set
            {
                this.tapeList[this.index] = value;
            }
        }

        public void Right()
        {
            this.index++;
            if (this.index == this.tapeList.Count)
                this.tapeList.Add(0);
        }

        public void Left()
        {
            this.index--;
            if (this.index == -1)
            {
                this.tapeList.Insert(0, 0);
                this.index++;
            }
        }

        public Tape()
        {
            this.tapeList = new List<int>();
            tapeList.Add(0);
            this.index = 0;
        }
    }
}
