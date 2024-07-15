namespace Infractructure.StateMachine
{
    public interface IGameStateMachine
    {
        void Enter<TState>() where TState : class, IDefaultState;
        void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>;
    }
}