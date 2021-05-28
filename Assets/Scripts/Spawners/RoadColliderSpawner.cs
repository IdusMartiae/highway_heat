using System;
using Entities;
using UnityEngine;

namespace Spawners
{
    public class RoadColliderSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject colliderPrefab;
        [SerializeField] private int poolSize = 100;
        
        private ColliderFactory colliderFactory;

        public void Awake()
        {
            colliderFactory = new ColliderFactory(poolSize, colliderPrefab);
        }

        public void Update()
        {
            Instantiate(colliderFactory.Create(), transform.position, Quaternion.Euler(0, 90, 0));
        }
        
    }
}