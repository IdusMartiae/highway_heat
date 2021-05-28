using Entities;
using UnityEngine;

namespace Spawners
{
    public class RoadSegmentSpawner : MonoBehaviour
    {
        [SerializeField] private RoadSegment colliderPrefab;
        [SerializeField] private int poolSize = 100;

        private RoadSegmentFactory roadSegmentFactory;

        public void Awake()
        {
            roadSegmentFactory = new RoadSegmentFactory(poolSize, colliderPrefab, transform);
        }

        public void Update()
        {
            var roadSegment = roadSegmentFactory.Create();
            roadSegment.transform.position = transform.position;
        }
    }
}