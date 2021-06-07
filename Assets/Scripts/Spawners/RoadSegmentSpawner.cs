using System.Collections.Generic;
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
        [SerializeField] private int colliderFrontQueueSize;
            
        private RoadSegmentFactory _roadSegmentFactory;
        
        public List<GameEntity> ColliderFrontQueue { get; private set; }

        private void Awake()
        {
            InitializeSpawner();
        }

        private void FixedUpdate()
        {
            CreateColliderAndAddToQueue();
        }

        private void OnDestroy()
        {
            ColliderFrontQueue.Clear();
        }

        private void InitializeSpawner()
        {
            _roadSegmentFactory = new RoadSegmentFactory(poolSize, colliderPrefab, transform, destroyDistance);
            ColliderFrontQueue = new List<GameEntity>();
        }

        private void AddColliderToQueue(GameEntity colliderGameEntity)
        {
            if (ColliderFrontQueue.Count == colliderFrontQueueSize)
            {
                RemoveColliderFromQueue();
            }
            
            ColliderFrontQueue.Add(colliderGameEntity);
        }

        private void RemoveColliderFromQueue()
        {
            if (ColliderFrontQueue.Count != 0)
            {
                ColliderFrontQueue.RemoveAt(0);
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