using ChessBotBackEnd.BoardAndPieces;
using ChessBotBackEnd.Helpers;
using System.Net;
using System.Runtime.CompilerServices;



Board chessBoard = new Board("4q3/8/2k2K2/8/8/8/8/8");
for (int i = 0; i < 10; i++)
{
    bool move = false;
    int SS;
    int ES;
    while(!move)
    {
        chessBoard.NumberPrint();
        List<Move> moves = Utils.FilterLegalMoves(Utils.PossibleMoves(chessBoard,chessBoard.getTurn()),chessBoard);
        Utils.PrintLegalMoves(moves);
        Console.WriteLine("What is ur starting square");
        //Utils.printLegalMoves(moves);
        Console.WriteLine(moves.Count());
        SS = int.Parse(Console.ReadLine());
        Console.WriteLine("What is ur ending square");
        ES = int.Parse(Console.ReadLine());
        if (moves.Contains(new Move(SS,ES)))
        {
            chessBoard.MovePiece(SS, ES);
        }
    }

}


