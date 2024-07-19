using System;
using Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views.GameView
{
    public class GameView : ViewBase
    {
        [SerializeField] private GameObject _memorizationTimer;
        [SerializeField] private GameObject _timer;
        [SerializeField] private TextMeshProUGUI _memorizationTimeText;
        [SerializeField] private TextMeshProUGUI _timerTimeText;
        [SerializeField] private Button _btnHelp;
        public Action OnHelpClicked;
        
        public override void Initialize()
        {
            base.Initialize();
            _btnHelp.onClick.AddListener(() => OnHelpClicked?.Invoke());
        }

        public void SetHelpButtonInteractable(bool active)
        {
            _btnHelp.interactable = active;
        }

        public void SetMemorizationTimerActive(bool active)
        {
            _memorizationTimer.SetActive(active);
            _timer.SetActive(!active);
        }

        public void UpdateMemorizationTime(int time)
        {
            _memorizationTimeText.text = time.ToString();
        }

        public void UpdateTimerTime(int time)
        {
            int minutes = time / Consts.MinutesToSeconds;
            int seconds = time % Consts.MinutesToSeconds;
            _timerTimeText.text = $"{minutes:00}:{seconds:00}";
        }
    }
}