using System.Collections.Generic;

namespace FSM
{
    public abstract class State
    {
        private StateMachine _stateMachine;
        private List<Transition> _transitions;

        protected State(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
            _transitions = new List<Transition>();
        }

        public void AddTransition(Transition transition) => 
            _transitions.Add(transition);

        public virtual void Exit()
        {
        }

        public virtual void Enter()
        {
        }

        public void Update()
        {
            foreach (Transition transition in _transitions)
            {
                if (transition.TryTransit(out State nextState))
                {
                    _stateMachine.ChangeState(nextState);

                    return;
                }
            }

            OnUpdate();
        }

        protected virtual void OnUpdate()
        {
        }
    }
}