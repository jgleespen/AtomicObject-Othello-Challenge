using System;

namespace ai.Board
{
    public class Board : ICloneable
    {
        public const int OtherPlayerDefault = 1;
        public int[][] GameBoard { get; }
        public int Player { get; set; }
        public int OtherPlayer { get; set; }

        public Board(int player, int[][] board)
        {
            Player = player;
            GameBoard = board;

            OtherPlayer = OtherPlayerDefault;

            //If player is 1 switch OtherPlayer
            if (player == 1)
                OtherPlayer = 2;
        }
        public object Clone()
        {
            var boardCopy = new Board(Player, new int[GameBoard.Length][]);
            
            for (var i = 0; i < boardCopy.GameBoard.Length; i++)
                boardCopy.GameBoard[i] = new int[GameBoard.Length];

            for (var i = 0; i < GameBoard.Length; i++)
            {
                var row = GameBoard[i];
                Array.Copy(row, 0, boardCopy.GameBoard[i], 0, row.Length);
            }
            return boardCopy;
        }
    }
}