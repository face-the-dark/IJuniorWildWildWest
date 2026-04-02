using Cysharp.Threading.Tasks;
using Infrastructure.Factory;
using UnityEngine;

namespace Infrastructure.StateMachine.States
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

        public async UniTaskVoid Enter(string sceneName)
        {
            await _sceneLoader.Load(sceneName);
            
            InitializeGameWorld();
            
            _gameStateMachine.Enter<GameLoopState>();
        }

        public void Exit()
        {
            
        }

        private void InitializeGameWorld()
        {
            InitializePlayer();
        }

        private void InitializePlayer()
        {
            _gameFactory.CreatePlayer(GameObject.FindWithTag("PlayerSpawnPoint").transform.position);
        }
    }
}