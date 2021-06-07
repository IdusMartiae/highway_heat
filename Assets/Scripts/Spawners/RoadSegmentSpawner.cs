using Entities;
using Factories;
using UnityEngine;

namespace Spawners
{
    public class RoadSegmentSpawner : MonoBehaviour
    {
        [SerializeField] private GameEntity colliderPrefab;
        [SerializeField] private int poolSize = 100;
        [SerializeField] private float destroyDistance = 10;
        
        private RoadSegmentFactory _roadSegmentFactory;
        
        private void Awake()
        {
            _roadSegmentFactory = new RoadSegmentFactory(poolSize, colliderPrefab, transform, destroyDistance);
        }

        private void Update()
        {
            var roadSegment = _roadSegmentFactory.Create().gameObject;
            roadSegment.transform.position = transform.position;
        }
    }
}