using Game.Gameplay.Features.FSM;

namespace Game.Gameplay.Features.Enemies.FSM.States
{
    public class AttackState : State
    {
        private readonly EnemyShooter _shooter;

        public AttackState(StateMachine stateMachine, EnemyShooter shooter) : base(stateMachine) => 
            _shooter = shooter;

        protected override void OnUpdate() => 
            _shooter.Shoot();
    }
}