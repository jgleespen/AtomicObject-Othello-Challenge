using ai.Board.Models;
using ai.Players;
using static ai.Board.BoardValidMoveExt;

namespace ai.Board
{
    public static class BoardPerformMoveExt
    {
        /*Function that first checks for a direction that a move can be made
         starting from a legal move coordinate and checking for consecutive enemy pieces until:
         * A board bound is hit (false)
         * An empty spot is hit (false)
         * Or until a player piece is hit (true)
         * Then performs the move by passing in a board to Flip ()*/
        public static void DoMove(
            this Board board,
            Coordinate coord
        )
        {
            int row = coord.Row;
            int col = coord.Col;
            
            //Check south
            if (board.CheckToFlip(row - 1, col, -1, 0))  
                board.Flip(row - 1, col, -1, 0);
            
            //Check north
            if (board.CheckToFlip(row + 1, col, 1, 0))
                board.Flip(row + 1, col, 1, 0);
            
            //Check west
            if (board.CheckToFlip(row, col - 1, 0, -1))
                board.Flip(row, col - 1, 0, -1);
            
            //Check east
            if (board.CheckToFlip( row, col + 1, 0, 1))
                board.Flip(row, col + 1, 0, 1);
            
            //Check south west
            if (board.CheckToFlip(row - 1, col - 1, -1, -1))
                board.Flip(row - 1, col - 1, -1, -1);
            
            //Check north west
            if (board.CheckToFlip(row + 1, col - 1, 1, -1))
                board.Flip(row + 1, col - 1, 1, -1);
            
            //Check south east
            if (board.CheckToFlip(row - 1, col + 1, -1, 1))
                board.Flip(row - 1, col + 1, -1, 1);
            
            //Check south west
            if (board.CheckToFlip(row + 1, col + 1, 1, 1))
                board.Flip(row + 1, col + 1, 1, 1);
        }

        /* Function for checking a direction that a potential move can be made
         * General Logic flow is: 
         * First check that our current place is in bounds 
         * Next check if our current place is empty (false)
         * Next check if our current place is not an enemy piece (false)
         * Then check if we have reached our piece, if not we need to continue
         * Check next coordinate is in bounds
         * Recurse to the next potential end coordinate*/
        private static bool CheckToFlip(
            this Board board,
            int row,
            int col,
            int changeInRow,
            int changeInCol
        )
        {
            //Check our coordinate is in bounds
            if (InBounds(row, col)) return false;

            //Check for empty coordinate
            if (board.GameBoard[row][col] != board.OtherPlayer) return false;
            
            //Check if we reached our player - true base case
            if (board.GameBoard[row][col] == board.Player) return true;
            
            //Check next next potential end coordinate is in bounds
            if (InBounds(row + changeInRow, col + changeInCol)) return false;

            return CheckToFlip(
                board,
                row + changeInRow,
                col + changeInCol,
                changeInRow,
                changeInCol
            );
        }

        /*Function for flipping pieces on a copy of the board to test move outcome for minimax
        * First check that our current coordinate is in bounds
         * Loop over sub array on board while the current place is held by an enemy
         * Flip each piece
         * Increment until bound is reached or we reach our own piece
         * Loop ends*/
        private static bool Flip(
            this Board board,
            int row,
            int col,
            int changeInRow,
            int changeInCol
        )
        {
            while (true)
            {
                if (board.GameBoard[row][col] != board.OtherPlayer) return false;
                if(InBounds(row, col)) return false;

                board.GameBoard[row][col] = board.Player;

                row += changeInRow;
                col += changeInCol;
            }
        }
    }
}