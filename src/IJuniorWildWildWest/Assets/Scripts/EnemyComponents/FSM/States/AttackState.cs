using FSM;

namespace EnemyComponents.FSM.States
{
    public class AttackState : State
    {
        private EnemyShooter _shooter;

        public AttackState(StateMachine stateMachine, EnemyShooter shooter) : base(stateMachine) => 
            _shooter = shooter;

        protected override void OnUpdate() => 
            _shooter.Shoot();
    }
}