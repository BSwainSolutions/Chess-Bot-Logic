using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessBotBackEnd.Helpers
{
    public struct Move
    {
        public int StartSquare;
        public int EndSquare;

        public Move(int start,int end)
        {
            this.StartSquare = start;
            this.EndSquare = end;  
        }

    }
}
