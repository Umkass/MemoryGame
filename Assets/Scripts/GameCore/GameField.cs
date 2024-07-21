﻿using System.Collections.Generic;
using Data;
using UI.Views.GameView;
using UnityEngine;
using UnityEngine.UI;

namespace GameCore
{
    public class GameField : MonoBehaviour
    {
        [SerializeField] private CardMatch _cardMatch;
        [SerializeField] private CountdownTimer _countdownTimer;
        public GridLayoutGroup gridLayoutGroup;

        private List<Card> _cards;
        private List<CardAnimation> _cardAnimations = new();
        private GameSettingsData _settingsData;
        private GameView _gameView;

        public void Initialize(GameSettingsData gameSettingsData, List<Card> cards, GameView gameView)
        {
            _settingsData = gameSettingsData;
            _cards = cards;
            _gameView = gameView;

            foreach (Card card in _cards)
            {
                CardAnimation cardAnimation = card.GetComponent<CardAnimation>();
                _cardAnimations.Add(cardAnimation);
            }

            _cardMatch.Initialize(_settingsData);
            _cardMatch.OnAllCardsMatched += GameWin;
        }

        public void PrepareGame()
        {
            ShuffleCards();
            StartMemorizationCardsFor(_settingsData.MemorizationTime);
            _countdownTimer.OnTimerFinish += StartGame;
        }

        private void StartGame()
        {
            _countdownTimer.OnTimerFinish -= StartGame;
            _gameView.UpdateTimerTime(_settingsData.GameTime * Consts.MinutesToSeconds);
            SetupGameUI();

            SubscribeToCardEvents();

            _countdownTimer.StartTimer(_settingsData.GameTime * Consts.MinutesToSeconds, _gameView.UpdateTimerTime);
            _countdownTimer.OnTimerFinish += GameLost;
        }

        private void ContinueGame()
        {
            _countdownTimer.OnTimerFinish -= ContinueGame;
            _gameView.UpdateTimerTime(_gameView.TimerTime - _settingsData.MemorizationTime);
            SetupGameUI();

            SubscribeToCardEvents();

            _countdownTimer.StartTimer(_gameView.TimerTime, _gameView.UpdateTimerTime);
            _countdownTimer.OnTimerFinish += GameLost;
        }

        public void GameWin()
        {
            Debug.Log("GameWin ");
            CleanupAfterGame();
        }

        private void GameLost()
        {
            Debug.Log("GameLost ");
            CleanupAfterGame();
        }

        private void StartMemorizationCardsFor(int time)
        {
            _gameView.SetHelpButtonInteractable(false);
            _gameView.UpdateMemorizationTime(time);
            _gameView.SetMemorizationTimerActive(true);
            _countdownTimer.StartTimer(time, _gameView.UpdateMemorizationTime);
            foreach (Card card in _cards)
            {
                if (!card.IsMatched)
                {
                    card.SetCardRevealed(false);
                    card.MemorizationCardFor(time);
                }
            }
        }

        private void OnHelpClicked()
        {
            _gameView.OnHelpClicked -= OnHelpClicked;
            UnsubscribeCardFromEvents();
            _countdownTimer.OnTimerFinish -= GameLost;
            ShuffleCards();
            _cardMatch.CleanRevealed();
            StartMemorizationCardsFor(_settingsData.MemorizationTime);
            _countdownTimer.OnTimerFinish += ContinueGame;
        }

        private void SetupGameUI()
        {
            _gameView.SetMemorizationTimerActive(false);
            _gameView.OnHelpClicked += OnHelpClicked;
            _gameView.SetHelpButtonInteractable(true);
        }

        private void SubscribeToCardEvents()
        {
            foreach (Card card in _cards)
                card.OnCardRevealed += _cardMatch.CardRevealed;
            foreach (CardAnimation cardAnimation in _cardAnimations)
                cardAnimation.OnAnimationStateChanged += OnAnimationStateChanged;
        }

        private void UnsubscribeCardFromEvents()
        {
            foreach (Card card in _cards)
                card.OnCardRevealed -= _cardMatch.CardRevealed;
            foreach (CardAnimation cardAnimation in _cardAnimations)
                cardAnimation.OnAnimationStateChanged -= OnAnimationStateChanged;
        }

        private void OnAnimationStateChanged(bool isPlaying)
        {
            foreach (Card card in _cards)
                card.SetInteractable(!isPlaying);

            _gameView.SetHelpButtonInteractable(!isPlaying);
        }

        private void CleanupAfterGame()
        {
            _countdownTimer.OnTimerFinish -= GameLost;
            UnsubscribeCardFromEvents();
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