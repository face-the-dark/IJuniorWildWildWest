using Cysharp.Threading.Tasks;

namespace Infrastructure.StateMachine.States
{
    public interface IState : IExitableState
    {
        void Enter();
    }
    
    public interface IPayLoadedState<TPayload> : IExitableState
    {
        UniTaskVoid Enter(TPayload payload);
    }

    public interface IExitableState
    {
        void Exit();
    }
}