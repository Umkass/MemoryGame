using System;
using System.IO;
using System.Text;
using Data;
using Infractructure.Services.Progress;
using UnityEngine;
using Utils;

namespace Infractructure.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private readonly IProgressService _progressService;

        public SaveLoadService(IProgressService progressService) =>
            _progressService = progressService;

        public void SaveGameSettings(SaveLoadId saveLoadId)
        {
            string jsonData = _progressService.GameSettingsData.ToJson();

            switch (saveLoadId)
            {
                case SaveLoadId.PlayerPrefs:
                    PlayerPrefs.SetString(ProgressKeys.GameSettings, jsonData);
                    PlayerPrefs.Save();
                    break;
                case SaveLoadId.JSON:
                    File.WriteAllText(FilePaths.JSONFilePath, jsonData);
                    break;
                case SaveLoadId.Base64:
                    string base64Data = Convert.ToBase64String(Encoding.UTF8.GetBytes(jsonData));
                    File.WriteAllText(FilePaths.Base64FilePath, base64Data);
                    break;
                default:
                    PlayerPrefs.SetString(ProgressKeys.GameSettings, jsonData);
                    PlayerPrefs.Save();
                    break;
            }
        }

        public GameSettingsData LoadGameSettings(SaveLoadId saveLoadId)
        {
            string jsonData = null;

            switch (saveLoadId)
            {
                case SaveLoadId.PlayerPrefs:
                    jsonData = PlayerPrefs.GetString(ProgressKeys.GameSettings);
                    break;
                case SaveLoadId.JSON:
                    if (File.Exists(FilePaths.JSONFilePath))
                    {
                        jsonData = File.ReadAllText(FilePaths.JSONFilePath);
                    }
                    break;
                case SaveLoadId.Base64:
                    if (File.Exists(FilePaths.Base64FilePath))
                    {
                        string base64Data = File.ReadAllText(FilePaths.Base64FilePath);
                        jsonData = Encoding.UTF8.GetString(Convert.FromBase64String(base64Data));
                    }
                    break;
                default:
                    jsonData = PlayerPrefs.GetString(ProgressKeys.GameSettings);
                    break;
            }

            return jsonData?.ToDeserialized<GameSettingsData>();
        }

        public void ClearAllSaves()
        {
            PlayerPrefs.DeleteKey(ProgressKeys.GameSettings);
            PlayerPrefs.Save();

            if (File.Exists(FilePaths.JSONFilePath))
                File.Delete(FilePaths.JSONFilePath);

            if (File.Exists(FilePaths.Base64FilePath))
                File.Delete(FilePaths.Base64FilePath);
        }
    }
}