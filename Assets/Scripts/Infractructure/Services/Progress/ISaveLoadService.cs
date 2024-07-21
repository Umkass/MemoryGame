using Data;

namespace Infractructure.Services.Progress
{
    public interface ISaveLoadService
    {
        void SaveGameSettings(SaveLoadId saveLoadId);
        GameSettingsData LoadGameSettings(SaveLoadId saveLoadId);
        void ClearAllSaves();
    }
}