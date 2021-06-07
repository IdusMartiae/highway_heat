using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [SerializeField] private GameObject roadSpawnerObject;
    [SerializeField] private float xOffset;
    [SerializeField] private float zOffset;
    
     private void Update()
    {
        var newPosition = new Vector3(
            roadSpawnerObject.transform.position.x + xOffset,
            transform.position.y,
            roadSpawnerObject.transform.position.z + zOffset
        );
        transform.position = newPosition;
    }
}
