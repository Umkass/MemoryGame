using Infractructure.UIServices.ViewService;
using UnityEngine;

namespace UI.Views.MainMenu
{
    public class MainMenuView : ViewBase
    {
        [SerializeField] private MainMenuItem _menuItem;
        
        public override void Construct(IViewService viewService)
        {
            base.Construct(viewService);
            _menuItem.Construct(viewService);
        }

        public override void Initialize()
        {
            base.Initialize();
            _menuItem.Initialize();
        }
    }
}