namespace Infrastructure.StateMachine.States
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