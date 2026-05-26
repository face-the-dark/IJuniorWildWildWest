using Game.Gameplay.Features.FSM;
using Game.Gameplay.Features.Players;

namespace Game.Gameplay.Features.Enemies.FSM.Transitions
{
    public class ToVictoryStateTransition : Transition
    {
        private Player _player;
        
        public ToVictoryStateTransition(State nextState, Player player) : base(nextState) => 
            _player = player;
        
        protected override bool CanTransit() => 
            _player.IsDead;
    }
}