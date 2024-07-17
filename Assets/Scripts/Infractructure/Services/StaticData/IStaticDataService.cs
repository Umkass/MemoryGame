using StaticData.GameSettings;
using StaticData.View;

namespace Infractructure.Services.StaticData
{
    public interface IStaticDataService
    {
        void LoadAll();
        ViewData GetView(ViewId viewId);
        DefaultGameSettingsData GetDefaultGameSettings();
    }
}