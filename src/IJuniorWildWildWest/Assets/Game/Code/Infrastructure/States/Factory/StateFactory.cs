using Game.Infrastructure.States.StateInfrastructure;
using VContainer;

namespace Game.Infrastructure.States.Factory
{
    public class StateFactory : IStateFactory
    {
        private readonly IObjectResolver _container;

        public StateFactory(IObjectResolver container)
        {
            _container = container;
        }

        public T GetState<T>() where T : class, IExitableState
        {
            return _container.Resolve<T>();
        }
    }
}