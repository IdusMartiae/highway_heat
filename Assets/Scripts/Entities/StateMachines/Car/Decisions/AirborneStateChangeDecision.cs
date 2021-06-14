using UnityEngine;

namespace Entities.StateMachines.Car.Decisions
{
    public class AirborneStateChangeDecision : Decision
    {
        private readonly Transform _carTransform;
        private readonly float _airborneAngle;

        public AirborneStateChangeDecision(Transform carTransform, float airborneAngle)
        {
            _carTransform = carTransform;
            _airborneAngle = airborneAngle;
        }

        public override bool DoDecide()
        {
            var currentAngle = _carTransform.eulerAngles.x > 180
                ? _carTransform.eulerAngles.x - 360
                : _carTransform.eulerAngles.x;
            return currentAngle <= -_airborneAngle;
        }
    }
}