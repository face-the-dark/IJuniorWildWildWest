using Infrastructure.AssetManagement;
using Infrastructure.Factory;
using Infrastructure.StateMachine;
using VContainer;
using VContainer.Unity;

namespace Infrastructure.Container
{
    public class RootLifetimeScope : LifetimeScope
    {
        protected override void Awake()
        {
            base.Awake();
            
            DontDestroyOnLoad(this);
        }
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<AssetProvider>(Lifetime.Singleton);
            builder.Register<GameFactory>(Lifetime.Singleton).As<IGameFactory>();
            builder.Register<SceneLoader>(Lifetime.Singleton);
            builder.Register<GameStateMachine>(Lifetime.Singleton).As<IGameStateMachine>();

            builder.RegisterEntryPoint<GameBootstrapper>();
        }
    }
}