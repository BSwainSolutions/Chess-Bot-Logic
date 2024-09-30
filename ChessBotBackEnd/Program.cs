using ChessBotBackEnd.BoardAndPieces;
using ChessBotBackEnd.Helpers;
using System.Net;

Board chessBoard = new Board("///3Q4////");
chessBoard.PrintBoard2();
//chessBoard.MovePiece(0, 28);
Console.WriteLine("\n");
chessBoard.PrintBoard2();
Utils.GetSlidingMoves(chessBoard, 27);
Console.ReadLine();

