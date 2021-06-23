using Simulations.Car;

namespace Entities.StateMachines.Car.Decisions
{
    public class ChangeStateToAirborneDecision : Decision
    {
        private readonly CarPhysics _carPhysics;
        private readonly float _velocityChangeThreshold;
        
        public ChangeStateToAirborneDecision(CarPhysics carPhysics, float velocityChangeThreshold)
        {
            _carPhysics = carPhysics;
            _velocityChangeThreshold = velocityChangeThreshold;
        }
        public override bool DecisionResult()
        {
            return _carPhysics.CachedYVelocity - _carPhysics.CurrentYVelocity > _velocityChangeThreshold;
        }
    }
}