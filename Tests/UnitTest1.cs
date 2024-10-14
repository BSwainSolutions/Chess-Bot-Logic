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
            List<Move> list = Utils.PossibleMoves(newBoard,newBoard.getTurn());
            Assert.Equal(list.Count,20);
        }
        [Fact]
        public void FirstMoveAttackedSqaures()
        {
            //Board newBoard = new Board();
            //Utils.GetAttackedSqaures(newBoard);
            //Assert.Equal(8, newBoard.getAttackedSqaures().Length);
        }

        [Fact]
        public void QueenAttackedSqaures()
        {
            //Board newBoard = new Board("k7/8/6q1/8/3Q4/8/8/7K");
            //Utils.GetAttackedSqaures(newBoard);
            //Assert.Equal(26, newBoard.getAttackedSqaures().Length);
        }

        [Fact]
        public void TestForCheck()
        {
            Board newB = new Board("4q3/8/2k2K2/8/8/8/8/8");
            List<Move> legalMoves = Utils.FilterLegalMoves(Utils.PossibleMoves(newB, (int)PieceColour.White), newB);
            Assert.Equal(3,legalMoves.Count);
        }
    }
}