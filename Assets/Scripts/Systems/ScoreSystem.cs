using System;
using UnityEngine;

namespace Systems
{
    public class ScoreSystem : MonoBehaviour
    {
        [SerializeField] private int scorePointsPerSecond = 10;
        [SerializeField] private float scoreUpdateInterval = 0.1f;
        [SerializeField] private float airborneMultiplier = 1.5f;
        [SerializeField] private Entities.Car car;

        private int _totalScore;
        private int _starBonus;
        private int _airborneBonus;
        private float _timer;

        public int TotalScore => _totalScore;
        public int StarBonus => _starBonus;
        public int AirborneBonus => _airborneBonus;

        public event Action<int> TotalScoreChange;
        public event Action<int> StarScoreChange;
        public event Action<int> AirborneScoreChange;

        private void Awake()
        {
            car.PickedUpStar += PickedUpStarHandler;
            car.CarLanded += CarLandedHandler;

            InitializeFields();
        }

        private void Update()
        {
            _timer += Time.deltaTime;

            if (_timer >= scoreUpdateInterval)
            {
                _totalScore += Mathf.RoundToInt(scorePointsPerSecond * scoreUpdateInterval);
                _timer = 0f;

                TotalScoreChange?.Invoke(_totalScore);
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
            _totalScore += scoreValue;
            _starBonus += scoreValue;

            TotalScoreChange?.Invoke(_totalScore);
            StarScoreChange?.Invoke(scoreValue);
        }

        private void CarLandedHandler(float airborneTime)
        {
            var airborneBonus = Mathf.RoundToInt(airborneTime * scorePointsPerSecond * airborneMultiplier);

            if (airborneBonus == 0) return;

            _totalScore += airborneBonus;
            _airborneBonus += airborneBonus;

            TotalScoreChange?.Invoke(_totalScore);
            AirborneScoreChange?.Invoke(airborneBonus);
        }
    }
}