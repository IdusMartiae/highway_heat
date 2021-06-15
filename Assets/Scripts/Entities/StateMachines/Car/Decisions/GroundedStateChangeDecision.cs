using System.Collections.Generic;
using UnityEngine;

namespace Entities.StateMachines.Car.Decisions
{
    public class GroundedStateChangeDecision : Decision
    {
        private readonly Transform _carTransform;
        private readonly List<GameEntity> _roadColliders;

        public GroundedStateChangeDecision(Transform carTransform, List<GameEntity> roadColliders)
        {
            _carTransform = carTransform;
            _roadColliders = roadColliders;
        }

        public override bool DoDecide()
        {
            // need new grounded criteria, this doesn't work properly 
            var isGrounded = _carTransform.position.y + 1 < _roadColliders[0].transform.position.y ;
            return isGrounded;
        }
    }
}