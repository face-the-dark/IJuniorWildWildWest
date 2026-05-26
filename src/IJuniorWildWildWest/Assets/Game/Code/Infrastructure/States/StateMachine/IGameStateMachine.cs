using Game.Infrastructure.States.StateInfrastructure;

namespace Game.Infrastructure.States.StateMachine
{
    public interface IGameStateMachine
    {
        public void Enter<TState>() where TState : class, IState;
        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayLoadedState<TPayload>;
    }
}