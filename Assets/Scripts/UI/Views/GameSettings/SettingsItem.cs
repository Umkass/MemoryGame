using Data;
using Infractructure.Services.Progress;
using Infractructure.Services.StaticData;
using Infractructure.StateMachine;
using StaticData.GameSettings;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views.GameSettings
{
    public class SettingsItem : MonoBehaviour
    {
        [SerializeField] private Slider _verticalSize;
        [SerializeField] private Slider _horizontalSize;
        [SerializeField] private Slider _memorizationTime;
        [SerializeField] private Slider _gameTime;
        [SerializeField] private Slider _soundVolume;
        [SerializeField] private Slider _musicVolume;
        [SerializeField] private Button _btnPlay;
        [SerializeField] private Button _btnSaveJSONAndPlay;
        [SerializeField] private Button _btnSavePlayerPrefsAndPlay;
        [SerializeField] private Button _btnSaveBase64AndPlay;
        private IGameStateMachine _stateMachine;
        private IStaticDataService _staticDataService;
        private ISaveLoadService _saveLoadService;
        private IProgressService _progressService;

        public void Construct(IGameStateMachine stateMachine, IStaticDataService staticDataService,
            ISaveLoadService saveLoadService, IProgressService progressService)
        {
            _stateMachine = stateMachine;
            _staticDataService = staticDataService;
            _saveLoadService = saveLoadService;
            _progressService = progressService;
        }

        public void Initialize()
        {
            GameSettingsData gameSettings = _saveLoadService.LoadGameSettings(SaveLoadId.PlayerPrefs);
            DefaultGameSettingsData defaultGameSettings = _staticDataService.GetDefaultGameSettings();
            InitializeMinMaxSettings(defaultGameSettings);
            if (gameSettings == null)
            {
                InitializeDefaultGameSettings(defaultGameSettings);
            }
            else
            {
                InitializeSavedGameSettings(gameSettings);
            }


            _btnPlay.onClick.AddListener(OnPlayClicked);
            _btnSaveJSONAndPlay.onClick.AddListener(OnSaveJSONAndPlay);
            _btnSavePlayerPrefsAndPlay.onClick.AddListener(OnSavePlayerPrefsAndPlay);
            _btnSaveBase64AndPlay.onClick.AddListener(OnSaveBase64AndPlay);
            _verticalSize.onValueChanged.AddListener(OnVerticalSizeChanged);
            _horizontalSize.onValueChanged.AddListener(OnHorizontalSizeChanged);
        }

        private void OnPlayClicked()
        {
            GameSettingsData gameSettingsData = new GameSettingsData
            {
                VerticalSize = (int)_verticalSize.value,
                HorizontalSize = (int)_horizontalSize.value,
                MemorizationTime = (int)_memorizationTime.value,
                GameTime = (int)_gameTime.value,
                SoundVolume = (int)_soundVolume.value,
                MusicVolume = (int)_musicVolume.value
            };
            _progressService.GameSettingsData = gameSettingsData;
            _stateMachine.Enter<GameState>();
        }

        private void OnSaveJSONAndPlay()
        {
            GameSettingsData gameSettingsData = new GameSettingsData
            {
                VerticalSize = (int)_verticalSize.value,
                HorizontalSize = (int)_horizontalSize.value,
                MemorizationTime = (int)_memorizationTime.value,
                GameTime = (int)_gameTime.value,
                SoundVolume = (int)_soundVolume.value,
                MusicVolume = (int)_musicVolume.value
            };
            _progressService.GameSettingsData = gameSettingsData;
            _stateMachine.Enter<GameState>();
        }

        private void OnSavePlayerPrefsAndPlay()
        {
            GameSettingsData gameSettingsData = new GameSettingsData
            {
                VerticalSize = (int)_verticalSize.value,
                HorizontalSize = (int)_horizontalSize.value,
                MemorizationTime = (int)_memorizationTime.value,
                GameTime = (int)_gameTime.value,
                SoundVolume = (int)_soundVolume.value,
                MusicVolume = (int)_musicVolume.value
            };
            _progressService.GameSettingsData = gameSettingsData;
            _saveLoadService.SaveGameSettings(SaveLoadId.PlayerPrefs);
            _stateMachine.Enter<GameState>();
        }

        private void OnSaveBase64AndPlay()
        {
            GameSettingsData gameSettingsData = new GameSettingsData
            {
                VerticalSize = (int)_verticalSize.value,
                HorizontalSize = (int)_horizontalSize.value,
                MemorizationTime = (int)_memorizationTime.value,
                GameTime = (int)_gameTime.value,
                SoundVolume = (int)_soundVolume.value,
                MusicVolume = (int)_musicVolume.value
            };
            _progressService.GameSettingsData = gameSettingsData;
            _stateMachine.Enter<GameState>();
        }

        private void InitializeMinMaxSettings(DefaultGameSettingsData defaultGameSettings)
        {
            _verticalSize.minValue = defaultGameSettings.VerticalSize.Min;
            _verticalSize.maxValue = defaultGameSettings.VerticalSize.Max;

            _horizontalSize.minValue = defaultGameSettings.HorizontalSize.Min;
            _horizontalSize.maxValue = defaultGameSettings.HorizontalSize.Max;

            _memorizationTime.minValue = defaultGameSettings.MemorizationTime.Min;
            _memorizationTime.maxValue = defaultGameSettings.MemorizationTime.Max;

            _gameTime.minValue = defaultGameSettings.GameTime.Min;
            _gameTime.maxValue = defaultGameSettings.GameTime.Max;

            _soundVolume.minValue = defaultGameSettings.SoundVolume.Min;
            _soundVolume.maxValue = defaultGameSettings.SoundVolume.Max;

            _musicVolume.minValue = defaultGameSettings.MusicVolume.Min;
            _musicVolume.maxValue = defaultGameSettings.MusicVolume.Max;
        }

        private void InitializeSavedGameSettings(GameSettingsData gameSettingsData)
        {
            _verticalSize.value = gameSettingsData.VerticalSize;
            _horizontalSize.value = gameSettingsData.HorizontalSize;
            _memorizationTime.value = gameSettingsData.MemorizationTime;
            _gameTime.value = gameSettingsData.GameTime;
            _soundVolume.value = gameSettingsData.SoundVolume;
            _musicVolume.value = gameSettingsData.MusicVolume;
        }

        private void InitializeDefaultGameSettings(DefaultGameSettingsData defaultGameSettings)
        {
            _verticalSize.value = defaultGameSettings.VerticalSize.DefaultValue;
            _horizontalSize.value = defaultGameSettings.HorizontalSize.DefaultValue;
            _memorizationTime.value = defaultGameSettings.MemorizationTime.DefaultValue;
            _gameTime.value = defaultGameSettings.GameTime.DefaultValue;
            _soundVolume.value = defaultGameSettings.SoundVolume.DefaultValue;
            _musicVolume.value = defaultGameSettings.MusicVolume.DefaultValue;
        }

        private void OnVerticalSizeChanged(float value)
        {
            int sliderValue = Mathf.RoundToInt(value);
            if (_horizontalSize.value % 2 != 0) // if horizontalSize not even
            {
                //verticalSize only even values
                if (sliderValue % 2 != 0)
                {
                    int evenValue = (sliderValue % 2 == 0) ? sliderValue : sliderValue + 1;
                    _verticalSize.value = evenValue;
                }
            }
        }

        private void OnHorizontalSizeChanged(float value)
        {
            int sliderValue = Mathf.RoundToInt(value);
            if (_verticalSize.value % 2 != 0) // if verticalSize not even
            {
                //horizontalSize only even values
                if (sliderValue % 2 != 0)
                {
                    int evenValue = (sliderValue % 2 == 0) ? sliderValue : sliderValue + 1;
                    _horizontalSize.value = evenValue;
                }
            }
        }
    }
}