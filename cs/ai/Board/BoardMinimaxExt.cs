﻿using System;
using ai.Board.Models;
using ai.Players;

namespace ai.Board
{
    public static class BoardMinimaxExt
    {
        //BRUH
        private const int DepthLimit = 5;

        /*I implemented minimax using a heuristic that tests potential scores and then returns their difference
        This evaluates scores utilizing functions from PerformMoveOnBoard.cs to check possible moves*/
        private static int MinimaxHeuristic(this Board board)
        {
            var otherPlayer = 1;
            if (board.Player == 1)
                otherPlayer = 2;

            var playerScore = board.Score(board.Player);
            var otherPlayerScore = board.Score(otherPlayer);
            return (playerScore - otherPlayerScore);
        }

        //Counts a players score on the board for input to Heuristic()
        private static int Score(this Board board, int player)
        {
            var total = 0;
            for (var row = 0; row < 8; row++)
            {
                for (var col = 0; col < 8; col++)
                {
                    if (board.GameBoard[row][col] == player)
                        total++;
                }
            }

            return total;
        }

        /*This function recursively makes moves on a copy of our current game board
         To find the best potential minimax value for a move. Once our move is found it is
         returned as a Coordinate.*/
        public static Coordinate ChooseMinimax(
            this Board board
        )
        {
            //Get a list of legal moves
            var legalMoves = board.FindAllLegalMoves();

            var bestMoveValue = Int64.MinValue; //Holds best move value
            var bestCoord = legalMoves[0]; //Holds best coordinate

            if (legalMoves.Count == 0)


                for (var i = 0; i < legalMoves.Count; i++)
                {
                    //Copy current board state so we can perform recursion and test moves
                    var boardCopy = (Board)board.Clone();

                    //Perform move on board using current iteration of legalMoves
                    boardCopy.DoMove(legalMoves[i]);

                    //Search for best move with call to MinimaxValue, starting at depth of 1
                    var minimaxValue = boardCopy.MinimaxValue(boardCopy.Player, 1);

                    //If we have found a better move value update bestMoveValue and bestCoord
                    if (minimaxValue > bestMoveValue)
                    {
                        bestMoveValue = minimaxValue;
                        bestCoord = legalMoves[i];
                    }
                }

            return bestCoord;
        }

        /*Recursively performs moves for each player on a copy of the board
         until our desired search depth is reached. Track minimum values for our opponent and
         maximum values for our player.*/
        private static Int64 MinimaxValue(
            this Board board,
            int currentPlayer,
            int depth
        )
        {
            //If we have reached our maximum desired search depth - evaluate score using Heuristic function
            if (depth == DepthLimit)
            {
                return board.MinimaxHeuristic();
            }

            /*Identify current player to make a move for their pieces
            this switches which player makes a move on each recursive call*/
            var opponent = 1;
            if (currentPlayer == 1)
                opponent = 2;

            //Get list of legal moves for our 'opponent' to check for best result
            var legalMoves = board.FindAllLegalMoves();


            if (legalMoves.Count == 0)
            {
                return board.MinimaxValue(opponent, depth + 1);
            }
            else
            {
                //If currentPlayer is the opponent, minimize
                var bestMoveValue = Int64.MinValue;

                //If currentPlayer is not our player, maximize 
                if (board.Player != currentPlayer)
                    bestMoveValue = Int64.MaxValue;

                for (var i = 0; i < legalMoves.Count; i++)
                {
                    var boardCopy = (Board)board.Clone();

                    //Perform move on copy of board
                    boardCopy.DoMove(legalMoves[i]);

                    //Recursively call until depth is reached
                    var minimaxValue = boardCopy.MinimaxValue(opponent, depth + 1);

                    //If we are on our player, update bestMoveValue with maximum value
                    if (board.Player == currentPlayer)
                    {
                        if (minimaxValue > bestMoveValue)
                            bestMoveValue = minimaxValue;
                    }

                    //If we are on an opponent turn, update bestMoveValue with minimum value
                    else
                    {
                        if (minimaxValue < bestMoveValue)
                            bestMoveValue = minimaxValue;
                    }
                }

                return bestMoveValue;
            }
        }
    }
}