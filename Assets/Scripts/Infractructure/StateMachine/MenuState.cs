using Data;
using Infractructure.UIServices.Factory;
using Infractructure.UIServices.ViewService;
using StaticData.View;
using UI;

namespace Infractructure.StateMachine
{
    public class MenuState : IDefaultState
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly ICurtain _loadingCurtain;
        private readonly IUIFactory _uiFactory;
        private readonly IViewService _viewService;
        private IGameStateMachine _stateMachine;

        public MenuState(ISceneLoader sceneLoader, ICurtain loadingCurtain, IUIFactory uiFactory, IViewService viewService)
        {
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _uiFactory = uiFactory;
            _viewService = viewService;
        }

        public void Initialize(IGameStateMachine stateMachine) =>
            _stateMachine = stateMachine;

        public void Enter()
        {
            _loadingCurtain.Show();
            _sceneLoader.LoadScene(SceneNames.Game, OnLoaded);
        }

        private async void OnLoaded()
        {
            await _uiFactory.CreateUIRoot();
            _loadingCurtain.Hide();
            _viewService.Initialize(_stateMachine);
            await _viewService.Open(ViewId.MainMenu);
        }

        public void Exit()
        {
            _uiFactory.Cleanup();
        }
    }
}