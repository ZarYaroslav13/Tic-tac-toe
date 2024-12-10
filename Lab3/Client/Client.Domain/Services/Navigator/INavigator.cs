using Client.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Domain.Services.Navigator;

public interface INavigator
{
    BaseViewModel CurrentView { get; set; }

    void NavigateTo<T>() where T : BaseViewModel;
}
