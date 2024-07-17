using Infractructure.Services.Progress;
using Infractructure.Services.StaticData;
using Infractructure.StateMachine;
using Infractructure.UIServices.ViewService;
using UnityEngine;

namespace UI.Views.GameSettings
{
    public class GameSettings : ViewBase
    {
        [SerializeField] private SettingsItem _settingsItem;

        public void Construct(IViewService viewService, IGameStateMachine stateMachine,
            IStaticDataService staticDataService, ISaveLoadService saveLoadService, IProgressService progressService)
        {
            base.Construct(viewService);
            _settingsItem.Construct(stateMachine, staticDataService, saveLoadService, progressService);
        }

        public override void Initialize()
        {
            base.Initialize();
            _settingsItem.Initialize();
        }
    }
}