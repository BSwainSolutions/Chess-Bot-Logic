using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessBotBackEnd.Helpers;

namespace ChessBotBackEnd.BoardAndPieces
{

    public class Board
    {
        private int EnPassantSquare = -1;
        private int[] boardArr;
        private PieceColour turn;
        private readonly string startingPos = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR";
        private readonly string TestPos = "4k3/8/8/3Q4/8/8/8/q3K3";
        private int[][] LegalMoves;

        public int[] getBoard()
        {
            return this.boardArr;
        }

        public int getEnPassantSqaure()
        {
            return this.EnPassantSquare;
        }
        public void setEnPassantSqaure(int target)
        {
            this.EnPassantSquare = target;
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
            int WhiteOrBlackMove = (this.turn == PieceColour.White) ? 8 : -8;

            if (end == this.EnPassantSquare)
            {
                boardArr[end - WhiteOrBlackMove] = 0;
                //remove piece from square.
            }


            //add logic for moving a pawn two off the starting rank and set the enpassant square
            if (getSquare(start) % 8 == (int)PieceType.Pawn && Math.Abs(end - start) == 16)
            {
                
                this.EnPassantSquare = end - WhiteOrBlackMove;
            }
            else
            {
                this.EnPassantSquare = -1;
            }
            

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

            int rank = 7;
            int file = 0;
            foreach (char c in fen)
            {
                if (c == '/')
                {
                    //indicates the end of a row //
                    rank--;
                    file = 0;
                }
                else if (c == ' ')
                {
                    // do nothing
                }
                else if (char.IsDigit(c))
                {
                    file -= (int)char.GetNumericValue(c); // skip empty squares
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

        public void NumberPrint()
        {
            Console.Write("|");
            for (int i = 0; i < 64; i++)
            {
                if (i % 8 == 0 && i != 0)
                    Console.Write("\n|");

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
                Console.Write(i.ToString().PadRight(5));

                //if (i % 8 == 0)  // End the row and start a new one
                //{
                //    Console.WriteLine();  // New line at the end of the row
                //}
            }
        }
    }
}
