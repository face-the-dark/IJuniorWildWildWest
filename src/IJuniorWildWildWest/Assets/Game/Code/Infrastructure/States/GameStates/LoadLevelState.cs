using Game.Gameplay.Features.Players;
using Game.Infrastructure.Factory;
using Game.Infrastructure.States.StateInfrastructure;
using Game.Infrastructure.States.StateMachine;

namespace Game.Infrastructure.States.GameStates
{
    public class LoadLevelState : IPayLoadedState<string>
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IGameFactory _gameFactory;
        private readonly SceneLoader _sceneLoader;

        public LoadLevelState(IGameStateMachine gameStateMachine, IGameFactory gameFactory, SceneLoader sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
            _gameFactory = gameFactory;
            _sceneLoader = sceneLoader;
        }

        public void Enter(string sceneName)
        {
            _sceneLoader.Load(sceneName);

            Player player = InitializeGameWorld();

            _gameStateMachine.Enter<GameLoopState, Player>(player);
        }

        public void Exit()
        {
            
        }

        private Player InitializeGameWorld() =>
            _gameFactory.CreatePlayer();
    }
}