using UnityEngine;

namespace Controllers
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float speedX = 20f; // TODO: use [SerializeField] with public Properties
        [SerializeField] private float speedY = 10f;
        [SerializeField] private float minY = -20f;
        [SerializeField] private float maxY = 20f;
    
        private void Update()
        {
            var playerDelta = new Vector3(Time.deltaTime * speedX, Input.GetAxis("Vertical") * speedY * Time.deltaTime, 0);
            transform.Translate(playerDelta);
        
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, minY, maxY), transform.position.z);
        }
    }
}