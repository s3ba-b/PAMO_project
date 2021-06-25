using MazeGame.Helpers;
using MazeGame.MazeConstructors;
using Xamarin.Forms;

namespace MazeGame.ViewModels
{
    public class CellViewModel
    {
        private readonly SquareColorViewModel _SquareColorViewModel;

        public CellViewModel(int id, double topLeftX, double topLeftY, int size) 
        {
            Id = id;
            Height = size;
            Width = size;
            _SquareColorViewModel = new SquareColorViewModel(topLeftX, topLeftY, Height, Width);
            
            var constructor = new MazeConstructor();
            Content = constructor.GetCellView(topLeftX, topLeftY, size, _SquareColorViewModel);
        }

        public int Id { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        public Grid Content { get; set; }
        public ESquareState State
        {
            get => _SquareColorViewModel.State;
            set => _SquareColorViewModel.State = value;
        }
    }
}
