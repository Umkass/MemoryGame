using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace StaticData.View
{
    [Serializable]
    public class ViewData
    {
        [field: SerializeField] public ViewId ViewId { get; private set; }
        [field: SerializeField] public AssetReferenceGameObject PrefabReference { get; private set; }
    }
}