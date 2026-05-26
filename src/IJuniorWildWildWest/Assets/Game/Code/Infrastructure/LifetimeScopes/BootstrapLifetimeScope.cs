using Game.Infrastructure.States.GameStates;
using Game.Infrastructure.States.StateMachine;
using VContainer;
using VContainer.Unity;

namespace Game.Infrastructure.LifetimeScopes
{
    public class BootstrapLifetimeScope : LifetimeScope, IInitializable
    {
        protected override void Configure(IContainerBuilder builder)
        {
            RegisterStateMachine(builder);
        }

        private void RegisterStateMachine(IContainerBuilder builder)
        {
            builder.Register<GameStateMachine>(Lifetime.Singleton).As<IGameStateMachine>();
        }

        public void Initialize()
        {
            Container.Resolve<IGameStateMachine>().Enter<BootstrapState>();
        }
    }
}