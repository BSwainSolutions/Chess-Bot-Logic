using ChessBotBackEnd.BoardAndPieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ChessBotBackEnd.Helpers
{
    public class Utils
    {
        //Direction of Horizontal And Vetical Moves //

        public static List<Move> LegalMoves(Board chessBoard)
        {
            List<Move> moves = new List<Move>();

            //for each square on the board
            for(int i =0; i < 64; i++)
            {
                if(chessBoard.getSquare(i) > (int)PieceColour.Black && chessBoard.getTurn() == (int)PieceColour.White)
                {
                    continue;
                }
                else if(chessBoard.getSquare(i) < (int)PieceColour.Black && chessBoard.getTurn() == (int)PieceColour.Black)
                {
                    continue;
                }
                //get its piecetype to switch on
                int piece = chessBoard.getSquare(i) % 8;
                
                switch (piece)
                {
                    case 0:
                        //do nothing 
                        break;
                    case (int)PieceType.Pawn:
                        // get pawn moves
                        AddMoves(GetPawnMoves(chessBoard, i), moves, i); 
                        break;  
                    case (int)PieceType.Rook:
                        //get rook moves
                        AddMoves(GetSlidingMoves(chessBoard, i), moves, i);
                        break;
                    case (int)PieceType.Bishop:
                        //get Bishop Moves
                        AddMoves(GetSlidingMoves(chessBoard, i), moves, i);
                        break;
                    case (int)PieceType.Knight:
                        //get Knight Moves
                        AddMoves(GetKnightHops(chessBoard, i), moves, i);
                        break;
                    case (int)PieceType.Queen:
                        //get Queen Moves
                        AddMoves(GetSlidingMoves(chessBoard, i), moves, i);
                        break;
                    case (int)PieceType.King:
                        //get King Moves
                        AddMoves(GetKingMoves(chessBoard, i), moves, i);
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
                if (board.getSquare(target) == 0)
                {
                    moves.Add(target);
                    continue;
                }
                if (board.getSquare(target) > (int)PieceColour.Black && board.getTurn() == (int)PieceColour.White)
                {
                    moves.Add(target); // Can capture enemy piece
                    continue;
                }
                else if (board.getSquare(target) <= (int)PieceColour.Black && board.getTurn() == (int)PieceColour.Black)
                {
                    moves.Add(target); // Can capture enemy piece
                    continue;
                }

                // do nothing //b
            }

            return moves.ToArray();
        }
    
        public static int[] GetKingMoves(Board board,int Pos)
        {
            List<int> moves = new List<int>();

            // Define all possible king move offsets
            int[] kingOffsets = { 1, -1, 8, -8, 9, 7, -9, -7 };

            int currentRow = Pos / 8;
            int currentCol = Pos % 8;

            foreach (int offset in kingOffsets)
            {
                int target = Pos + offset;

                // Out of bounds check
                if (target < 0 || target > 63)
                    continue;

                int targetRow = target / 8;
                int targetCol = target % 8;

                // Check for horizontal wrapping (left or right edge of the board)
                if (Math.Abs(currentCol - targetCol) > 1)
                    continue;

                // If the square is occupied by a friendly piece, ignore it
                int pieceAtTarget = board.getSquare(target);
                if (pieceAtTarget != 0)
                {
                    // Check if the piece is friendly (same color as the king)
                    if ((board.getTurn() == (int)PieceColour.White && pieceAtTarget < (int)PieceColour.Black) ||
                        (board.getTurn() == (int)PieceColour.Black && pieceAtTarget >= (int)PieceColour.Black))
                    {
                        continue; // Can't move to a square occupied by a friendly piece
                    }
                }

                // Add valid move
                moves.Add(target);
            }

            return moves.ToArray();
        }
        
        public static int[] GetPawnMoves(Board board,int Pos)
        {
            List<int> moves = new List<int>();

            int piece = board.getSquare(Pos);
            bool isWhite = piece < (int)PieceColour.Black;
            int direction = isWhite ? 8 : -8; // White moves "up" (-8), black moves "down" (+8)
            int currentRow = Pos / 8;
            int currentCol = Pos % 8;

            int forwardOne = Pos + direction;
            if (forwardOne >= 0 && forwardOne <= 63 && board.getSquare(forwardOne) == 0) // Ensure it's within bounds and the square is empty
            {
                moves.Add(forwardOne);

                if ((isWhite && currentRow == 1) || (!isWhite && currentRow == 6)) // White pawns start at row 6, black pawns at row 1
                {
                    int forwardTwo = Pos + 2 * direction;
                    if (board.getSquare(forwardTwo) == 0) // Both squares must be empty
                    {
                        moves.Add(forwardTwo);
                    }
                }
            }

            int[] diagonalOffsets = { direction - 1, direction + 1 }; // Left and right diagonal moves
            foreach (int offset in diagonalOffsets)
            {
                int diagonalTarget = Pos + offset;
                int targetRow = diagonalTarget / 8;
                int targetCol = diagonalTarget % 8;

                // Ensure diagonal move doesn't wrap across the board horizontally
                if (diagonalTarget >= 0 && diagonalTarget <= 63 && Math.Abs(currentCol - targetCol) == 1)
                {
                    int pieceAtTarget = board.getSquare(diagonalTarget);
                    if(board.getEnPassantSqaure() == diagonalTarget)
                    {
                        moves.Add(diagonalTarget);
                        continue;
                    }
                    // Check if an enemy piece is there to capture
                    if (pieceAtTarget != 0 &&
                       ((isWhite && pieceAtTarget <= (int)PieceColour.Black) || // White captures black
                        (!isWhite && pieceAtTarget > (int)PieceColour.Black)))   // Black captures white
                    {
                        moves.Add(diagonalTarget);
                        continue;
                    }
                }
            }

            // Return the list of possible moves as an array
            return moves.ToArray();
        }

        private static void AddMoves(int[] moves, List<Move> List,int Pos)
        {
            foreach (int move in moves)
            {
                List.Add(new Move(Pos, move));
            }
        }

        public static void printLegalMoves(List<Move> legalMoves)
        {
            foreach (Move i in legalMoves)
            {
                Console.Out.WriteLine(i.StartSquare.ToString() + " -- " + i.EndSquare.ToString());
                
            }
        }
    }
}
