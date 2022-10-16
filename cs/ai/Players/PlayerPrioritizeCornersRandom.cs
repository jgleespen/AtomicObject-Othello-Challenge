using ai.Board;
using ai.Board.Models;
using static ai.Players.PlayerUtil.CornerSideRandomMovePicker;

namespace ai.Players
{
    public static class PlayerPrioritizeCornersRandom
    {
        public static int[] NextMove(GameMessage gameMessage)
        {
            var board = new Board.Board(gameMessage.player, gameMessage.board);
            var legalMoves = board.FindAllLegalMoves();
            return PickMove(legalMoves, board.Player);
        }
        
    }
}