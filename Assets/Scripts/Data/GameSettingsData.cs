using System;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class GameSettingsData
    {
        [field: SerializeField] public int VerticalSize { get; set; }
        [field: SerializeField] public int HorizontalSize { get; set; }
        [field: SerializeField] public int MemorizationTime { get; set; }
        [field: SerializeField] public int GameTime { get; set; }
        [field: SerializeField] public int SoundVolume { get; set; }
        [field: SerializeField] public int MusicVolume { get; set; }

        public override string ToString()
        {
            return $"VerticalSize: {VerticalSize}, " +
                   $"HorizontalSize: {HorizontalSize}, " +
                   $"MemorizationTime: {MemorizationTime}, " +
                   $"GameTime: {GameTime}, " +
                   $"SoundVolume: {SoundVolume}, " +
                   $"MusicVolume: {MusicVolume}";
        }
    }
}