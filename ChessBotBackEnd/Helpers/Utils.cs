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

        public static int[] GetSlidingMoves(Board board, int Pos)
        {
            List<int> moves = new List<int>();
            int CurrentRow = Pos / 8;
            int CurrentCol = Pos % 8;
            int[] possibleWays;

            switch (board.getSquare(Pos) % 8) // Adjusted for modulus logic to identify piece
            {
                case (int)PieceType.Bishop:
                    possibleWays = new int[] { 7, -7, 9, -9 }; // Diagonal directions
                    break;
                case (int)PieceType.Rook:
                    possibleWays = new int[] { 1, -1, 8, -8 }; // Horizontal and vertical
                    break;
                case (int)PieceType.Queen:
                    possibleWays = new int[] { 1, -1, 8, -8, 7, -7, 9, -9 }; // Both diagonal and straight
                    break;
                default:
                    return new int[] { }; // Return empty array for unsupported piece types
            }

            foreach (int direction in possibleWays)
            {
                // Move in this direction a maximum of 7 times (as far as the board allows)
                for (int i = 1; i < 8; i++)
                {
                    int target = Pos + direction * i;

                    // Out of bounds check
                    if (target < 0 || target > 63) break;

                    int targetRow = target / 8;
                    int targetCol = target % 8;

                    // Horizontal wrap check for rook/queen horizontal moves
                    if (direction == 1 && targetRow != CurrentRow) break;
                    if (direction == -1 && targetRow != CurrentRow) break;

                    // Diagonal wrap check for bishop/queen diagonal moves
                    if (direction == 7 || direction == -9) // Moves towards bottom-left or top-right
                    {
                        if (targetCol >= CurrentCol) break; // Prevent leftward wrapping
                    }
                    if (direction == 9 || direction == -7) // Moves towards top-left or bottom-right
                    {
                        if (targetCol <= CurrentCol) break; // Prevent rightward wrapping
                    }

                    // If the target square is occupied, check if it's an enemy piece
                    if (board.getSquare(target) != 0)
                    {
                        if (board.getSquare(target) > (int)PieceColour.Black && board.getTurn() == (int)PieceColour.White)
                        {
                            moves.Add(target); // Can capture enemy piece
                        }
                        else if (board.getSquare(target) <= (int)PieceColour.Black && board.getTurn() == (int)PieceColour.Black)
                        {
                            moves.Add(target); // Can capture enemy piece
                        }
                        break; // Stop further movement in this direction
                    }

                    // Add valid move if the target square is empty
                    moves.Add(target);
                }
            }

            return moves.ToArray();
        }

        public static int[] GetKnightHops(Board board, int Pos)
        {
            List<int> moves = new List<int>();

            // Define all possible knight move offsets
            int[] knightOffsets = { 15, 17, 10, 6, -15, -17, -10, -6 };

            int currentRow = Pos / 8;
            int currentCol = Pos % 8;

            foreach (int offset in knightOffsets)
            {
                int target = Pos + offset;

                // Out of bounds check
                if (target < 0 || target > 63)
                    continue;

                int targetRow = target / 8;
                int targetCol = target % 8;

                // Check for horizontal or vertical wrapping (i.e., moving from one edge to another)
                if (Math.Abs(currentRow - targetRow) > 2 || Math.Abs(currentCol - targetCol) > 2)
                    continue;

                // If the square is occupied by a friendly piece, ignore it
                int pieceAtTarget = board.getSquare(target);
                if (pieceAtTarget != 0)
                {
                    if ((board.getTurn() == (int)PieceColour.White && pieceAtTarget > (int)PieceColour.Black) ||
                        (board.getTurn() == (int)PieceColour.Black && pieceAtTarget <= (int)PieceColour.Black))
                    {
                        continue; // Can't move to a square occupied by a friendly piece
                    }
                }

                // Add valid move
                moves.Add(target);
            }

            return moves.ToArray();
        }
    }
}
