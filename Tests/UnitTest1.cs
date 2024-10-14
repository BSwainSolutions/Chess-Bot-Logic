using ChessBotBackEnd.Helpers;
using ChessBotBackEnd.BoardAndPieces;
using Newtonsoft.Json.Bson;
namespace Tests
{
    public class LegalMovesTest
    {
        [Fact]
        public void FirstPossibleMoves()
        {
            Board newBoard = new Board();
            List<Move> list = Utils.LegalMoves(newBoard,newBoard.getTurn());
            Assert.Equal(list.Count,20);
        }
        [Fact]
        public void FirstMoveAttackedSqaures()
        {
            Board newBoard = new Board();
            Utils.UpdateAttackedSqaures(newBoard);
            Assert.Equal(8, newBoard.getAttackedSqaures().Length);
        }

        [Fact]
        public void QueenAttackedSqaures()
        {
            Board newBoard = new Board("k7/8/6q1/8/3Q4/8/8/7K");
            Utils.UpdateAttackedSqaures(newBoard);
            Assert.Equal(26, newBoard.getAttackedSqaures().Length);
        }
    }
}