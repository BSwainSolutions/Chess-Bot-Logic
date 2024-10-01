using ChessBotBackEnd.BoardAndPieces;
using ChessBotBackEnd.Helpers;
using System.Net;

Board chessBoard = new Board("///2pQ4////");
chessBoard.PrintBoard();
Console.WriteLine("\n");
chessBoard.NumberPrint();
Utils.GetSlidingMoves(chessBoard, 27);
Console.ReadLine();

