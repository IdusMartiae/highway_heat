using System.Collections.Generic;
using Entities;
using Spawners;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [SerializeField] private RoadSegmentSpawner roadSegmentSpawner;
    [SerializeField] private float xOffset;
    [SerializeField] private float yOffset;
    [SerializeField] private float zOffset;

    private float _carX;
    private float _carY;
    private float _carZ;
    private float _carRotationX;
    
     private void Update()
     {
         if (roadSegmentSpawner.ColliderFrontQueue.Count != 0)
         {
             CalculateCarTransform(); 
             ChangeCarTransform();
         }
         
    }

     private void ChangeCarTransform()
     {
         var newX = roadSegmentSpawner.transform.position.x + xOffset;
         transform.position = new Vector3(newX, _carY, _carZ);
         transform.eulerAngles = new Vector3(_carRotationX, 90f, 0f);
     }

     private void CalculateCarTransform()
     {
         var colliders = roadSegmentSpawner.ColliderFrontQueue;
         
         CalculateAverageCoordinates(out _carX, out _carY, out _carZ, colliders);
         CalculateCarRotation(_carX, _carY, colliders);
         
         _carX += xOffset;
         _carY += yOffset;
         _carZ += zOffset;
     }

     private void CalculateAverageCoordinates(out float x, out float y, out float z, List<GameEntity> list)
     {
         x = 0f;
         y = 0f;
         z = 0f;
         
         foreach (var item in list)
         {
             var itemPosition = item.transform.position;
             
             x += itemPosition.x;
             y += itemPosition.y;
             z += itemPosition.z;
             
         }

         x /= list.Count;
         y /= list.Count;
         z /= list.Count;
         
     }

     private void CalculateCarRotation(float x, float y, List<GameEntity> list)
     {
         _carRotationX = 0f;

         foreach (var item in list)
         {
             var itemPosition = item.transform.position;
             
             _carRotationX -= (itemPosition.y - y) / (itemPosition.x - x);
    
             
         }

         _carRotationX /= list.Count;

         _carRotationX = Mathf.Atan(_carRotationX) * 180 / Mathf.PI;
     }
}
