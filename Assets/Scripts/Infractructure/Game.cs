using Infractructure.StateMachine;

namespace Infractructure
{
    public class Game
    {
        private readonly GameStateMachine _stateMachine;

        public Game(GameStateMachine stateMachine) => 
            _stateMachine = stateMachine;

        public void Init(ICoroutineRunner coroutineRunner)
        {
            _stateMachine.Init(coroutineRunner);
            _stateMachine.Enter<BootstrapState>();
        }
    }
}