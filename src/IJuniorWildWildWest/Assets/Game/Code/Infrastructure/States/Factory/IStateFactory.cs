using Game.Infrastructure.States.StateInfrastructure;

namespace Game.Infrastructure.States.Factory
{
    public interface IStateFactory
    {
        T GetState<T>() where T : class, IExitableState;
    }
}