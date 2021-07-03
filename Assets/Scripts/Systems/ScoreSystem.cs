using System;
using Configurations;
using UnityEngine;

namespace Systems
{
    public class ScoreSystem : MonoBehaviour
    {
        [SerializeField] private GameConfiguration gameConfiguration;
        [SerializeField] private int scorePointsPerSecond = 10;
        [SerializeField] private float scoreUpdateInterval = 0.1f;
        [SerializeField] private float airborneMultiplier = 1.5f;
        [SerializeField] private Entities.Car car;

        private int _totalScore;
        private int _distanceBonus;

        private int _starBonus;
        private int _airborneBonus;

        private float _timer;

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
            car.PickedUpStar += PickedUpStarHandler;
            car.CarLanded += CarLandedHandler;

            InitializeFields();
        }

        private void Update()
        {
            if (gameConfiguration.Paused) return;

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