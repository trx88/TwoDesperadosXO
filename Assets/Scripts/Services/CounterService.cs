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

        private bool _isRunning;

        public CounterService(float tickInterval = 0.1f)
        {
            _tickInterval = tickInterval;
        }

        public void StartCounter(Action<float> onTickCallback)
        {
            if (_isRunning)
            {
                return;   
            }
            _onTick = onTickCallback;
            _elapsed = 0f;
            _totalElapsed = 0f;
            _isRunning = true;
        }

        public void StopCounter()
        {
            _isRunning = false;
        }

        /// <summary>
        /// Called by the MonoBehaviour runner every frame.
        /// </summary>
        public void Tick(float deltaTime)
        {
            if (!_isRunning) return;

            _elapsed += deltaTime;
            
            if (_elapsed >= _tickInterval)
            {
                //Get actual seconds
                _totalElapsed += _tickInterval;
                _onTick?.Invoke(_totalElapsed);
                _elapsed -= _tickInterval;
            }
        }
    }
}
