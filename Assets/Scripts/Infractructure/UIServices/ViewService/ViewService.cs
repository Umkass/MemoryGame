using System.Threading.Tasks;
using Audio;
using Infractructure.Services.Progress;
using Infractructure.Services.SaveLoad;
using Infractructure.Services.StaticData;
using Infractructure.StateMachine;
using Infractructure.UIServices.Factory;
using StaticData.View;
using UI.Views;
using UI.Views.GameOverView;
using UI.Views.GameSettings;
using UI.Views.GameView;
using UI.Views.MainMenu;
using UnityEngine;

namespace Infractructure.UIServices.ViewService
{
    public class ViewService : IViewService
    {
        private readonly IUIFactory _uiFactory;
        private readonly IStaticDataService _staticDataService;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IProgressService _progressService;
        private IGameStateMachine _stateMachine;

        private ViewBase _currentView;
        private AudioManager _audioManager;

        public ViewService(IUIFactory uiFactory, IStaticDataService staticDataService, ISaveLoadService saveLoadService,
            IProgressService progressService)
        {
            _uiFactory = uiFactory;
            _staticDataService = staticDataService;
            _saveLoadService = saveLoadService;
            _progressService = progressService;
        }

        public void Initialize(IGameStateMachine stateMachine, AudioManager audioManager)
        {
            _stateMachine = stateMachine;
            _audioManager = audioManager;
        }

        public async Task<ViewBase> Open(ViewId viewId)
        {
            if (viewId == ViewId.Unknown)
                return null;

            ViewBase previousView = _currentView;
            _currentView = await OpenNew(viewId);

            if (previousView != null)
                CloseView(previousView);

            return _currentView;
        }

        private async Task<ViewBase> OpenNew(ViewId viewId)
        {
            ViewBase view = await _uiFactory.CreateWindow(viewId);
            switch (viewId)
            {
                case ViewId.MainMenu:
                    MainMenuView mainMenuView = view.GetComponent<MainMenuView>();
                    mainMenuView.Construct(this);
                    mainMenuView.Initialize();
                    mainMenuView.SubscribeUpdates();
                    break;
                case ViewId.GameSettings:
                    GameSettings gameSettings = view.GetComponent<GameSettings>();
                    gameSettings.Construct(this, _stateMachine, _staticDataService, _saveLoadService, _progressService,
                        _audioManager);
                    gameSettings.Initialize();
                    gameSettings.SubscribeUpdates();
                    break;
                case ViewId.Game:
                    GameView gameView = view.GetComponent<GameView>();
                    gameView.Construct(this);
                    gameView.Initialize();
                    gameView.SubscribeUpdates();
                    break;
                case ViewId.GameOver:
                    GameOverView gameOverView = view.GetComponent<GameOverView>();
                    gameOverView.Construct(this, _progressService, _stateMachine);
                    gameOverView.Initialize();
                    gameOverView.SubscribeUpdates();
                    break;
            }

            return view;
        }

        private void CloseView(ViewBase view)
        {
            if (view == null)
                return;

            Object.Destroy(view.gameObject);
        }
    }
}