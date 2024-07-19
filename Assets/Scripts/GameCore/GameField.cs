using System.Collections.Generic;
using Data;
using UI.Views.GameView;
using UnityEngine;
using UnityEngine.UI;

namespace GameCore
{
    public class GameField : MonoBehaviour
    {
        [SerializeField] private CountdownTimer _countdownTimer;
        public GridLayoutGroup gridLayoutGroup;
        
        private List<Card> _cards;
        private GameSettingsData _gameSettingsData;
        private GameView _gameView;

        public void Initialize(GameSettingsData gameSettingsData, List<Card> cards, GameView gameView)
        {
            _gameSettingsData = gameSettingsData;
            _cards = cards;
            _gameView = gameView;
        }

        public void PrepareGame()
        {
            ShuffleCards();
            StartMemorizationCardsFor(_gameSettingsData.MemorizationTime);
        }
        
        private void StartGame()
        {
            _gameView.UpdateTimerTime(_gameSettingsData.GameTime * Consts.MinutesToSeconds);
            _gameView.SetMemorizationTimerActive(false);
            _gameView.SetHelpButtonInteractable(true);
            _countdownTimer.StartTimer(_gameSettingsData.GameTime * Consts.MinutesToSeconds, _gameView.UpdateTimerTime);
            _countdownTimer.OnTimerFinish += EndGame;
        }

        private void EndGame()
        {
            _countdownTimer.OnTimerFinish -= EndGame;
        }

        private void StartMemorizationCardsFor(int time)
        {
            _gameView.SetHelpButtonInteractable(false);
            _gameView.UpdateMemorizationTime(time);
            _gameView.SetMemorizationTimerActive(true);
            _countdownTimer.StartTimer(time, _gameView.UpdateMemorizationTime);
            _countdownTimer.OnTimerFinish += StartGame;
            foreach (var card in _cards) 
                card.MemorizationCardFor(time);
        }

        private void ShuffleCards()
        {
            for (int i = 0; i < _cards.Count; i++)
            {
                Card temp = _cards[i];
                int randomIndex = Random.Range(i, _cards.Count);
                _cards[i] = _cards[randomIndex];
                _cards[randomIndex] = temp;
            }

            foreach (Card card in _cards)
                card.transform.SetSiblingIndex(_cards.IndexOf(card));
        }
    }
}