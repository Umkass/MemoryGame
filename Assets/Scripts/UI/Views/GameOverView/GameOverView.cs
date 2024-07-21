using Data;
using Infractructure.Services.Progress;
using Infractructure.StateMachine;
using Infractructure.UIServices.ViewService;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views.GameOverView
{
    public class GameOverView : ViewBase
    {
        [SerializeField] private TextMeshProUGUI _gameResultText;
        [SerializeField] private Button _btnRestart;
        [SerializeField] private Button _btnMainMenu;
        private IProgressService _progressService;
        private IGameStateMachine _stateMachine;

        public void Construct(IViewService viewService,IProgressService progressService, IGameStateMachine stateMachine)
        {
            base.Construct(viewService);
            _progressService = progressService;
            _stateMachine = stateMachine;
        }
        
        public override void Initialize()
        {
            base.Initialize();
            _gameResultText.text = _progressService.IsLastGameWon ? Consts.GameWonText : Consts.GameLostText;
            _btnRestart.onClick.AddListener(OnRestart);
            _btnMainMenu.onClick.AddListener(OnMainMenu);
        }

        private void OnRestart() => 
            _stateMachine.Enter<GameState>();

        private void OnMainMenu() => 
            _stateMachine.Enter<MenuState>();
    }
}