using Infractructure.UIServices.ViewService;
using StaticData.View;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views.MainMenu
{
    public class MainMenuItem : MonoBehaviour
    {
        [SerializeField] private Button _btnPlay;
        private IViewService _viewService;

        public void Construct(IViewService viewService) =>
            _viewService = viewService;

        public void Initialize() =>
            _btnPlay.onClick.AddListener(OnPlayClicked);

        private void OnPlayClicked() =>
            _viewService.Open(ViewId.GameSettings);
    }
}