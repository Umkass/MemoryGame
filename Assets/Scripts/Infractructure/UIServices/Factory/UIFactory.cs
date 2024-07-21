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
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticDataService;
        public GameObject UIRoot { get; private set; }

        public UIFactory(IStaticDataService staticDataService, IAssetProvider assetProvider)
        {
            _staticDataService = staticDataService;
            _assetProvider = assetProvider;
        }

        public async Task CreateUIRoot()
        {
            GameObject root = await _assetProvider.Instantiate(AssetAddress.UIRoot);
            UIRoot = root;
        }

        public async Task<ViewBase> CreateWindow(ViewId viewId)
        {
            ViewData config = _staticDataService.GetView(viewId);
            GameObject windowGameObject = await _assetProvider.Instantiate(config.PrefabReference.AssetGUID, UIRoot.transform);
            ViewBase window = windowGameObject.GetComponent<ViewBase>();
            return window;
        }
        
        public void Cleanup()
        {
            Object.Destroy(UIRoot);
            _assetProvider.Cleanup();
        }
    }
}