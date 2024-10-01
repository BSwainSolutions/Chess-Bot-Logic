using ChessBotBackEnd.BoardAndPieces;
using ChessBotBackEnd.Helpers;
using System.Net;

Board chessBoard = new Board();
chessBoard.PrintBoard();
Console.WriteLine("\n");
Utils.LegalMoves(chessBoard);
Console.ReadLine();

