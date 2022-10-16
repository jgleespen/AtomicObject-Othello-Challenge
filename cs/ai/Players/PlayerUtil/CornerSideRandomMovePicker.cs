using System;
using System.Collections.Generic;
using ai.Board.Models;

namespace ai.Players.PlayerUtil
{
    //Checks for corners, then sides, then returns random move if no corners or sides are present
    public static class CornerSideRandomMovePicker
    {
        private const int UpperCornerRow = 0;
        private const int LowerCornerRow = 7;

        private const int StartCornerCol = 0;
        private const int EndCornerCol = 7;

        //Check if there is a corner move to be made
        public static Coordinate CheckCorner(List<Coordinate> legalMoves)
        {
            //Check legal moves for corner moves
            foreach (var c in legalMoves)
            {
                if (c.Row == UpperCornerRow && c.Col == StartCornerCol)
                    return c;

                if (c.Row == LowerCornerRow && c.Col == StartCornerCol)
                    return c;

                if (c.Row == UpperCornerRow && c.Col == EndCornerCol)
                    return c;

                if (c.Row == LowerCornerRow && c.Col == EndCornerCol)
                    return c;
            }

            return new Coordinate(-1, -1);
        }

        //Check if there is a side move to be made as those are better than center moves
        public static Coordinate CheckSide(List<Coordinate> legalMoves)
        {
            foreach (var c in legalMoves)
            {
                if (1 < c.Row && c.Row < 6 && (c.Col == 0 || c.Col == 7))
                    return c;
                if (1 < c.Col && c.Col < 6 && (c.Row == 0 || c.Row == 7))
                    return c;
            }

            return new Coordinate(-1, -1);
        }

        public static int[] PickMove(List<Coordinate> legalMoves, int player)
        {
            //Check for corner spots
            var potentialCorner = CheckCorner(legalMoves);
            if (potentialCorner.Row != -1 && potentialCorner.Col != -1)
            {
                return potentialCorner.ToArray();
            }

            //check for side spots
            var potentialSide = CheckSide(legalMoves);
            if (potentialSide.Row != -1 && potentialSide.Col != -1)
                return potentialSide.ToArray();

            return RandomPick(legalMoves);
        }
        
        
        //Randomly pick a move from the legal moves list
        public static int[] RandomPick(List<Coordinate> legalMoveList)
        {
            var range = new Random().Next(0, legalMoveList.Count);
            return legalMoveList[range].ToArray();
        }
        
    }
}