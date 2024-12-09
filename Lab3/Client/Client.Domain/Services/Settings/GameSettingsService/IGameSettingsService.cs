using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Domain.Services.Settings.GameSettingsService;

public interface IGameSettingsService
{
    public GameMode GetGameMode();

    public void ChangeGameMode(GameMode mode);
}
