using UnityEngine;
using UnityEngine.Events;

namespace Entities
{
    public class EventSystem
    {
        public UnityEvent<Transform, float> spawnerMovementEvent = new UnityEvent<Transform, float>();
    }
}