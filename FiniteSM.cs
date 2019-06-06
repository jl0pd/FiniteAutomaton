using System;
using System.Collections;
using System.Collections.Generic;

namespace StateMachine
{
    public class FiniteSM<TResult, TState> : IEnumerable<TResult>, IEnumerator<TResult>
    {
        private TResult Value;
        private TState InitialState;
        private TState CurrentState;
        private TState BreakState;
        private Dictionary<TState, Func<TResult, (TState, TResult)>> States;

        public FiniteSM(
            Dictionary<TState, Func<TResult, (TState, TResult)>> states,
            TState initialState, TState breakState)
        {
            States = states;
            CurrentState = InitialState = initialState;
            BreakState = breakState;
        }

        public void ForEach(Action<TResult> fn)
        {
            foreach(var el in this)
            {
                fn(el);
            }
        }

        public void Reset() => CurrentState = InitialState;

        public bool MoveNext()
        {
            (CurrentState, Value) = States[CurrentState](Value);
            return CurrentState.Equals(BreakState) ? false : true;
        }

        public TResult Current
        {
            get => CurrentState.Equals(BreakState) ? throw new InvalidOperationException() : Value;
        }

        Object IEnumerator.Current { get => Current; }

        IEnumerator IEnumerable.GetEnumerator() => (IEnumerator)GetEnumerator();

        public IEnumerator<TResult> GetEnumerator() => (IEnumerator<TResult>)this;

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    States = null;
                    InitialState = default(TState);
                    CurrentState = default(TState);
                    BreakState = default(TState);
                    Value = default(TResult);
                }

                disposedValue = true;
            }
        }

        public void Dispose() => Dispose(true);
        #endregion
    }
}