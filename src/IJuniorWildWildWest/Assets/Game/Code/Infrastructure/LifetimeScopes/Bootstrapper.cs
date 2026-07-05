using Game.Infrastructure.States.GameStates;
using Game.Infrastructure.States.StateMachine;
using VContainer.Unity;

namespace Game.Infrastructure.LifetimeScopes
{
    public class Bootstrapper : IInitializable
    {
        private readonly IGameStateMachine _gameStateMachine;

        public Bootstrapper(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Initialize()
        {
            _gameStateMachine.Enter<BootstrapState>();
        }
    }
}