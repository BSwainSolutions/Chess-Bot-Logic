using ChessBotBackEnd.BoardAndPieces;
using ChessBotBackEnd.Helpers;
using System.Net;
using System.Runtime.CompilerServices;



Board chessBoard = new Board();
for (int i = 0; i < 10; i++)
{
    
    bool move = false;
    int SS;
    int ES;
    while(!move)
    {
        List<Move> moves = Utils.FilterLegalMoves(Utils.PossibleMoves(chessBoard,chessBoard.getTurn()),chessBoard);
        chessBoard.NumberPrint();
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


