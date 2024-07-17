using System.Collections.Generic;
using UnityEngine;

namespace StaticData.View
{
    [CreateAssetMenu(fileName = "ViewsData", menuName = "StaticData/ViewsStaticData")]
    public class ViewsStaticData : ScriptableObject
    {
        [field: SerializeField] public List<ViewData> ViewsData { get; private set; }
    }
}