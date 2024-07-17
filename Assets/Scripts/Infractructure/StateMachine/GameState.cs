using Infractructure.Services.Progress;
using Infractructure.UIServices.ViewService;
using StaticData.View;

namespace Infractructure.StateMachine
{
    public class GameState : IDefaultState
    {
        private readonly IViewService _viewService;
        private readonly IProgressService _progressService;

        public GameState(IViewService viewService, IProgressService progressService)
        {
            _viewService = viewService;
            _progressService = progressService;
        }

        public void Enter()
        {
            _viewService.Open(ViewId.Game);
            //_progressService.GameSettingsData
        }

        public void Exit()
        {
        }
    }
}