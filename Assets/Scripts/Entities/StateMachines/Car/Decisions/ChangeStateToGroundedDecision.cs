using Simulations.Car;
using UnityEngine;

namespace Entities.StateMachines.Car.Decisions
{
    public class ChangeStateToGroundedDecision : Decision
    {
        private readonly CarPhysics _carPhysics;
        private readonly Transform _carTransform;
        private readonly RoadRenderer _roadRenderer;
        private readonly int _anchorPointIndex;
        private readonly float _groundedPositionThreshold;

        public ChangeStateToGroundedDecision(Transform carTransform,
            CarPhysics carPhysics,
            RoadRenderer roadRenderer,
            int anchorPointIndex,
            float groundedPositionThreshold)
        {
            _carTransform = carTransform;
            _carPhysics = carPhysics;
            _roadRenderer = roadRenderer;
            _anchorPointIndex = anchorPointIndex;
            _groundedPositionThreshold = groundedPositionThreshold;
        }
        
        public override bool DecisionResult()
        {
            return (_carPhysics.CurrentYVelocity < 0) &&
                   (_carTransform.position.y - _roadRenderer.PositionPoints[_anchorPointIndex].y <
                    _groundedPositionThreshold);
        }
    }
}