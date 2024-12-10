using Client.Domain.ViewModels;

namespace Client.Domain.Services.Navigator;

public interface INavigator
{
    BaseViewModel CurrentView { get; set; }

    void NavigateTo<T>() where T : BaseViewModel;
}
