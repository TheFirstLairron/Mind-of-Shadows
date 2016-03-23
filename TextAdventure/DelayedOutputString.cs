using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TextAdventure
{
    class DelayedOutputString
    {
        public string strValue { get; set; }
        public static readonly int DELAY = 45;

        public void print(bool endOfLine = true)
        {
            foreach (char letter in strValue)
            {
                Console.Write(letter);
                Thread.Sleep(DELAY);
            }
            if(endOfLine)
            {
                Console.WriteLine();
            }
        }

        public DelayedOutputString(string value)
        {
            strValue = value;
        }
    }
}
