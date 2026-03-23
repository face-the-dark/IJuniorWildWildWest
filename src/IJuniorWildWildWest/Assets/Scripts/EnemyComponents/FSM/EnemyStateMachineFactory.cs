using EnemyComponents.FSM.States;
using EnemyComponents.FSM.Transitions;
using FSM;
using UnityEngine;

namespace EnemyComponents.FSM
{
    public class EnemyStateMachineFactory : MonoBehaviour
    {
        public StateMachine Create
        (
            EnemyMover mover,
            ShootPointsCalculator shootPointsCalculator,
            EnemyShooter shooter,
            EnemyVision vision,
            Collider collider,
            Health health
        )
        {
            StateMachine stateMachine = new StateMachine();

            MoveState moveState = new MoveState(stateMachine, mover, shootPointsCalculator);
            AttackState attackState = new AttackState(stateMachine, shooter);
            DeadState deadState = new DeadState(stateMachine, collider);

            ToMoveStateTransition toMoveStateTransition = new ToMoveStateTransition(moveState, vision);
            ToAttackStateTransition toAttackStateTransition = new ToAttackStateTransition(attackState, mover);
            ToDeadStateTransition toDeadStateTransition = new ToDeadStateTransition(deadState, health);

            moveState.AddTransition(toAttackStateTransition);
            attackState.AddTransition(toMoveStateTransition);
            moveState.AddTransition(toDeadStateTransition);
            attackState.AddTransition(toDeadStateTransition);

            stateMachine.ChangeState(moveState);

            return stateMachine;
        }
    }
}