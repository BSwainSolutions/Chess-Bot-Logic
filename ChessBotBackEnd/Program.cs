using ChessBotBackEnd.BoardAndPieces;
using ChessBotBackEnd.Helpers;
using System.Net;

Board chessBoard = new Board("///2pK4////");
chessBoard.PrintBoard();
Console.WriteLine("\n");
chessBoard.NumberPrint();
Utils.GetKnightHops(chessBoard, 27);
Console.ReadLine();

