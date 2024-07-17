using System;
using UnityEngine;

namespace StaticData.GameSettings
{
    [Serializable]
    public class SliderSettingData
    {
        [SerializeField] [Tooltip("Range is 0-99")]
        private int min = 0;

        [SerializeField] [Tooltip("Range is 1-100")]
        private int max = 100;

        [SerializeField] [Tooltip("Range is Min-Max")]
        private int defaultValue = 50;

        public int Min
        {
            get => min;
            private set => min = value;
        }

        public int Max
        {
            get => max;
            private set => max = value;
        }

        public int DefaultValue
        {
            get => defaultValue;
            private set => defaultValue = value;
        }
    }
}