using Data;
using UnityEngine;
using Utils;

namespace Infractructure.Services.Progress
{
    public class SaveLoadService : ISaveLoadService
    {
        private readonly IProgressService _progressService;

        public SaveLoadService(IProgressService progressService)
        {
            _progressService = progressService;
        }

        public void SaveGameSettings(SaveLoadId saveLoadId)
        {
            switch (saveLoadId)
            {
                case SaveLoadId.PlayerPrefs:
                    PlayerPrefs.SetString(ProgressKeys.GameSettings, _progressService.GameSettingsData.ToJson());
                    PlayerPrefs.Save();
                    break;
                case SaveLoadId.JSON:
                    break;
                case SaveLoadId.Base64:
                    break;
                default:
                    PlayerPrefs.SetString(ProgressKeys.GameSettings, _progressService.GameSettingsData.ToJson());
                    PlayerPrefs.Save();
                    break;
            }
        }

        public GameSettingsData LoadGameSettings(SaveLoadId saveLoadId)
        {
            switch (saveLoadId)
            {
                case SaveLoadId.PlayerPrefs:
                    return PlayerPrefs.GetString(ProgressKeys.GameSettings)?
                        .ToDeserialized<GameSettingsData>();
                case SaveLoadId.JSON:
                    return null;
                case SaveLoadId.Base64:
                    return null;
                default:
                    return PlayerPrefs.GetString(ProgressKeys.GameSettings)?
                        .ToDeserialized<GameSettingsData>();
            }
        }
    }
}