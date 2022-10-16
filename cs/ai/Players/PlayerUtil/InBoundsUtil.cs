namespace ai.Players.PlayerUtil
{
    public static class InBoundsUtil
    {
        public static bool InBounds(int row, int col) 
            => (row < 0 || row > 7 || col < 0 || col > 7);
    }
}