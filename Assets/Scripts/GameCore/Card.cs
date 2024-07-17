using StaticData.Cards;
using UnityEngine;
using UnityEngine.UI;

namespace GameCore
{
    public class Card : MonoBehaviour
    {
        [SerializeField] private Image _image;
        private CardId _cardId;

        public void Initialize(CardId cardId, Sprite sprite)
        {
            _cardId = cardId;
            _image.sprite = sprite;
        }
    }
}