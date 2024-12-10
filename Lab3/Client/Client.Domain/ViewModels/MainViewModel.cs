using Client.Domain.Services.Navigator;

namespace Client.Domain.ViewModels;

public class MainViewModel : BaseViewModel
{
    public MainViewModel(INavigator navigator) : base(navigator)
    {
        navigator.NavigateTo<HomeViewModel>();
    }
}
