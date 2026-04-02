using Infrastructure.StateMachine;
using Infrastructure.StateMachine.States;
using VContainer.Unity;

namespace Infrastructure
{
    public class GameBootstrapper : IInitializable
    {
        private readonly IGameStateMachine _stateMachine;

        public GameBootstrapper(IGameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Initialize()
        {
            _stateMachine.Enter<BootstrapState>();
        }
    }
}