using System.Collections.Generic;
using Entities.Factories;
using UnityEngine;

namespace Entities.Spawners
{
    public class RoadSegmentSpawner : MonoBehaviour
    {
        [SerializeField] private GameEntity colliderPrefab;
        [SerializeField] private int poolSize = 10;
        [SerializeField] private float destroyDistance = 10;
        [SerializeField] private int colliderCaptureNumber = 5;

        private RoadSegmentFactory _roadSegmentFactory;

        public List<GameEntity> CapturedColliderQueue { get; private set; }

        private void Awake()
        {
            InitializeSpawner();
        }

        private void FixedUpdate()
        {
            CreateColliderAndAddToQueue();
        }

        private void InitializeSpawner()
        {
            _roadSegmentFactory = new RoadSegmentFactory(poolSize, colliderPrefab, transform, destroyDistance);
            CapturedColliderQueue = new List<GameEntity>();
        }

        private void AddColliderToQueue(GameEntity colliderGameEntity)
        {
            if (CapturedColliderQueue.Count == colliderCaptureNumber)
            {
                RemoveColliderFromQueue();
            }

            CapturedColliderQueue.Add(colliderGameEntity);
        }

        private void RemoveColliderFromQueue()
        {
            if (CapturedColliderQueue.Count != 0)
            {
                CapturedColliderQueue.RemoveAt(0);
            }
        }

        private void CreateColliderAndAddToQueue()
        {
            var roadSegment = _roadSegmentFactory.Create();
            roadSegment.transform.position = transform.position;

            AddColliderToQueue(roadSegment);
        }
    }
}