using System.Collections;
using Audio;
using Data;
using Infractructure.Services.Progress;
using Infractructure.Services.SaveLoad;
using Infractructure.Services.StaticData;
using Infractructure.StateMachine;
using StaticData.GameSettings;
using TMPro;
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
        [SerializeField] private Button _btnClearSaves;
        [SerializeField] private Button _btnLoadPlayerPrefsSave;
        [SerializeField] private Button _btnLoadJSONSave;
        [SerializeField] private Button _btnLoadBase64Save;
        [SerializeField] private Button _btnPlay;
        [SerializeField] private Button _btnSavePlayerPrefs;
        [SerializeField] private Button _btnSaveJSON;
        [SerializeField] private Button _btnSaveBase64;
        [SerializeField] private Image _noSaveNotification;
        [SerializeField] private TextMeshProUGUI _noSaveText;
        [SerializeField] private TextMeshProUGUI _noSaveTypeText;
        private Coroutine _notificationCoroutine;

        private IGameStateMachine _stateMachine;
        private IStaticDataService _staticDataService;
        private ISaveLoadService _saveLoadService;
        private IProgressService _progressService;
        private AudioManager _audioManager;

        public void Construct(IGameStateMachine stateMachine, IStaticDataService staticDataService,
            ISaveLoadService saveLoadService, IProgressService progressService, AudioManager audioManager)
        {
            _stateMachine = stateMachine;
            _staticDataService = staticDataService;
            _saveLoadService = saveLoadService;
            _progressService = progressService;
            _audioManager = audioManager;
        }

        public void Initialize()
        {
            DefaultGameSettingsData defaultGameSettings = _staticDataService.GetDefaultGameSettings();
            InitializeSliders(defaultGameSettings);
            AddButtonListeners();
            AddSliderListeners();
        }

        private void InitializeSliders(DefaultGameSettingsData defaultGameSettings)
        {
            SetSliderLimits(_verticalSize, defaultGameSettings.VerticalSize);
            SetSliderLimits(_horizontalSize, defaultGameSettings.HorizontalSize);
            SetSliderLimits(_memorizationTime, defaultGameSettings.MemorizationTime);
            SetSliderLimits(_gameTime, defaultGameSettings.GameTime);
            SetSliderLimits(_soundVolume, defaultGameSettings.SoundVolume);
            SetSliderLimits(_musicVolume, defaultGameSettings.MusicVolume);
            ApplyDefaultSettings(defaultGameSettings);
        }

        private void AddButtonListeners()
        {
            _btnClearSaves.onClick.AddListener(OnClearSaves);
            _btnLoadPlayerPrefsSave.onClick.AddListener(() => LoadGameSettings(SaveLoadId.PlayerPrefs));
            _btnLoadJSONSave.onClick.AddListener(() => LoadGameSettings(SaveLoadId.JSON));
            _btnLoadBase64Save.onClick.AddListener(() => LoadGameSettings(SaveLoadId.Base64));
            _btnPlay.onClick.AddListener(OnPlayClicked);
            _btnSavePlayerPrefs.onClick.AddListener(() => SaveGameSettings(SaveLoadId.PlayerPrefs));
            _btnSaveJSON.onClick.AddListener(() => SaveGameSettings(SaveLoadId.JSON));
            _btnSaveBase64.onClick.AddListener(() => SaveGameSettings(SaveLoadId.Base64));
        }

        private void AddSliderListeners()
        {
            _verticalSize.onValueChanged.AddListener(OnVerticalSizeChanged);
            _horizontalSize.onValueChanged.AddListener(OnHorizontalSizeChanged);
            _musicVolume.onValueChanged.AddListener(OnMusicVolumeChanged);
            _soundVolume.onValueChanged.AddListener(OnSoundVolumeChanged);
        }

        private void OnMusicVolumeChanged(float value)
        {
            value /= Consts.SettingVolumeToASVolume;
            _audioManager.UpdateVolumeMusic(value);
        }

        private void OnSoundVolumeChanged(float value)
        {
            value /= Consts.SettingVolumeToASVolume;
            _audioManager.UpdateVolumeSound(value);
        }

        private void OnClearSaves() =>
            _saveLoadService.ClearAllSaves();

        private void LoadGameSettings(SaveLoadId saveLoadId)
        {
            GameSettingsData gameSettings = _saveLoadService.LoadGameSettings(saveLoadId);
            if (gameSettings != null)
                ApplyGameSettings(gameSettings);
            else
                ShowNoSaveNotification(saveLoadId);
        }
        
        private void OnPlayClicked()
        {
            SaveGameSettingsToProgressService();
            _stateMachine.Enter<GameState>();
        }

        private void SaveGameSettings(SaveLoadId saveLoadId)
        {
            SaveGameSettingsToProgressService();
            _saveLoadService.SaveGameSettings(saveLoadId);
        }

        private void SaveGameSettingsToProgressService()
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
        }

        private void ApplyDefaultSettings(DefaultGameSettingsData defaultGameSettings)
        {
            _verticalSize.value = defaultGameSettings.VerticalSize.DefaultValue;
            _horizontalSize.value = defaultGameSettings.HorizontalSize.DefaultValue;
            _memorizationTime.value = defaultGameSettings.MemorizationTime.DefaultValue;
            _gameTime.value = defaultGameSettings.GameTime.DefaultValue;
            _soundVolume.value = defaultGameSettings.SoundVolume.DefaultValue;
            _musicVolume.value = defaultGameSettings.MusicVolume.DefaultValue;
        }

        private void ShowNoSaveNotification(SaveLoadId saveLoadId)
        {
            if (_notificationCoroutine == null)
                _notificationCoroutine = StartCoroutine(NoSaveNotificationCoroutine(saveLoadId));
        }

        private IEnumerator NoSaveNotificationCoroutine(SaveLoadId saveLoadId)
        {
            float fadeInDuration = 0.25f;
            float fadeOutDuration = 0.25f;
            float waitDuration = 0.5f;

            Color notificationBackgroundStartColor = new Color(0.1415094f, 0.1415094f, 0.1415094f, 0f);
            Color notificationBackgroundEndColor = new Color(0.1415094f, 0.1415094f, 0.1415094f, 1f);
            Color textStartColor = new Color(1f, 1f, 1f, 0f);
            Color textEndColor = new Color(1f, 1f, 1f, 1f);

            _noSaveTypeText.text = saveLoadId.ToString();
            _noSaveNotification.gameObject.SetActive(true);

            yield return FadeCoroutine(notificationBackgroundStartColor, notificationBackgroundEndColor,
                textStartColor, textEndColor, fadeInDuration);

            yield return new WaitForSeconds(waitDuration);

            yield return FadeCoroutine(notificationBackgroundEndColor, notificationBackgroundStartColor,
                textEndColor, textStartColor, fadeOutDuration);

            _noSaveNotification.gameObject.SetActive(false);
            _notificationCoroutine = null;
        }

        private IEnumerator FadeCoroutine(Color notificationStartColor, Color notificationEndColor,
            Color textStartColor, Color textEndColor, float duration)
        {
            float elapsedTime = 0f;

            _noSaveNotification.color = notificationStartColor;
            _noSaveText.color = textStartColor;
            _noSaveTypeText.color = textStartColor;

            while (elapsedTime < duration)
            {
                float t = elapsedTime / duration;
                _noSaveNotification.color = Color.Lerp(notificationStartColor, notificationEndColor, t);
                _noSaveText.color = Color.Lerp(textStartColor, textEndColor, t);
                _noSaveTypeText.color = Color.Lerp(textStartColor, textEndColor, t);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            _noSaveNotification.color = notificationEndColor;
            _noSaveText.color = textEndColor;
            _noSaveTypeText.color = textEndColor;
        }

        private void ApplyGameSettings(GameSettingsData gameSettings)
        {
            _verticalSize.value = gameSettings.VerticalSize;
            _horizontalSize.value = gameSettings.HorizontalSize;
            _memorizationTime.value = gameSettings.MemorizationTime;
            _gameTime.value = gameSettings.GameTime;
            _soundVolume.value = gameSettings.SoundVolume;
            _musicVolume.value = gameSettings.MusicVolume;
        }

        private void SetSliderLimits(Slider slider, SliderSettingData limits)
        {
            slider.minValue = limits.Min;
            slider.maxValue = limits.Max;
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