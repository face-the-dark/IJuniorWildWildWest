using EnemyComponents.FSM.States;
using EnemyComponents.FSM.Transitions;
using FSM;
using PlayerComponents;
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
            EnemyAnimator animator,
            Collider collider,
            Health health,
            Player player
        )
        {
            StateMachine stateMachine = new StateMachine();

            MoveState moveState = new MoveState(stateMachine, mover, shootPointsCalculator);
            AttackState attackState = new AttackState(stateMachine, shooter);
            DeadState deadState = new DeadState(stateMachine, collider);
            VictoryState victoryState = new VictoryState(stateMachine, animator);

            ToMoveStateTransition toMoveStateTransition = new ToMoveStateTransition(moveState, vision);
            ToAttackStateTransition toAttackStateTransition = new ToAttackStateTransition(attackState, mover);
            ToDeadStateTransition toDeadStateTransition = new ToDeadStateTransition(deadState, health);
            ToVictoryStateTransition toVictoryStateTransition = new ToVictoryStateTransition(victoryState, player);

            moveState.AddTransition(toAttackStateTransition);
            moveState.AddTransition(toDeadStateTransition);
            moveState.AddTransition(toVictoryStateTransition);
            
            attackState.AddTransition(toMoveStateTransition);
            attackState.AddTransition(toDeadStateTransition);
            attackState.AddTransition(toVictoryStateTransition);

            stateMachine.ChangeState(moveState);

            return stateMachine;
        }
    }
}