using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessBotBackEnd.BoardAndPieces
{

    internal class Piece
    {
        private int xpos; private int ypos;

        public Piece()
        {

        }

        public Piece(int xpos, int ypos)
        {
            this.xpos = xpos;
            this.ypos = ypos;
        }
    
    }
}
