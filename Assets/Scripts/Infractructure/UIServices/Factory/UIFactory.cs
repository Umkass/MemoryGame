using System.Threading.Tasks;
using Infractructure.AssetManagement;
using Infractructure.Services.StaticData;
using StaticData.View;
using UI.Views;
using UnityEngine;

namespace Infractructure.UIServices.Factory
{
    public class UIFactory : IUIFactory
    {
        private Transform _uiRoot;
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticDataService;

        public UIFactory(IStaticDataService staticDataService, IAssetProvider assetProvider)
        {
            _staticDataService = staticDataService;
            _assetProvider = assetProvider;
        }

        public async Task CreateUIRoot()
        {
            GameObject root = await _assetProvider.Instantiate(AssetAddress.UIRoot);
            _uiRoot = root.transform;
        }

        public async Task<ViewBase> CreateWindow(ViewId viewId)
        {
            ViewData config = _staticDataService.GetView(viewId);
            GameObject windowGameObject = await _assetProvider.Instantiate(config.PrefabReference.AssetGUID, _uiRoot);
            ViewBase window = windowGameObject.GetComponent<ViewBase>();
            return window;
        }
    }
}