using System;
using Configurations;
using UnityEngine;
using Zenject;

namespace Systems
{
    public class ScoreSystem : MonoBehaviour
    {
        [SerializeField] private int scorePointsPerSecond = 5;
        [SerializeField] private float scoreUpdateInterval = 0.2f;
        [SerializeField] private float airborneMultiplier = 1.5f;
        
        private GameConfiguration _gameConfiguration;
        private Entities.Car _car;
        private int _totalScore;
        private int _distanceBonus;
        private int _starBonus;
        private int _airborneBonus;
        private float _timer;

        [Inject]
        private void Initialize(GameConfiguration gameConfiguration, Entities.Car car)
        {
            _gameConfiguration = gameConfiguration;
            _car = car;
        }
        
        public int TotalScore
        {
            get => _totalScore;
            set
            {
                _totalScore = value;
                TotalScoreChange(_totalScore);
            }
        }

        public int DistanceBonus => _distanceBonus;
        public int StarBonus => _starBonus;
        public int AirborneBonus => _airborneBonus;

        // TODO REPLACE WITH SIGNALS
        public event Action<int> TotalScoreChange = delegate { };
        public event Action<int> StarScoreChange = delegate { };
        public event Action<int> AirborneScoreChange = delegate { };

        private void Awake()
        {
            _car.PickedUpStar += PickedUpStarHandler;
            _car.CarLanded += CarLandedHandler;
            InitializeFields();
        }

        private void Update()
        {
            if (_gameConfiguration.Paused) return;

            _timer += Time.deltaTime;

            if (_timer >= scoreUpdateInterval)
            {
                var distanceScoreDelta = Mathf.RoundToInt(scorePointsPerSecond * scoreUpdateInterval);

                _distanceBonus += distanceScoreDelta;
                _totalScore += distanceScoreDelta;
                _timer = 0f;

                TotalScoreChange(_totalScore);
            }
        }
        
        public void ResetScore()
        {
            InitializeFields();
        }

        private void InitializeFields()
        {
            _totalScore = 0;
            _starBonus = 0;
            _airborneBonus = 0;
            _timer = 0f;
        }

        private void PickedUpStarHandler(int scoreValue)
        {
            _starBonus += scoreValue;
            StarScoreChange(scoreValue);
        }

        private void CarLandedHandler(float airborneTime)
        {
            var airborneBonus = Mathf.RoundToInt(airborneTime * scorePointsPerSecond * airborneMultiplier);

            if (airborneBonus == 0) return;

            _airborneBonus += airborneBonus;
            AirborneScoreChange(airborneBonus);
        }
    }
}