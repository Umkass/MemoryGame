using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace StaticData.Cards
{
    [CreateAssetMenu(fileName = "CardsData", menuName = "StaticData/CardsStaticData")]
    public class CardsStaticData : ScriptableObject
    {
        [field: SerializeField] public List<CardData> CardsData { get; private set; }
        [field: SerializeField] public AssetReferenceGameObject PrefabReference { get; private set; }
    }
}