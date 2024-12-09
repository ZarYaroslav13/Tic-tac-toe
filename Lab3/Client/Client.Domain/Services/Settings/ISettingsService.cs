using Client.Domain.Services.Settings.GameSettingsService;
using Client.Domain.Services.Settings.PortSettingsService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Domain.Services.Settings; 

public interface ISettingsService
{
    public IGameSettingsService GetGameSettings();

    public IPortSettingsService GetPortSettings();
}
