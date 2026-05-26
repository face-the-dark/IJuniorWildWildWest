using Game.Infrastructure.States.StateInfrastructure;
using Game.Infrastructure.States.StateMachine;

namespace Game.Infrastructure.States.GameStates
{
    public class BootstrapState : IState
    {
        private const string GameScene = "Game";
        
        private readonly IGameStateMachine _gameStateMachine;

        public BootstrapState(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Exit()
        {
        }

        public void Enter()
        {
            _gameStateMachine.Enter<LoadLevelState, string>(GameScene);
        }
    }
}