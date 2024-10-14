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
        private int[] AttackedSquares;
        private bool HasWhiteKingMoved;
        private bool HasBlackKingMoved;
        private bool HasWhiteKingSideRookMoved;
        private bool HasWhiteQueenSideRookMoved;
        private bool HasBlackKingSideRookMoved;
        private bool HasBlackQueenSideRookMoved;

        private int[] boardArr;
        private PieceColour turn;
        private readonly string startingPos = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR";
        //private readonly string TestPos = "4k3/8/8/3Q4/8/8/8/q3K3";
        private int[][] LegalMoves;

        public int getEnPassantSqaure()
        {
            return this.EnPassantSquare;
        }
        public int getTurn()
        {
            return (int)this.turn;
        }

        public void setAttackedSqaures(int[] squareList)
        {
            this.AttackedSquares = squareList;
        }
        public int[] getAttackedSqaures()
        {
            return this.AttackedSquares;
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
            Utils.UpdateAttackedSqaures(this);
        }

        public int getSquare(int target)
        {
            return this.boardArr[target];
        }

        private void IfCastlingPieceMoved(int start)
        {
            switch (start)
            {
                case 0:
                    this.HasWhiteQueenSideRookMoved = true;
                    break;
                case 7:
                    this.HasWhiteKingSideRookMoved = true;
                    break;
                case 4:
                    this.HasWhiteKingMoved = true;
                    break;
                case 56:
                    this.HasBlackQueenSideRookMoved = true;
                    break;
                case 60:
                    this.HasBlackKingMoved = true;  
                    break;
                case 63:
                    this.HasBlackKingSideRookMoved = true; 
                    break;
                default:
                    break;
            }

            return;
        }

        public bool HasKingMoved(int Turn)
        {
            if(Turn == (int)PieceColour.White && HasWhiteKingMoved)
            { 
                return true;                
            }
            if(Turn == (int)PieceColour.Black && HasBlackKingMoved)
            {
                return true;
            }

            return false;
        }

        public bool HasRookMoved(bool isKingSide)
        {
            if(turn == PieceColour.White)
            {
                switch(isKingSide) 
                {
                    case true:
                        return HasWhiteKingSideRookMoved;
                    case false:
                        return HasWhiteQueenSideRookMoved;
                    default:
                        break;
                }
            }
            else
            {
                switch(isKingSide)
                {
                    case true:
                        return HasBlackKingSideRookMoved;
                    case false:
                        return HasBlackQueenSideRookMoved;
                    default:
                        break;
                }
            }

            return false;
        }

        public bool isSquareUnderAttack(int Square)
        {
            if(this.AttackedSquares != null)
            {
                if (this.AttackedSquares.Contains(Square)) return true;
            }
            return false;
        }
        public void MovePiece(int start, int end)
        {
            // update private vars regarding castling rules
            IfCastlingPieceMoved(start);

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


            //otherwise move the piece
            boardArr[end] = boardArr[start];
            boardArr[start] = 0;

            //if its a king and its moved two squares
            if (getSquare(start) % 8 == 6 && Math.Abs(start - end) == 2)
            {
                // king side castling
                if(end > start)
                {
                    boardArr[start + 1] = boardArr[end+1];
                    boardArr[end + 1] = 0;
                }
                else
                {
                    boardArr[start - 1] = boardArr[end - 2];
                    boardArr[end - 2] = 0;
                }
            }

            

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
