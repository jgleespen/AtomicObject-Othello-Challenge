using System.Collections.Generic;
using ai.Board.Models;
using static ai.Board.BoardValidMoveExt;

namespace ai.Board
{
    public static class BoardMostFlipsExt
    {
        public static Coordinate MostFlips(this Board board, List<Coordinate> legalMoves)
        {
            //get legal moves
            //do moves and return max flips

            var result = 0;
            var bestMove = legalMoves[0];

            for (int i = 0; i < legalMoves.Count; i++)
            {
                var flip = board.DoMoveCountFlips(legalMoves[i]);
                if (flip > result)
                {
                    bestMove = legalMoves[i];
                }
            }

            return bestMove;
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

        /*This function is just a variation of the one in BoardPerformMoveExt.cs
         This function tracks number of flips*/
        
        /*Function for flipping pieces on a copy of the board to test move outcome for minimax
        * First check that our current coordinate is in bounds
         * Loop over sub array on board while the current place is held by an enemy
         * Flip each piece
         * Increment until bound is reached or we reach our own piece
         * Loop ends*/
        private static int Flip(
            this Board board,
            int row,
            int col,
            int changeInRow,
            int changeInCol
        )
        {
            var result = 0;
            //Check bounds
            if (InBounds(row, col)) return result;

            while (board.GameBoard[row][col] == board.OtherPlayer)
            {
                board.GameBoard[row][col] = board.Player;

                //Check next potential end coordinate is in bounds
                if (InBounds(row + changeInRow, col + changeInCol)) return result;

                result++;
                row += changeInRow;
                col += changeInCol;
            }

            return result;
        }
        
        /*This function is just a variation of the one in BoardPerformMoveExt.cs
         This function tracks and returns number of flips to determine a 'good' move*/
        public static int DoMoveCountFlips(
            this Board board,
            Coordinate coord
        )
        {
            int row = coord.Row;
            int col = coord.Col;
            var result = 0;
            var flip = 0;
            
            //Check south
            if (board.CheckToFlip(row - 1, col, -1, 0))
            {
                flip = board.Flip(row - 1, col, -1, 0);
                if (flip > result)
                    result =+ flip;
            }

            //Check north
            if (board.CheckToFlip(row + 1, col, 1, 0))
            {
                flip = board.Flip(row + 1, col, 1, 0);
                if (flip > result)
                    result += flip;
            }

            //Check west
            if (board.CheckToFlip(row, col - 1, 0, -1))
            {
                
                flip = board.Flip(row, col - 1, 0, -1);
                if (flip > result)
                    result += flip;
            }

            //Check east
            if (board.CheckToFlip(row, col + 1, 0, 1))
            {
                board.Flip(row, col + 1, 0, 1);
                if (flip > result)
                    result += flip;
            }

            //Check south west
            if (board.CheckToFlip(row - 1, col - 1, -1, -1))
            {
                board.Flip(row - 1, col - 1, -1, -1);
                if (flip > result)
                    result += flip;

            }

            //Check north west
            if (board.CheckToFlip(row + 1, col - 1, 1, -1))
            {
                board.Flip(row + 1, col - 1, 1, -1);
                if (flip > result)
                    result += flip;
            }

            //Check south east
            if (board.CheckToFlip(row - 1, col + 1, -1, 1))
            {
                board.Flip(row - 1, col + 1, -1, 1);
                if (flip > result)
                    result += flip;
            }

            //Check south west
            if (board.CheckToFlip(row + 1, col + 1, 1, 1))
            {
                board.Flip(row + 1, col + 1, 1, 1);
                if (flip > result)
                    result += flip;
            }

            return result;
        }
    }
}