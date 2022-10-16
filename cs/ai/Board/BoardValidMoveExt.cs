using System.Collections.Generic;
using ai.Board.Models;
using ai.Players;
using static ai.Players.PlayerUtil.InBoundsUtil;
namespace ai.Board
{
    public static class BoardValidMoveExt
    {
        /* Function for iterating through the board in a direction.
         Checks that each place is either held by an enemy or our own piece
         If an empty space is hit, or pointer leaves board points - return false
         If our player is found - return true*/
        public static bool CheckMove(
            this Board board,
            int changeInRow,
            int changeInCol,
            int row,
            int col
        )
        {
            while (true)
            {
                if (board.GameBoard[row][col] == board.Player)
                {
                    return true;
                }

                if (board.GameBoard[row][col] != board.OtherPlayer)
                {
                    return false;
                }

                if (InBounds(row + changeInRow, col + changeInCol)) return false;

                row += changeInRow;
                col += changeInCol;
            }
        }

        //Function for checking if the given neighboring space is held by an enemy
        //Also checks for space to make a move 2 places away from board[row, col]
        public static bool CheckNextSpaceForEnemy(
            this Board board,
            int changeInRow,
            int changeInCol,
            int row,
            int col
        )
        {
            //check that we are not searching for valid moves out of bounds to avoid null pointer
            if (InBounds(row + changeInRow, col + changeInCol)) return false;

            if (board.GameBoard[row + changeInRow][col + changeInCol] != board.OtherPlayer) return false;

            if (InBounds(row + changeInRow + changeInRow, col + changeInCol + changeInCol)) return false;

            /*after we check that there is to make a move,
             and that an adjacent cell is held by the opposing player
             we need to iterate in the current 'direction' while checking 
             if each place is held by the opposing player.*/
            return CheckMove(
                board,
                changeInRow,
                changeInCol,
                row + changeInRow + changeInRow,
                col + changeInCol + changeInCol
            );
        }

        public static List<Coordinate> FindAllLegalMoves(
            this Board board
        )
        {
            var legalMoves = new List<Coordinate>();

            //iterate over entire board
            for (var row = 0; row < 8; row++)
            {
                for (var col = 0; col < 8; col++)
                {
                    //if there is no piece, we have a potential move - check all 8 directions for a move
                    if (board.GameBoard[row][col] == 0)
                    {
                        //Check northwest path
                        if (board.CheckNextSpaceForEnemy(-1, -1, row, col))
                        {
                            legalMoves.Add(new Coordinate(row, col));
                            continue;
                        }

                        //check north path
                        if (board.CheckNextSpaceForEnemy(-1, 0, row, col))
                        {
                            legalMoves.Add(new Coordinate(row, col));
                            continue;
                        }

                        //check northeast path
                        if (board.CheckNextSpaceForEnemy(-1, 1, row, col))
                        {
                            legalMoves.Add(new Coordinate(row, col));
                            continue;
                        }

                        //Check western path
                        if (board.CheckNextSpaceForEnemy(0, -1, row, col))
                        {
                            legalMoves.Add(new Coordinate(row, col));
                            continue;
                        }

                        //check eastern path
                        if (board.CheckNextSpaceForEnemy(0, 1, row, col))
                        {
                            legalMoves.Add(new Coordinate(row, col));
                            continue;
                        }

                        //check southwest path
                        if (board.CheckNextSpaceForEnemy(1, -1, row, col))
                        {
                            legalMoves.Add(new Coordinate(row, col));
                            continue;
                        }

                        //check southern path
                        if (board.CheckNextSpaceForEnemy(1, 0, row, col))
                        {
                            legalMoves.Add(new Coordinate(row, col));
                            continue;
                        }

                        //check southeast path
                        if (board.CheckNextSpaceForEnemy(1, 1, row, col))
                            legalMoves.Add(new Coordinate(row, col));
                    }
                }
            }
            return legalMoves;
        }
    }
}