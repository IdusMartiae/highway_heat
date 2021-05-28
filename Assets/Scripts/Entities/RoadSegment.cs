using UnityEngine;

namespace Entities
{
    public class RoadSegment : MonoBehaviour
    {
        private RoadSegmentFactory _factory;

        public void Initialize(RoadSegmentFactory factory)
        {
            _factory = factory;
        }
        
        public void OnBecameInvisible()
        {
            Debug.Log($"Out of view: {name}");
            _factory.Destroy(this);
        }
    }
}