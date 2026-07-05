using Game.Gameplay.Cameras;
using Game.Gameplay.Features;
using Game.Gameplay.Features.Players;
using Game.Infrastructure.Factory;
using Game.Infrastructure.Loading;
using Game.Infrastructure.States.StateInfrastructure;
using Game.Infrastructure.States.StateMachine;

namespace Game.Infrastructure.States.GameStates
{
    public class LoadLevelState : IPayLoadedState<string>
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IGameFactory _gameFactory;
        private readonly ISceneLoader _sceneLoader;

        public LoadLevelState(IGameStateMachine gameStateMachine, IGameFactory gameFactory, ISceneLoader sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
            _gameFactory = gameFactory;
            _sceneLoader = sceneLoader;
        }

        public async void Enter(string sceneName)
        {
            await _sceneLoader.Load(sceneName);

            Player player = InitializeGameWorld();

            _gameStateMachine.Enter<GameLoopState, Player>(player);
        }

        public void Exit()
        {
        }

        private Player InitializeGameWorld()
        {
            PlayerCameraInfo playerCameraInfo = _gameFactory.CreatePlayerCameraInfo();
            PlayerCamera playerCamera = _gameFactory.CreatePlayerCamera();
            _gameFactory.CreateLookTarget();
            Player player = _gameFactory.CreatePlayer();
            Weapon playerWeapon = _gameFactory.CreatePlayerWeapon();

            return player;
        }
    }
}