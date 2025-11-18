using System;
using UnityEngine;

namespace Services
{
    public class CounterService
    {
        private float _elapsed;
        private float _totalElapsed;
        private readonly float _tickInterval;
        private Action<float> _onTick;

        public bool IsRunning { get; private set; }

        public CounterService(float tickInterval = 0.1f)
        {
            _tickInterval = tickInterval;
        }

        public void StartCounter(Action<float> onTickCallback)
        {
            if (IsRunning)
            {
                return;   
            }
            _onTick = onTickCallback;
            _elapsed = 0f;
            _totalElapsed = 0f;
            IsRunning = true;
        }

        public void StopCounter()
        {
            IsRunning = false;
        }

        public void ResetCounter()
        {
            _elapsed = 0f;
        }

        /// <summary>
        /// Called by the MonoBehaviour runner every frame.
        /// </summary>
        public void Tick(float deltaTime)
        {
            if (!IsRunning) return;

            _elapsed += deltaTime;
            
            if (_elapsed >= _tickInterval)
            {
                //Get actual seconds
                _totalElapsed += _tickInterval;
                _onTick?.Invoke(_totalElapsed);
                // _onTick?.Invoke(_totalElapsed * _tickInterval);
                // _elapsed = 0f;
                _elapsed -= _tickInterval;
            }
        }
    }
}
