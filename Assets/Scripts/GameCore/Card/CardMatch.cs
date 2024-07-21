using System;
using Data;
using UnityEngine;

namespace GameCore
{
    public class CardMatch : MonoBehaviour
    {
        private Card _firstRevealed;
        private Card _secondRevealed;
        private GameSettingsData _settingsData;
        private int _matchesScore;
        private int _totalPairs;

        public Action OnAllCardsMatched;

        public void Initialize(GameSettingsData settingsData)
        {
            _settingsData = settingsData;
            _totalPairs = (_settingsData.HorizontalSize * _settingsData.VerticalSize) / 2;
        }

        public void CardRevealed(Card card)
        {
            if (_firstRevealed == null)
            {
                _firstRevealed = card;
            }
            else if (_secondRevealed == null)
            {
                _secondRevealed = card;
                CheckMatch();
            }
        }

        public void CleanRevealed()
        {
            _firstRevealed = null;
            _secondRevealed = null;
        }
        
        private void CheckMatch()
        {
            if (_firstRevealed.Id == _secondRevealed.Id)
                HandleMatch();
            else
                HandleNoMatch();
        }

        private void HandleMatch()
        {
            _firstRevealed.IsMatched = true;
            _secondRevealed.IsMatched = true;
            CleanRevealed();
            _matchesScore++;

            if (_matchesScore >= _totalPairs) 
                OnAllCardsMatched?.Invoke();
        }

        private void HandleNoMatch()
        {
            _firstRevealed.TurnCardOver();
            _secondRevealed.TurnCardOver();
            CleanRevealed();
        }
    }
}