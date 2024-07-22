using System.IO;
using UnityEngine;

namespace Data
{
    public static class FilePaths
    {
        public const string ViewsDataPath = "StaticData/UI/ViewsData";
        public const string DefaultGameSettingsDataPath = "StaticData/DefaultGameSettingsData";
        public const string CardsDataPath = "StaticData/CardsData";

        public static readonly string JSONFilePath = Path.Combine(Application.persistentDataPath, "GameSettings.json");
        public static readonly string Base64FilePath = Path.Combine(Application.persistentDataPath, "GameSettingsBase64.txt");
    }
}