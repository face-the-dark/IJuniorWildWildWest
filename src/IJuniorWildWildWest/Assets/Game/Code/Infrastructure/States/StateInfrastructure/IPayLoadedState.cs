namespace Game.Infrastructure.States.StateInfrastructure
{
    public interface IPayLoadedState<TPayload> : IExitableState
    {
        void Enter(TPayload payload);
    }
}