using ai.Board;
using ai.Board.Models;
using static ai.Players.PlayerUtil.CornerSideRandomMovePicker;
namespace ai.Players
{
    public class PlayerMinimaxPrioritizeCorners
    {
        
        public static int[] NextMove(GameMessage gameMessage)
        {
            var board = new Board.Board(gameMessage.player, gameMessage.board);
            
            var legalMoves = board.FindAllLegalMoves(board.Player, board.OtherPlayer);
            var pickCorner = CheckCorner(legalMoves);
            
            if (pickCorner.Row != -1)
                return pickCorner.ToArray();
            
            return board.ChooseMinimax().ToArray();
        }
    }
}