using System.Threading.Tasks;
using GameCore;
using Infractructure.Services.Factory;
using Infractructure.Services.Progress;
using Infractructure.UIServices.Factory;
using Infractructure.UIServices.ViewService;
using StaticData.View;

namespace Infractructure.StateMachine
{
    public class GameState : IDefaultState
    {
        private readonly IViewService _viewService;
        private readonly IProgressService _progressService;
        private readonly IUIFactory _uiFactory;
        private readonly IGameFactory _gameFactory;

        public GameState(IViewService viewService, IProgressService progressService, IGameFactory gameFactory)
        {
            _viewService = viewService;
            _progressService = progressService;
            _gameFactory = gameFactory;
        }

        public async void Enter()
        {
            await _viewService.Open(ViewId.Game);
            await _gameFactory.CreateGameField(_progressService.GameSettingsData.VerticalSize,
                _progressService.GameSettingsData.HorizontalSize);
            await _gameFactory.CreateCards(_progressService.GameSettingsData.VerticalSize *
                                     _progressService.GameSettingsData.HorizontalSize);
        }

        public void Exit()
        {
        }
    }
}