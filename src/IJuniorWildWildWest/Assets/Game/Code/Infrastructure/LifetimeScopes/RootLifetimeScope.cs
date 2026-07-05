using Game.Gameplay.Cameras.Provider;
using Game.Gameplay.Features;
using Game.Gameplay.Features.Players;
using Game.Gameplay.Levels;
using Game.Infrastructure.AssetManagement;
using Game.Infrastructure.Factory;
using Game.Infrastructure.Loading;
using Game.Infrastructure.Services;
using Game.Infrastructure.States.Factory;
using Game.Infrastructure.States.GameStates;
using Game.Infrastructure.States.StateMachine;
using VContainer;
using VContainer.Unity;

namespace Game.Infrastructure.LifetimeScopes
{
    public class RootLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            RegisterStateMachine(builder);
            RegisterGameplayServices(builder);
            RegisterGameStates(builder);
            RegisterStateFactory(builder);
            RegisterGameplayFactories(builder);
            RegisterAssetManagementServices(builder);
            RegisterCommonServices(builder);
            RegisterEntryPoint(builder);
            builder.Register<DirectionCalculator>(Lifetime.Singleton);
            builder.Register<PlayerDataProvider>(Lifetime.Singleton);
        }

        private void RegisterEntryPoint(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<Bootstrapper>();
        }

        private void RegisterCommonServices(IContainerBuilder builder)
        {
            builder.Register<SceneLoader>(Lifetime.Singleton).As<ISceneLoader>();
        }

        private void RegisterAssetManagementServices(IContainerBuilder builder)
        {
            builder.Register<AssetProvider>(Lifetime.Singleton).As<IAssetProvider>();
        }

        private void RegisterGameplayFactories(IContainerBuilder builder)
        {
            builder.Register<GameFactory>(Lifetime.Singleton).As<IGameFactory>();
        }

        private void RegisterStateFactory(IContainerBuilder builder)
        {
            builder.Register<StateFactory>(Lifetime.Singleton).As<IStateFactory>();
        }

        private void RegisterGameStates(IContainerBuilder builder)
        {
            builder.Register<BootstrapState>(Lifetime.Singleton);
            builder.Register<LoadLevelState>(Lifetime.Singleton);
            builder.Register<GameLoopState>(Lifetime.Singleton);
            builder.Register<WinState>(Lifetime.Singleton);
        }

        private void RegisterGameplayServices(IContainerBuilder builder)
        {
            builder.Register<LevelDataProvider>(Lifetime.Singleton).As<ILevelDataProvider>();
            builder.Register<CameraProvider>(Lifetime.Singleton).As<ICameraProvider>();
            builder.Register<WaveService>(Lifetime.Singleton);
        }

        private void RegisterStateMachine(IContainerBuilder builder)
        {
            builder.Register<GameStateMachine>(Lifetime.Singleton).As<IGameStateMachine>();
        }
    }
}