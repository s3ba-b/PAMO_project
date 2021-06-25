using System.Threading.Tasks;
using Xamarin.Forms;

namespace MazeGame.ViewModels
{
    public class StartScreenViewModel
    {
        private readonly INavigation _navigation;
        
        public static async Task<StartScreenViewModel> GetStartScreenViewModel(INavigation navigation)
        {
            var thisClass = new StartScreenViewModel(navigation);
            await thisClass.MoveToMenuAsync();
            return thisClass;
        }
        
        private StartScreenViewModel(INavigation navigation)
        {
            _navigation = navigation;
        }

        private async Task MoveToMenuAsync()
        {
            await Task.Delay(3000); 
            await _navigation.PushAsync(new Views.Menu());
        }
    }
}