namespace Entities.StateMachines.Car.Decisions
{
    public class ChangeStateToGroundedDecision : Decision
    {
        public override bool DecisionResult()
        {
            return true;
        }
    }
}