﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessBotBackEnd.Helpers
{
    public struct Move
    {
        private int StartSquare;
        private int EndSquare;

        public Move(int start,int end)
        {
            this.StartSquare = start;
            this.EndSquare = end;  
        }
    }
}
