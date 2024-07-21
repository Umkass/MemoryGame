using System;
using System.Collections;
using StaticData.Cards;
using UnityEngine;
using UnityEngine.UI;

namespace GameCore
{
    public class Card : MonoBehaviour
    {
        [SerializeField] private CardAnimation _cardAnimation;
        [SerializeField] private Image _image;
        [SerializeField] private GameObject _cardBack;
        [SerializeField] private Button btnCard;
        private bool _isMemorization;
        
        public bool IsRevealed { get; private set; }
        public bool IsMatched { get; set; }
        public CardId Id { get; private set; }

        public event Action<Card> OnCardRevealed;

        private void Awake()
        {
            _cardAnimation = GetComponent<CardAnimation>();
            btnCard.onClick.AddListener(CardClicked);
        }

        public void Initialize(CardId cardId, Sprite sprite)
        {
            Id = cardId;
            _image.sprite = sprite;
        }

        private void CardClicked()
        {
            TurnCardOver();
        }

        public void MemorizationCardFor(float time) =>
            StartCoroutine(MemorizationCardCoroutine(time));

        private IEnumerator MemorizationCardCoroutine(float time)
        {
            _isMemorization = true;
            SetInteractable(false);
            TurnCardOver();
            yield return new WaitForSeconds(time);
            TurnCardOver();
            _isMemorization = false;
            SetInteractable(true);
        }

        public void SetCardRevealed(bool isRevealed)
        {
            IsRevealed = isRevealed;
            UpdateUI();
        }

        public void SetInteractable(bool interactable) => 
            btnCard.interactable = !IsRevealed && !IsMatched && !_isMemorization && interactable;

        public void TurnCardOver() =>
            _cardAnimation.PlayCardTurnOver(IsRevealed);

        public void CardOnEdge()
        {
            IsRevealed = !IsRevealed;
            UpdateUI();
        }

        private void UpdateUI()
        {
            _cardBack.SetActive(!IsRevealed);
            _image.gameObject.SetActive(IsRevealed);
        }

        public void CardRevealed() =>
            OnCardRevealed?.Invoke(this);
    }
}