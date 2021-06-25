using Xamarin.Forms;

namespace MazeGame.MazeConstructors
{
    /// <summary>
    /// Methods with logic creating a color for cell.
    /// </summary>
    public interface IMazeSquareColorConstructor
    {
        BoxView GetSquareColorView(double size, double topLeftX, double topLeftY);
    }
    
    public class MazeSquareColorConstructor : IMazeSquareColorConstructor
    {
        private const double CanvasPadding = 3;
        
        public BoxView GetSquareColorView(double size, double topLeftX, double topLeftY) =>
            new BoxView()
            {
                WidthRequest = size - (CanvasPadding * 2),
                HeightRequest = size - (CanvasPadding * 2),
                Margin = new Thickness(topLeftX + CanvasPadding, topLeftY + CanvasPadding, 0, 0),
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Start
            };
    }
}