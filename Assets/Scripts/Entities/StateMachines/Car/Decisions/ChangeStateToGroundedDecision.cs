using Systems.Car;
using UnityEngine;

namespace Entities.StateMachines.Car.Decisions
{
    public class ChangeStateToGroundedDecision : Decision
    {
        private readonly CarPhysics _carPhysics;
        private readonly Transform _carTransform;
        private readonly RoadRenderer _roadRenderer;
        private readonly int _anchorPointIndex;
        private readonly float _halfLength;

        public ChangeStateToGroundedDecision(Transform carTransform,
            CarPhysics carPhysics,
            RoadRenderer roadRenderer,
            int anchorPointIndex,
            float halfLength)
        {
            _carTransform = carTransform;
            _carPhysics = carPhysics;
            _roadRenderer = roadRenderer;
            _anchorPointIndex = anchorPointIndex;
            _halfLength = halfLength;
        }
        
        public override bool DecisionResult()
        {
            var velocityIsNegative = _carPhysics.CurrentYVelocity < 0;
            var carIsTouchingTheRoad = _carTransform.position.y -
                                       _halfLength * Mathf.Sin(_carTransform.eulerAngles.x * Mathf.Deg2Rad) - _roadRenderer.GetRoadThickness()
                                       < _roadRenderer.PositionPoints[_anchorPointIndex].y;
            return velocityIsNegative && carIsTouchingTheRoad;
        }
    }
}