using System.Collections.Generic;
using ai.Board.Models;

namespace ai.Board
{
    public static class BoardValidMoveExt
    {
        public const int UpperBound = 7;
        public const int LowerBound = 0;
        
        //Function to check if coordinate is in bounds
        public static bool InBounds(int row, int col) 
            => (row < LowerBound || row > UpperBound || col < LowerBound || col > UpperBound);
        
        /*Function for iterating through the board in a direction.
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

        /*Function for checking if the given neighboring space is held by an enemy
        Also checks for space to make a move 2 places away from board[row, col]*/
        public static bool CheckNextSpaceForEnemy(
            this Board board,
            int changeInRow,
            int changeInCol,
            int row,
            int col
        )
        {
            //Check that we are not searching for valid moves out of bounds to avoid null pointer
            if (InBounds(row + changeInRow, col + changeInCol)) return false;

            if (board.GameBoard[row + changeInRow][col + changeInCol] != board.OtherPlayer) return false;

            if (InBounds(row + changeInRow + changeInRow, col + changeInCol + changeInCol)) return false;

            /*After we check that there is to make a move,
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

            //Iterate over entire board
            for (var row = 0; row < 8; row++)
            {
                for (var col = 0; col < 8; col++)
                {
                    //If there is no piece, we have a potential move - check all 8 directions for a move
                    if (board.GameBoard[row][col] == 0)
                    {
                        //Check northwest path
                        if (board.CheckNextSpaceForEnemy(-1, -1, row, col))
                        {
                            legalMoves.Add(new Coordinate(row, col));
                            continue;
                        }

                        //Check north path
                        if (board.CheckNextSpaceForEnemy(-1, 0, row, col))
                        {
                            legalMoves.Add(new Coordinate(row, col));
                            continue;
                        }

                        //Check northeast path
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

                        //Check eastern path
                        if (board.CheckNextSpaceForEnemy(0, 1, row, col))
                        {
                            legalMoves.Add(new Coordinate(row, col));
                            continue;
                        }

                        //Check southwest path
                        if (board.CheckNextSpaceForEnemy(1, -1, row, col))
                        {
                            legalMoves.Add(new Coordinate(row, col));
                            continue;
                        }

                        //Check southern path
                        if (board.CheckNextSpaceForEnemy(1, 0, row, col))
                        {
                            legalMoves.Add(new Coordinate(row, col));
                            continue;
                        }

                        //Check southeast path
                        if (board.CheckNextSpaceForEnemy(1, 1, row, col))
                            legalMoves.Add(new Coordinate(row, col));
                    }
                }
            }
            return legalMoves;
        }
    }
}