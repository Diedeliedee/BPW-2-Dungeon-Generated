using System;
using System.Collections.Generic;
using UnityEngine;

namespace Joeri.Tools.Structure.StateMachine.Advanced
{
    public class CompositeFSM<T> : IStateMachine
    {
        private CompositeState<T> m_activeRootState = null;
        private Dictionary<Type, CompositeState<T>> m_rootStates = new();

        public CompositeState<T> activeState { get => m_activeRootState; }

        public CompositeFSM(T _source, params State<T>[] _states)
        {
            for (int i = 0; i < _states.Length; i++)
            {
                m_rootStates.Add(_states[i].execution.GetType(), BuildState(_states[i], this, _source));
                OnSwitch(_states[0].execution.GetType());
            }
        }

        public void Tick()
        {
            m_activeRootState.Tick();
        }

        public bool OnSwitch(Type state)
        {
            m_activeRootState?.Reset();
            m_activeRootState?.OnExit();

            try     { m_activeRootState = m_rootStates[state]; }
            catch   { Debug.LogError($"The state: '{state.Name}' is not found within the state dictionary."); return false; }

            m_activeRootState.OnEnter();
            m_activeRootState.Activate();
            return true;
        }

        private CompositeState<T> BuildState(State<T> _state, IStateMachine _parent, T _source)
        {
            var createdState    = new CompositeState<T>(_state.execution, _state.conditions, _parent, _source);
            var childAmount     = _state.children.Length;

            if (childAmount > 0)
            {
                var children = new CompositeState<T>[childAmount];

                for (int i = 0; i < _state.children.Length; i++) children[i] = BuildState(_state.children[i], createdState, _source);
                createdState.RegisterChildren(children);
            }
            return createdState;
        }

        public void Draw(Vector3 _position)
        {
            m_activeRootState.Draw(_position, "");
        }
    }
}