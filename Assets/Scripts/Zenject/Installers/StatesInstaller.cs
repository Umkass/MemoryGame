using Infractructure.StateMachine;

namespace Zenject.Installers
{
    public class StatesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<BootstrapState>().AsSingle();
            Container.Bind<MenuState>().AsSingle();
            Container.Bind<GameState>().AsSingle();
            Container.Bind<GameStateMachine>().AsSingle();
        }
    }
}