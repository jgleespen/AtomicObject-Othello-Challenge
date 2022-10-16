namespace ai.Board.Models
{
    public class Coordinate
    {
        public int Row { get; set; }
        public int Col { get; set; }

        public Coordinate(int row, int col)
        {
            Row = row;
            Col = col;
        }

        public int[] ToArray()
        {
            return new[] { Row, Col };
        }

        public override string ToString()
        {
            return $"[{Row}, {Col}]";
        }
    }
}