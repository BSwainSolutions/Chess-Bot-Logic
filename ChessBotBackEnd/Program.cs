using ChessBotBackEnd.BoardAndPieces;
using ChessBotBackEnd.Helpers;
using System.Net;
using System.Runtime.CompilerServices;



Board chessBoard = new Board("k7/8/6q1/8/3Q4/8/8/7K");
for (int i = 0; i < 10; i++)
{
    
    bool move = false;
    int SS;
    int ES;
    while(!move)
    {
        List<Move> moves = Utils.LegalMoves(chessBoard,chessBoard.getTurn());
        chessBoard.NumberPrint();
        Console.WriteLine("What is ur starting square");
        //Utils.printLegalMoves(moves);
        int[] AttackedSqaures = Utils.GetAttackedSqaures(chessBoard);
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


