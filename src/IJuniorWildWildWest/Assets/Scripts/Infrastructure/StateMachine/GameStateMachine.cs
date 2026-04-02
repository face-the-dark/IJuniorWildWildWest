using System;
using System.Collections.Generic;
using Infrastructure.Factory;
using Infrastructure.StateMachine.States;

namespace Infrastructure.StateMachine
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;

        private IExitableState _currentState;

        public GameStateMachine
        (
            IGameFactory factory,
            SceneLoader sceneLoader
        )
        {
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this),
                [typeof(LoadLevelState)] = new LoadLevelState(this, factory, sceneLoader),
                [typeof(GameLoopState)] = new GameLoopState()
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayLoadedState<TPayload>
        {
            TState state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _currentState?.Exit();

            TState state = GetState<TState>();
            _currentState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState =>
            _states[typeof(TState)] as TState;
    }
}