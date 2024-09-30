using ChessBotBackEnd.BoardAndPieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ChessBotBackEnd.Helpers
{
    internal class Utils
    {
        public static List<Move> LegalMoves(Board chessBoard)
        {
            List<Move> moves = new List<Move>();
            int[] board = chessBoard.getBoard();

            foreach(int i in board)
            {
                switch (i)
                {
                    case 0:
                        break;
                    case (int)PieceType.Pawn:
                        break;
                    default:
                        break;
                }
            }

            return moves;
        }

        public static int[] GetVertHoriMoves(Board board, int Piece)
        {
            // A piece moving sideways can move +1 to a maximum of 7
            // can move -1 to a minimum of 0
            // can move +8 a maximum of 7 times 
            // can move -8 a maximum of 7 times
            List<int> moves = new List<int>();
            int[] possibleWays = { 1, -1, 8, -8 };
            int CurrentRow = Piece / 8;
            int CurrentCol = Piece % 8;

            // to move sideways, the piece must have a free square
            // and must not be being blocked by any piece
            // and if its an opposing piece it can move and take that but not any further

            //calculate the distance from this position to the edges
            

            //for each different way
            foreach (int direction in possibleWays)
            {
                //to a maximum of seven times// 
                // start at 1 end at 7
                for (int i = 1; i < 8; i++)
                {
                    int target = Piece + (direction * i);

                    //dont check any more in that direction if is already outof bounds
                    if (target > 63 || target < 0) break;

                    int targetRow = target / 8;
                    int targetCol = target % 8;

                    if (direction == 1 && targetRow != CurrentRow)  break;
                    if (direction == -1 && targetRow != CurrentRow) break;

                    //if its not empty check its not its own piece and break from this direction
                    if (board.getSquare(target) != 0)
                    {
                        //if its greater than PieceColour.Black 
                        // and is whites move it can move there
                        // but no more further
                        if(board.getSquare(target) > (int)PieceColour.Black && board.getTurn() == (int)PieceColour.White)
                        {
                            //add move 
                            moves.Add(target);
                            break;
                        }
                    }

                    //add move as hasnt had to break
                    //TODO
                    moves.Add(target);
                }


            }

            
            return moves.ToArray();

        }

    }
}
