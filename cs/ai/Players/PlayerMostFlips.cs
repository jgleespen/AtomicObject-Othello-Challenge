using ai.Board;
using ai.Board.Models;
using static ai.Board.BoardMostFlipsExt;

namespace ai.Players
{
    public class PlayerMostFlips
    {
        public static int[] NextMove(GameMessage gameMessage)
        {
            var board = new Board.Board(gameMessage.player, gameMessage.board);
            var legalMoves = board.FindAllLegalMoves(board.Player, board.OtherPlayer);

            return board.MostFlips(legalMoves).ToArray();
        }
        
    }
}