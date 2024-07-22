using Audio;
using Data;
using Infractructure.Services.Factory;
using Infractructure.UIServices.Factory;
using Infractructure.UIServices.ViewService;
using StaticData.View;
using UI.Curtain;

namespace Infractructure.StateMachine
{
    public class MenuState : IDefaultState
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly ICurtain _loadingCurtain;
        private readonly IUIFactory _uiFactory;
        private readonly IViewService _viewService;
        private readonly IGameFactory _gameFactory;
        private IGameStateMachine _stateMachine;

        public MenuState(ISceneLoader sceneLoader, ICurtain loadingCurtain, IUIFactory uiFactory,
            IViewService viewService, IGameFactory gameFactory)
        {
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _uiFactory = uiFactory;
            _viewService = viewService;
            _gameFactory = gameFactory;
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
            await _uiFactory.WarmUp();
            await _uiFactory.CreateUIRoot();

            await _gameFactory.WarmUp();
            AudioManager audioManager = await _gameFactory.CreateAudioManager();

            _loadingCurtain.Hide();

            _viewService.Initialize(_stateMachine, audioManager);
            await _viewService.Open(ViewId.MainMenu);
        }

        public void Exit()
        {
            _gameFactory.Cleanup();
            _uiFactory.Cleanup();
        }
    }
}