using Infractructure.AssetManagement;
using Infractructure.Services.Factory;
using Infractructure.Services.Progress;
using Infractructure.Services.StaticData;
using Infractructure.UIServices.Factory;
using Infractructure.UIServices.ViewService;

namespace Zenject.Installers
{
    public class ServicesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IStaticDataService>().To<StaticDataService>().AsSingle();
            Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
            Container.Bind<IProgressService>().To<ProgressService>().AsSingle();
            Container.Bind<IUIFactory>().To<UIFactory>().AsSingle();
            Container.Bind<ISaveLoadService>().To<SaveLoadService>().AsSingle();
            Container.Bind<IViewService>().To<ViewService>().AsSingle();
            Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();
        }
    }
}