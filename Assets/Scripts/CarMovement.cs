using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [SerializeField] private GameObject gameObject;
    [SerializeField] private float xOffset;
    [SerializeField] private float zOffset;
    
    void Update()
    {
        var newPosition = new Vector3(
            gameObject.transform.position.x + xOffset,
            transform.position.y,
            gameObject.transform.position.z + zOffset
        );
        transform.position = newPosition;
    }
}
