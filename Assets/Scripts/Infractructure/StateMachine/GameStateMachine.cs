using System;
using System.Collections.Generic;

namespace Infractructure.StateMachine
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IState> _states;
        private readonly BootstrapState _bootstrapState;
        private readonly MenuState _menuState;
        private readonly GameState _gameState;
        private readonly ISceneLoader _sceneLoader;
        private IState _activeState;
        
        public GameStateMachine(BootstrapState bootstrapState, MenuState menuState, GameState gameState,
            GameOverState gameOverState, ISceneLoader sceneLoader)
        {
            _bootstrapState = bootstrapState;
            _menuState = menuState;
            _gameState = gameState;
            _sceneLoader = sceneLoader;
            _states = new Dictionary<Type, IState>
            {
                [typeof(BootstrapState)] = bootstrapState,
                [typeof(MenuState)] = menuState,
                [typeof(GameState)] = gameState,
                [typeof(GameOverState)] = gameOverState,
            };
        }

        public void Init(ICoroutineRunner coroutineRunner)
        {
            _sceneLoader.Init(coroutineRunner);
            _bootstrapState.Initialize(this);
            _menuState.Initialize(this);
            _gameState.Initialize(this);
        }

        public void Enter<TState>() where TState : class, IDefaultState
        {
            IDefaultState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            TState state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IState
        {
            _activeState?.Exit();

            TState state = GetState<TState>();
            _activeState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IState =>
            _states[typeof(TState)] as TState;
    }
}