using System.Collections.Generic;
using System.Linq;
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
    
     private void FixedUpdate()
     {
         CalculateCarTransform(); 
         ChangeCarTransform();
    }

     private void ChangeCarTransform()
     {
         var newX = roadSegmentSpawner.transform.position.x + xOffset;
         transform.position = new Vector3(newX, _carY, _carZ);
        
     }

     private void CalculateCarTransform()
     {
         var colliders = roadSegmentSpawner.ColliderFrontQueue;
         
         CalculateAverageCoordinates(out _carX, out _carY, out _carZ, colliders);
         CalculateCarRotation(colliders);
         
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

     private void CalculateCarRotation(List<GameEntity> list)
     {
         var firstItem = list[0];
         var lastItem = list.Last();

         var deltaX = lastItem.transform.position.x - firstItem.transform.position.x;
         var deltaY = lastItem.transform.position.y - firstItem.transform.position.y;
         
         _carRotationX = - Mathf.Atan2(deltaY, deltaX) * 180 / Mathf.PI;
         
         /*var firstItem = list[0];
         var lastItem = list.Last();
         
         var deltaY = lastItem.transform.position.y - firstItem.transform.position.y;
         
         transform.eulerAngles = new Vector3(-deltaY*20, 90, 0);*/
     }
}
