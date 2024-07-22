using Data;

namespace Infractructure.Services.SaveLoad
{
    public interface ISaveLoadService
    {
        void SaveGameSettings(SaveLoadId saveLoadId);
        GameSettingsData LoadGameSettings(SaveLoadId saveLoadId);
        void ClearAllSaves();
    }
}