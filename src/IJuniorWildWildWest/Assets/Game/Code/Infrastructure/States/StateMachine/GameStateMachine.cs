using System;
using System.Collections.Generic;
using Game.Infrastructure.States.StateInfrastructure;

namespace Game.Infrastructure.States.StateMachine
{
    public class GameStateMachine : IGameStateMachine
    {
        private Dictionary<Type, IExitableState> _states = new();

        private IExitableState _currentState;
        
        public void RegisterStates(Dictionary<Type, IExitableState> states)
        {
            foreach (KeyValuePair<Type, IExitableState> state in states)
            {
                _states.Add(state.Key, state.Value);
            }
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