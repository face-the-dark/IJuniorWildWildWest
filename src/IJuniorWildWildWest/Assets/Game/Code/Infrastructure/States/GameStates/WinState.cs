using Game.Gameplay.Features.Players;
using Game.Infrastructure.States.StateInfrastructure;

namespace Game.Infrastructure.States.GameStates
{
    public class WinState : IPayLoadedState<Player>
    {
        public void Exit()
        {
        }

        public void Enter(Player player)
        {
            player.Win();
        }
    }
}
