using ChessBotBackEnd.Helpers;
using ChessBotBackEnd.BoardAndPieces;
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
    }
}