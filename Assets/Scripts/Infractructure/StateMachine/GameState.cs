using System.Collections.Generic;
using Audio;
using Data;
using GameCore;
using Infractructure.Services.Factory;
using Infractructure.Services.Progress;
using Infractructure.UIServices.Factory;
using Infractructure.UIServices.ViewService;
using StaticData.View;
using UI.Views;
using UI.Views.GameView;

namespace Infractructure.StateMachine
{
    public class GameState : IDefaultState
    {
        private readonly IViewService _viewService;
        private readonly IProgressService _progressService;
        private readonly IUIFactory _uiFactory;
        private readonly IGameFactory _gameFactory;
        private IGameStateMachine _stateMachine;

        public GameState(IViewService viewService, IProgressService progressService,IUIFactory uiFactory, IGameFactory gameFactory)
        {
            _viewService = viewService;
            _progressService = progressService;
            _uiFactory = uiFactory;
            _gameFactory = gameFactory;
        }

        public void Initialize(IGameStateMachine stateMachine) =>
            _stateMachine = stateMachine;

        public async void Enter()
        {
            AudioManager audioManager = await _gameFactory.CreateAudioManager();
            audioManager.UpdateVolumeMusic(_progressService.GameSettingsData.MusicVolume/Consts.SettingVolumeToASVolume);
            audioManager.UpdateVolumeSound(_progressService.GameSettingsData.SoundVolume/Consts.SettingVolumeToASVolume);
            
            await _uiFactory.CreateUIRoot();
            ViewBase view = await _viewService.Open(ViewId.Game);
            GameView gameView = view.GetComponent<GameView>();
            GameField gameField = await _gameFactory.CreateGameField(_progressService.GameSettingsData.VerticalSize,
                _progressService.GameSettingsData.HorizontalSize);
            List<Card> cards = await _gameFactory.CreateCards(_progressService.GameSettingsData.VerticalSize *
                                                              _progressService.GameSettingsData.HorizontalSize);

            gameField.Initialize(_progressService, _stateMachine, cards, audioManager, gameView);
            gameField.PrepareGame();
        }

        public void Exit()
        {
        }
    }
}