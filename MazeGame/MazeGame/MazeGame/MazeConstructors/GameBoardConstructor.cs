using MazeGame.Helpers;
using MazeGame.ViewModels;
using MazeGame.Views;
using Xamarin.Forms;

namespace MazeGame.MazeConstructors
{
    /// <summary>
    /// Methods used for creating gameboard view.
    /// </summary>
    public interface IGameBoardConstructor
    {
        Grid GetGameBoardView(
            MazeViewModel mazeViewModel,
            out Button getHintsButton,
            out Label hintsLeftLabel,
            Command getHintCommand,
            Command upButtonCommand,
            Command downButtonCommand,
            Command leftButtonCommand,
            Command rightButtonCommand);
    }
    
    public class GameBoardConstructor
    {
        public Grid GetGameBoardView(
            MazeViewModel mazeViewModel, 
            out Button getHintsButton, 
            out Label hintsLeftLabel, 
            Command getHintCommand,
            Command upButtonCommand, 
            Command downButtonCommand,
            Command leftButtonCommand,
            Command rightButtonCommand)
        {
            var grid = new Grid();
            
            grid.Children.Add(new Maze
            {
                BindingContext = mazeViewModel
            });
            
            grid.Children.Add(GetControls(upButtonCommand, downButtonCommand, leftButtonCommand, rightButtonCommand));
            
            var hintStack = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.End,
                Margin = new Thickness(10, 0, 0, 10)
            };
            
            getHintsButton = new Button()
            {
                Text = "Get hint",
                Command = getHintCommand
            };
            
            hintsLeftLabel = new Label()
            {
                Text = $"Hints left {GameplayConsts.START_AMOUNT_OF_HINTS}",
                FontSize = 20,
                VerticalTextAlignment = TextAlignment.Center,
            };

            hintStack.Children.Add(getHintsButton);

            hintStack.Children.Add(hintsLeftLabel);

            grid.Children.Add(hintStack);

            return grid;
        }
        
        private StackLayout GetControls(
            Command upButtonCommand, 
            Command downButtonCommand,
            Command leftButtonCommand,
            Command rightButtonCommand)
        {
            var stack = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.End,
                Margin = new Thickness(0, 0, 0, 70)
            };

            stack.Children.Add(new Button()
            {
                Text = "Up",
                WidthRequest = 50,
                HeightRequest = 50,
                Margin = new Thickness(50, 0, 50,0),
                Command = upButtonCommand
            });

            var internalStack = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Spacing = 50
            };
            
            stack.Children.Add(internalStack);
            
            internalStack.Children.Add(new Button()
            {
                Text = "Left",
                WidthRequest = 50,
                HeightRequest = 50,
                Command = leftButtonCommand
            });
            
            internalStack.Children.Add(new Button()
            {
                Text = "Right",
                WidthRequest = 50,
                HeightRequest = 50,
                Command = rightButtonCommand
            });
            
            stack.Children.Add(new Button()
            {
                Text = "Down",
                WidthRequest = 50,
                HeightRequest = 50,
                Margin = new Thickness(50, 0, 50,0),
                Command = downButtonCommand
            });

            return stack;
        }
    }
}