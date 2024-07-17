using System.Collections.Generic;
using System.Threading.Tasks;
using Infractructure.Services.Progress;
using Infractructure.Services.StaticData;
using Infractructure.StateMachine;
using Infractructure.UIServices.Factory;
using StaticData.View;
using UI.Views;
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
        private IGameStateMachine _stateMachine;
        private readonly IProgressService _progressService;

        private readonly Stack<ViewBase> _sequenceOfViews = new();
        private ViewBase _currentView;

        public ViewService(IUIFactory uiFactory, IStaticDataService staticDataService, ISaveLoadService saveLoadService,
            IProgressService progressService)
        {
            _uiFactory = uiFactory;
            _staticDataService = staticDataService;
            _saveLoadService = saveLoadService;
            _progressService = progressService;
        }

        public void Initialize(IGameStateMachine stateMachine) =>
            _stateMachine = stateMachine;

        public async Task<ViewBase> Open(ViewId viewId)
        {
            if (viewId == ViewId.Unknown)
                return null;

            if (_currentView != null)
            {
                _sequenceOfViews.Push(_currentView);
                HideView(_currentView);
            }

            _currentView = await OpenNew(viewId);
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
                    gameSettings.Construct(this, _stateMachine, _staticDataService, _saveLoadService, _progressService);
                    gameSettings.Initialize();
                    gameSettings.SubscribeUpdates();
                    break;
                case ViewId.Game:
                    GameView gameView = view.GetComponent<GameView>();
                    gameView.Construct(this);
                    gameView.Initialize();
                    gameView.SubscribeUpdates();
                    break;
            }

            return view;
        }

        public void CloseCurrent()
        {
            if (_currentView == null)
                return;

            Object.Destroy(_currentView.gameObject);

            if (_sequenceOfViews.Count != 0)
                ShowView(_sequenceOfViews.Pop());
        }

        private void ShowView(ViewBase view)
        {
            view.gameObject.SetActive(true);
            _currentView = view;
        }

        private void HideView(ViewBase view) =>
            view.gameObject.SetActive(false);
    }
}