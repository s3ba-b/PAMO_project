using Q_Learning;

namespace MazeGame.Helpers
{
    /// <summary>
    /// Data structure used for logic in game.
    /// </summary>
    public class MazeSettings
    {
        public MazeModel Model { get; set; }
        public double XPos { get; set; }
        public double YPos { get; set; }
        public double MazeWidth => Model.QuantityOfColumns * Model.SizeOfCell;
        public double MazeHeight => Model.QuantityOfRows * Model.SizeOfCell;
        public double StartXPos => XPos - MazeWidth / 2;
        public double StartYPos => YPos - MazeHeight / 2;
        public double WindowWidth { get; set; }
        public double WindowHeight { get; set; }
    }
}
