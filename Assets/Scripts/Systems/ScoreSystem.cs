using UnityEngine;

namespace Systems
{
    public class ScoreSystem : MonoBehaviour
    {
        [SerializeField] private int scorePointsPerSecond = 10;
        [SerializeField] private float scoreUpdateInterval = 0.1f;
        [SerializeField] private float airborneMultiplier = 1.5f;
        [SerializeField] private int starValue = 10;
        [SerializeField] private Entities.Car car;
    
        private int _totalScore;
        private int _starBonus;
        private int _airborneBonus;
        private float _timer;
        
        public int TotalScore => _totalScore;
        public int StarBonus => _starBonus;
        public int AirborneBonus => _airborneBonus;

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
            }

        }

        private void InitializeFields()
        {
            _totalScore = 0;
            _starBonus = 0;
            _airborneBonus = 0;
            _timer = 0f;
        }
        
        private void PickedUpStarHandler()
        {
            _totalScore += starValue;
            _starBonus += starValue;
        }

        private void CarLandedHandler(float airborneTime)
        {
            var airborneBonus = Mathf.RoundToInt(airborneTime * scorePointsPerSecond * airborneMultiplier);
            _totalScore += airborneBonus;
            _airborneBonus += airborneBonus;
        }

        public void ResetScore()
        {
            InitializeFields();
        }
    }
}
