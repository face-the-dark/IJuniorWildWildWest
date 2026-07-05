using Game.Gameplay.Features;
using Game.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Infrastructure.LifetimeScopes
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private LevelInitializer _levelInitializer;
        [SerializeField] private WaveView _waveView;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(_levelInitializer);
            builder.RegisterComponent(_waveView);
        }
    }
}