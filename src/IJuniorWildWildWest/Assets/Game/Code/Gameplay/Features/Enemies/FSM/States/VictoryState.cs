using Game.Gameplay.Features.FSM;

namespace Game.Gameplay.Features.Enemies.FSM.States
{
    public class VictoryState : State
    {
        private EnemyAnimator _animator;

        public VictoryState(StateMachine stateMachine, EnemyAnimator animator) : base(stateMachine) => 
            _animator = animator;

        public override void Enter() => 
            _animator.PlayVictory();
    }
}