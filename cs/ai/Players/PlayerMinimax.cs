using ai.Board;
using ai.Board.Models;

namespace ai.Players
{
    public class PlayerMinimax
    {
        
        public static int[] NextMove(GameMessage gameMessage)
        {
            var board = new Board.Board(gameMessage.player, gameMessage.board);

            return board.ChooseMinimax().ToArray();
        }
    }
}