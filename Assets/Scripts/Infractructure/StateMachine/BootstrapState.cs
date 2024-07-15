using Data;

namespace Infractructure.StateMachine
{
    public class BootstrapState : IDefaultState
    {
        private readonly ISceneLoader _sceneLoader;
        private IGameStateMachine _stateMachine;

        public BootstrapState(ISceneLoader sceneLoader) => 
            _sceneLoader = sceneLoader;

        public void Initialize(IGameStateMachine stateMachine) => 
            _stateMachine = stateMachine;

        public void Enter() =>
            _sceneLoader.LoadScene(SceneNames.Initial, onLoaded: EnterMenuState);

        public void Exit()
        {
        }

        private void EnterMenuState() =>
            _stateMachine.Enter<MenuState>();
    }
}