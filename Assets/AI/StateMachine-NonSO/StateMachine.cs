using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM.NonSO
{
    public class StateMachine
    {
        private class Transition
        {
            public System.Func<bool> Condition { get; }
            public IState To { get; }

            public Transition(IState to, System.Func<bool> condition)
            {
                To = to;
                Condition = condition;
            }
        }

        private IState currentState;
        private Dictionary<System.Type, List<Transition>> transitions = new Dictionary<System.Type, List<Transition>>();
        private List<Transition> currentTransitions = new List<Transition>();
        private List<Transition> anyTransitions = new List<Transition>();
        private static List<Transition> EmptyTransitions = new List<Transition>(0);

        public void Update()
        {
            var transition = GetTransition();
            if (transition != null)
                SetState(transition.To);

            currentState?.Update();
        }

        public IState GetCurrentState()
        {
            return currentState;
        }

        public void SetState(IState state)
        {
            if (state == currentState)
                return;

            currentState?.OnExit();
            currentState = state;
            transitions.TryGetValue(currentState.GetType(), out currentTransitions);    // Find transition from the transition Dic
            if (currentTransitions == null)
                currentTransitions = EmptyTransitions;

            currentState.OnEnter();
        }

        public void AddTransition(IState from, IState to, System.Func<bool> predicate)
        {
            // Also can use TryGetValue Method
            if (!transitions.ContainsKey(from.GetType()))
            {
                transitions.Add(from.GetType(), new List<Transition>());
            }
            transitions[from.GetType()].Add(new Transition(to, predicate));
        }

        public void AddAnyTranstion(IState state, System.Func<bool> predicate)
        {
            anyTransitions.Add(new Transition(state, predicate));
        }

        private Transition GetTransition()
        {
            foreach (var transition in anyTransitions)
                if (transition.Condition())
                    return transition;

            foreach (var transition in currentTransitions)
                if (transition.Condition())
                    return transition;

            return null;
        }
    }

}
