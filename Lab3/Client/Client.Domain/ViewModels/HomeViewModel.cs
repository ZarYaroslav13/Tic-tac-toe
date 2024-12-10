using Client.Domain.Services.Navigator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Domain.ViewModels;

public class HomeViewModel : BaseViewModel
{
    public HomeViewModel(INavigator navigator) : base(navigator)
    {
    }
}
