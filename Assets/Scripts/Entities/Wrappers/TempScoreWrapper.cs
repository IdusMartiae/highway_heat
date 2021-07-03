using UnityEngine;

namespace Entities.Wrappers
{
    public class TempScoreWrapper
    {
        private int _score;
        private readonly float _lifetime;
        private float _currentLifetime;

        public int Score
        {
            get => _score;
            set
            {
                _score = value;
                _currentLifetime = 0;
            }
        }

        public TempScoreWrapper(float lifetime)
        {
            _lifetime = lifetime;
        }

        public bool IsScoreAlive()
        {
            _currentLifetime += Time.deltaTime;

            return !(_currentLifetime >= _lifetime);
        }

        public int FlushDeadScore()
        {
            var score = _score;
            
            _score = 0;
            _currentLifetime = 0;
            
            return score;
        }
    }
}