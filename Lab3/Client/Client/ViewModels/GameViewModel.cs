using Client.Presentation.Services.Navigator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Presentation.ViewModels;

public class GameViewModel : BaseViewModel
{
    public GameViewModel(INavigator navigator) : base(navigator)
    {
    }
}
