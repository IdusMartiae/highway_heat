using UnityEngine.Events;

namespace Entities.StateMachines.Car
{
    public class CarDecision : Decision
    {
        private bool _receivedEvent;

        public CarDecision(UnityEvent unityEvent)
        {
            unityEvent.AddListener(() => _receivedEvent = true);
        }
        
        public override bool DecisionResult()
        {
            if (_receivedEvent)
            {
                _receivedEvent = false;
                return true;
            }

            return false;
        }
    }
}