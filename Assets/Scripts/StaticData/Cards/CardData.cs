using System;
using UnityEngine;

namespace StaticData.Cards
{
    [Serializable]
    public class CardData
    {
        [field: SerializeField] public CardId CardId { get; private set; }
        [field: SerializeField] public Sprite CardSprite { get; private set; }
    }
}