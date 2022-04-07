using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameKit;

namespace Fsm
{
    public class StateMachine
    {
        private class Transition
        {
            public string id { get; }
            public System.Func<bool> Condition { get; }
            public IState From { get; }
            public IState To { get; }
            public bool isActive { get; set; }
            public float Duration { get; set; }
            public float ExitTime { get; set; }
            public float Possibility { get; set; }
            public Transition(IState from, IState to, System.Func<bool> condition, float duration = 0, float exitTime = 0, float possibility = 1, bool active = true)
            {
                id = from.ToString() + "-" + to.ToString();
                From = from;
                To = to;
                Condition = condition;
                Duration = duration;
                ExitTime = exitTime;
                Possibility = possibility;
                isActive = active;
            }
        }
        private bool isExit = false;
        private float nextDuration = 0;
        private float startTime;
        private IState currentState;
        private Dictionary<System.Type, List<Transition>> transitions = new Dictionary<System.Type, List<Transition>>();
        private List<Transition> currentTransitions = new List<Transition>();
        private List<Transition> anyTransitions = new List<Transition>();
        private static List<Transition> EmptyTransitions = new List<Transition>(0);

        public void Update()
        {
            if (Time.fixedTime - startTime < nextDuration && nextDuration != 0)
                return;

            var transition = GetTransition();
            if (transition != null && !isExit)
            {
                if (transition.Duration != 0)
                {
                    startTime = Time.fixedTime;
                    nextDuration = transition.Duration;
                }
                else
                {
                    nextDuration = 0;
                }


                if (transition.ExitTime == 0)
                {
                    SetState(transition.To);
                }
                else
                {
                    isExit = true;
                    MonoManager.instance.StartCoroutine(PerformTransition(transition.ExitTime, transition.To));
                }
            }
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

        public void AddTransition(IState from, IState to, System.Func<bool> predicate, float duration = 0, float exitTime = 0, float possibility = 1, bool active = true, bool randDuration = false)
        {
            // Also can use TryGetValue Method
            if(randDuration)
                duration = Random.Range(0.5f,3f);
            if (!transitions.ContainsKey(from.GetType()))
            {
                transitions.Add(from.GetType(), new List<Transition>());
            }
            transitions[from.GetType()].Add(new Transition(from, to, predicate, duration, exitTime, possibility, active));
        }

        public void AddAnyTransition(IState to, System.Func<bool> predicate, float duration = 0, float exitTime = 0, float possibility = 1, bool active = true, bool randDuration = false)
        {
            if(randDuration)
                duration = Random.Range(0.5f,3f);
            IState anyState = new AnyState();
            anyTransitions.Add(new Transition(anyState, to, predicate, duration, exitTime, possibility, active));
        }

        private Transition GetTransition()
        {
            foreach (var transition in anyTransitions)
            {
                if (!transition.isActive)
                    continue;

                if (transition.Condition())
                {
                    if (Random.Range(0f, 1f) < transition.Possibility && transition.To != currentState)
                    {
                        return transition;
                    }
                }
            }

            foreach (var transition in currentTransitions)
            {
                if (!transition.isActive)
                    continue;

                if (transition.Condition())
                {
                    if (Random.Range(0f, 1f) < transition.Possibility)
                    {
                        return transition;
                    }
                }
            }
            return null;
        }

        private IEnumerator PerformTransition(float exitTime, IState state)
        {
            yield return exitTime;
            SetState(state);
            isExit = false;
        }

        public void SetTransitionActive(string id, bool isActive)
        {
            foreach (var transition in anyTransitions)
            {
                if (transition.id == id)
                {
                    transition.isActive = isActive;
                }
            }

            foreach (var trans in transitions)
            {
                foreach (var transition in trans.Value)
                {
                    if (transition.id == id)
                    {
                        transition.isActive = isActive;
                    }
                }
            }
        }

        public void SetTransitionDuration(string id, float durtaion)
        {
            foreach (var transition in anyTransitions)
            {
                if (transition.id == id)
                {
                    transition.Duration = durtaion;
                }
            }

            foreach (var trans in transitions)
            {
                foreach (var transition in trans.Value)
                {
                    if (transition.id == id)
                    {
                        transition.Duration = durtaion;
                    }
                }
            }
        }

        public void SetTransitionExitTime(string id, float exitTime)
        {
            foreach (var transition in anyTransitions)
            {
                if (transition.id == id)
                {
                    transition.ExitTime = exitTime;
                }
            }

            foreach (var trans in transitions)
            {
                foreach (var transition in trans.Value)
                {
                    if (transition.id == id)
                    {
                        transition.ExitTime = exitTime;
                    }
                }
            }
        }

        public void SetTransitionPossibility(string id, float possibility)
        {
            foreach (var transition in anyTransitions)
            {
                if (transition.id == id)
                {
                    transition.Possibility = possibility;
                }
            }

            foreach (var trans in transitions)
            {
                foreach (var transition in trans.Value)
                {
                    if (transition.id == id)
                    {
                        transition.Possibility = possibility;
                    }
                }
            }
        }

    }
}
