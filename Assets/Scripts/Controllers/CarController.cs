using UnityEngine;

namespace Controllers
{
    public class CarController : MonoBehaviour
    {

        [SerializeField] private GameObject gameObject;
        [SerializeField] private float carOffset;
    
        void Update()
        {
            var newPosition  = new Vector3(transform.position.x, transform.position.y, gameObject.transform.position.z + carOffset);
            transform.position = newPosition;
        }
    }
}