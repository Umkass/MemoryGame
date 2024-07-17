using UnityEngine;

namespace StaticData.GameSettings
{
    [CreateAssetMenu(fileName = "DefaultGameSettingsData", menuName = "StaticData/DefaultGameSettingsData")]
    public class DefaultGameSettingsData : ScriptableObject
    {
        [field: SerializeField] public SliderSettingData VerticalSize { get; private set; }
        [field: SerializeField] public SliderSettingData HorizontalSize { get; private set; }
        [field: SerializeField] public SliderSettingData MemorizationTime { get; private set; }
        [field: SerializeField] public SliderSettingData GameTime { get; private set; }
        [field: SerializeField] public SliderSettingData SoundVolume { get; private set; }
        [field: SerializeField] public SliderSettingData MusicVolume { get; private set; }
    }
}