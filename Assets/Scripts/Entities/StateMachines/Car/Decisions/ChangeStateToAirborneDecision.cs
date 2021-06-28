using Systems.Car;

namespace Entities.StateMachines.Car.Decisions
{
    public class ChangeStateToAirborneDecision : Decision
    {
        private readonly CarPhysics _carPhysics;

        public ChangeStateToAirborneDecision(CarPhysics carPhysics)
        {
            _carPhysics = carPhysics;
        }
        public override bool DecisionResult()
        {
            if (_carPhysics.CachedYVelocity > 0)
            {
                 return _carPhysics.CachedYVelocity - _carPhysics.CurrentYVelocity > 1 ;
            }
            
            return _carPhysics.Gravity - _carPhysics.Acceleration > 1;
        }
    }
}