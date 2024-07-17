using Data;
using Infractructure.AssetManagement;
using Infractructure.Services.StaticData;

namespace Infractructure.StateMachine
{
    public class BootstrapState : IDefaultState
    {
        private readonly ISceneLoader _sceneLoader;
        private IGameStateMachine _stateMachine;
        private readonly IStaticDataService _staticDataService;
        private readonly IAssetProvider _assetProvider;

        public BootstrapState(ISceneLoader sceneLoader, IStaticDataService staticDataService, IAssetProvider assetProvider)
        {
            _sceneLoader = sceneLoader;
            _staticDataService = staticDataService;
            _assetProvider = assetProvider;
            InitializeServices();
        }

        public void Initialize(IGameStateMachine stateMachine) =>
            _stateMachine = stateMachine;

        public void Enter() =>
            _sceneLoader.LoadScene(SceneNames.Initial, onLoaded: EnterMenuState);

        public void Exit()
        {
        }

        private void InitializeServices()
        {
            _staticDataService.LoadAll();
            _assetProvider.Initialize();
        }

        private void EnterMenuState() =>
            _stateMachine.Enter<MenuState>();
    }
}