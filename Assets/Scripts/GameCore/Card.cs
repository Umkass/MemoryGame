using System.Collections;
using Data;
using StaticData.Cards;
using UnityEngine;
using UnityEngine.UI;

namespace GameCore
{
    public class Card : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private GameObject _cardBack;
        [SerializeField] private Button btnCard;
        [SerializeField] private Animation _animation;
        private bool _isFaceUp;
        private CardId _cardId;

        private void Awake()
        {
            btnCard.onClick.AddListener(TurnCardOver);
        }

        public void Initialize(CardId cardId, Sprite sprite)
        {
            _cardId = cardId;
            _image.sprite = sprite;
        }

        public void MemorizationCardFor(float time)
        {
            StartCoroutine(MemorizationCardCoroutine(time));
        }

        private IEnumerator MemorizationCardCoroutine(float time)
        {
            TurnCardOver();
            yield return new WaitForSeconds(time);
            TurnCardOver();
        }

        public void CardOnEdge()
        {
            _isFaceUp = !_isFaceUp;
            _cardBack.SetActive(!_isFaceUp);
            _image.gameObject.SetActive(_isFaceUp);
        }

        public void MakeInteractable()
        {
            btnCard.interactable = true;
        }
        
        public void MakeNotInteractable()
        {
            btnCard.interactable = false;
        }
        
        private void TurnCardOver()
        {
            _animation.Play(_isFaceUp 
                ? AnimationNames.CardFaceDown 
                : AnimationNames.CardFaceUp);
        }
    }
}