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

        //Direction of Horizontal And Vetical Moves //
       

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

        public static int[] GetVertHoriMoves(Board board, int Pos)
        {
            // A piece moving sideways can move +1 to a maximum of 7
            // can move -1 to a minimum of 0
            // can move +8 a maximum of 7 times 
            // can move -8 a maximum of 7 times
            List<int> moves = new List<int>();
            int CurrentRow = Pos / 8;
            int CurrentCol = Pos % 8;
            int[] possibleWays;
            switch ((board.getSquare(Pos) % 8))
            {
                case (int)PieceType.Bishop:
                    // make possible moves int[] contains diagonal moves
                    possibleWays = new int[] { 7,-7,9,-9 };
                    break;
                case (int)PieceType.Rook:
                    possibleWays = new int[] {-1,-1,8,-8};
                    break;
                case (int)PieceType.Queen:
                    possibleWays = new int[] {-1, -1, 8, -8, 7, -7, 9, -9};
                    break;
                //if not any of these types then other movement logic is required //
                default:
                    possibleWays = new int[] { 0 };
                    break;
            }


            foreach (int direction in possibleWays)
            {
                //to a maximum of seven times// 
                // start at 1 end at 7
                for (int i = 1; i < 8; i++)
                {
                    int target = Pos + (direction * i);

                    //dont check any more in that direction if is already outof bounds
                    if (target > 63 || target < 0) break;

                    int targetRow = target / 8;
                    int targetCol = target % 8;

                    //horizontal wrap check
                    if (direction == 1 && targetRow != CurrentRow)  break;
                    if (direction == -1 && targetRow != CurrentRow) break;

                    //diagonal wrap check 
                    if ((direction == 7 || direction == -9) && targetCol <= CurrentCol) break; 
                    if ((direction == 9 || direction == -7) && targetCol >= CurrentCol) break;
                    
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
