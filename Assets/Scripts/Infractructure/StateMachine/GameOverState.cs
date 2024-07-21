using Infractructure.Services.Factory;
using Infractructure.UIServices.Factory;
using Infractructure.UIServices.ViewService;
using StaticData.View;

namespace Infractructure.StateMachine
{
    public class GameOverState : IDefaultState
    {
        private readonly IViewService _viewService;
        private readonly IGameFactory _gameFactory;
        private readonly IUIFactory _uiFactory;

        public GameOverState(IViewService viewService, IGameFactory gameFactory, IUIFactory uiFactory)
        {
            _viewService = viewService;
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
        }

        public async void Enter()
        {
            await _viewService.Open(ViewId.GameOver);
        }
        

        public void Exit()
        {
            _gameFactory.Cleanup();
            _uiFactory.Cleanup();
        }
    }
}