using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessBotBackEnd.Helpers;

namespace ChessBotBackEnd.BoardAndPieces
{

    internal class Board
    {
        private int[] boardArr;
        private PieceColour turn;
        private readonly string startingPos = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR";
        private readonly string TestPos = "4k3/8/8/3Q4/8/8/8/q3K3";
        private int[][] LegalMoves;

        public int[] getBoard()
        {
            return this.boardArr;
        }

        public int getTurn()
        {
            return (int)this.turn;
        }

        //general constructor for a new game 
        public Board()
        {
            boardArr = new int[64];
            // populate board
            PopulateBoardUsingFEN(startingPos);
        }

        public Board(string fen)
        {
            boardArr = new int[64];
            PopulateBoardUsingFEN(fen);
        }

        public int getSquare(int target)
        {
            return this.boardArr[target];
        }

        public void MovePiece(int start, int end)
        {
            if (IsLegalMove(start, end))
            {
                boardArr[end] = boardArr[start];
                boardArr[start] = 0;

                if (turn == PieceColour.White)
                {
                    turn = PieceColour.Black;
                }
                else
                {
                    turn = PieceColour.White;
                }
                return;
            }
            return;
        }

        private bool IsLegalMove(int start, int end)
        {
            // TODO 
            return true;
        }

        private void PopulateBoardUsingFEN(string fen)
        {
            //* populate the board //
            // FEN notation is as follows // 
            // r - rook 
            // n - knight
            // b - bishop 
            // q - queen
            // k - king
            // UPPERCASE BEING WHITE 
            // LOWERCASE BEING BLACK

            int rank = 0;
            int file = 0;
            int placement = 0;
            foreach (char c in fen)
            {
                if (c == '/')
                {
                    //indicates the end of a row //
                    rank++;
                    file = 0;
                }
                else if (c == ' ')
                {
                    // do nothing
                }
                else if (char.IsDigit(c))
                {
                    file += (int)char.GetNumericValue(c); // skip empty squares
                }
                else
                {
                    int currentPieceValue = 0;
                    switch (char.ToLower(c))
                    {
                        case 'r':
                            currentPieceValue += (int)PieceType.Rook;
                            break;
                        case 'n':
                            currentPieceValue += (int)PieceType.Knight;
                            break;
                        case 'b':
                            currentPieceValue += (int)PieceType.Bishop;
                            break;
                        case 'q':
                            currentPieceValue += (int)PieceType.Queen;
                            break;
                        case 'k':
                            currentPieceValue += (int)PieceType.King;
                            break;
                        case 'p':
                            currentPieceValue += (int)PieceType.Pawn;
                            break;
                        default:
                            throw new ArgumentException($"INVALID CHESS PIECE IN FEN NOTATION");
                            break;

                    }

                    // if its lower case its black so add eight // 
                    if (char.IsLower(c))
                    {
                        currentPieceValue += 8;
                    }

                    boardArr[rank * 8 + file] = currentPieceValue;
                    file++;
                }
            }
        }

        public void PrintBoard()
        {
            Console.Write("|");
            for (int i = 0; i <64 ; i++)
            {
                if (i % 8 == 0 && i != 0) Console.Write("\n|");
                //Console.Write("| ");
                switch (boardArr[i])
                {
                    case 0:
                        Console.Write(" ");
                        break;
                    case (int)PieceType.Pawn + (int)PieceColour.White:
                        Console.Write("P");
                        break;
                    case (int)PieceType.Rook + (int)PieceColour.White:
                        Console.Write("R");
                        break;
                    case (int)PieceType.Knight + (int)PieceColour.White:
                        Console.Write("N");
                        break;
                    case (int)PieceType.Bishop + (int)PieceColour.White:
                        Console.Write("B");
                        break;
                    case (int)PieceType.King + (int)PieceColour.White:
                        Console.Write("K");
                        break;
                    case (int)PieceType.Queen + (int)PieceColour.White:
                        Console.Write("Q");
                        break;
                    case (int)PieceType.Pawn + (int)PieceColour.Black:
                        Console.Write("p");
                        break;
                    case (int)PieceType.Rook + (int)PieceColour.Black:
                        Console.Write("r");
                        break;
                    case (int)PieceType.Knight + (int)PieceColour.Black:
                        Console.Write("n");
                        break;
                    case (int)PieceType.Bishop + (int)PieceColour.Black:
                        Console.Write("b");
                        break;
                    case (int)PieceType.King + (int)PieceColour.Black:
                        Console.Write("k");
                        break;
                    case (int)PieceType.Queen + (int)PieceColour.Black:
                        Console.Write("q");
                        break;
                    default:
                        break;
                }
                Console.Write(" |");

            }
        }

        public void PrintBoard2()
        {
            for (int i = 63; i >= 0; i--)
            {
                if (i % 8 == 7)  // Start a new row
                {
                    Console.Write("|");
                }

                // Switch to determine what to print based on the boardArr
                switch (boardArr[i])
                {
                    case 0:
                        Console.Write(" ");
                        break;
                    case (int)PieceType.Pawn + (int)PieceColour.White:
                        Console.Write("P");
                        break;
                    case (int)PieceType.Rook + (int)PieceColour.White:
                        Console.Write("R");
                        break;
                    case (int)PieceType.Knight + (int)PieceColour.White:
                        Console.Write("N");
                        break;
                    case (int)PieceType.Bishop + (int)PieceColour.White:
                        Console.Write("B");
                        break;
                    case (int)PieceType.King + (int)PieceColour.White:
                        Console.Write("K");
                        break;
                    case (int)PieceType.Queen + (int)PieceColour.White:
                        Console.Write("Q");
                        break;
                    case (int)PieceType.Pawn + (int)PieceColour.Black:
                        Console.Write("p");
                        break;
                    case (int)PieceType.Rook + (int)PieceColour.Black:
                        Console.Write("r");
                        break;
                    case (int)PieceType.Knight + (int)PieceColour.Black:
                        Console.Write("n");
                        break;
                    case (int)PieceType.Bishop + (int)PieceColour.Black:
                        Console.Write("b");
                        break;
                    case (int)PieceType.King + (int)PieceColour.Black:
                        Console.Write("k");
                        break;
                    case (int)PieceType.Queen + (int)PieceColour.Black:
                        Console.Write("q");
                        break;
                    default:
                        break;
                }
                Console.Write(i.ToString().PadLeft(5));

                if (i % 8 == 0)  // End the row and start a new one
                {
                    Console.WriteLine();  // New line at the end of the row
                }
            }
        }
    }
}
