namespace Q_Learning
{
    public class MazeModel
    {
        public int Id { get; set; }
        public int QuantityOfSquares { get; set; }
        public int QuantityOfColumns { get; set; }
        public int QuantityOfRows { get; set; }
        public int SizeOfCell { get; set; }
        public int Start { get; set; }
        public int Goal { get; set; }
        public int[][] Matrix { get; set; }
        public double[][] Reward { get; set; }
        public double[][] Quality { get; set; }
    }
}
