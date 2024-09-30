using ChessBotBackEnd.BoardAndPieces;
using ChessBotBackEnd.Helpers;
using System.Net;

Board chessBoard = new Board("4k3/8/8/3Q4/8/8/8/q3K3");
chessBoard.PrintBoard();
//chessBoard.MovePiece(0, 28);
Console.WriteLine("\n");
chessBoard.PrintBoard();
Utils.GetVertHoriMoves(chessBoard, 0);
Console.ReadLine();

