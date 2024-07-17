using Infractructure;
using UI;
using UnityEngine;

namespace Zenject.Installers
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private LoadingCurtain _loadingCurtain;

        public override void InstallBindings()
        {
            Container.Bind<Game>().AsSingle();
            Container.BindInterfacesAndSelfTo<SceneLoader>().AsSingle();
            Container.Bind<ICurtain>().To<LoadingCurtain>().FromComponentInNewPrefab(_loadingCurtain).AsSingle();
        }
    }
}