namespace Infractructure.StateMachine
{
    public interface IGameStateMachine
    {
        void Enter<TState>() where TState : class, IDefaultState;
    }
}