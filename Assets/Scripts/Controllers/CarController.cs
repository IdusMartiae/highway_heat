using UnityEngine;

namespace Controllers
{
    public class CarController : MonoBehaviour
    {
        [SerializeField] private GameObject gameObject;
        [SerializeField] private float frontOffset;
        [SerializeField] private float sideOffset;
    
        void Update()
        {
            var newPosition  = new Vector3(
                gameObject.transform.position.x - sideOffset,
                transform.position.y,
                gameObject.transform.position.z - frontOffset
                );
            transform.position = newPosition;
        }
    }
}