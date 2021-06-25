using MazeGame.Helpers;
using System.Collections.Generic;
using MazeGame.MazeConstructors;
using Xamarin.Forms;

namespace MazeGame.ViewModels
{
    public class MazeViewModel
    {
        public MazeViewModel(MazeSettings settings)
        {
            Settings = settings;
            var constructor = new MazeConstructor();
            CellsViewModelsList = constructor.GetCellsViewModelsList(Settings);
            Content = constructor.GetMazeVisualization(CellsViewModelsList, Settings);
        }

        public Grid Content { get; set; }
        public IEnumerable<CellViewModel> CellsViewModelsList { get; set; }
        public MazeSettings Settings { get; set; }
    }
}
