using Game.Gameplay.Features.Players;
using Game.Infrastructure.Services;
using Game.Infrastructure.States.StateInfrastructure;

namespace Game.Infrastructure.States.GameStates
{
    public class GameLoopState : IPayLoadedState<Player>
    {
        private readonly WaveService _waveService;

        public GameLoopState(WaveService waveService)
        {
            _waveService = waveService;
        }

        public void Exit()
        {
        }

        public void Enter(Player player)
        {
            _waveService.StartWave(player);
        }
    }
}
