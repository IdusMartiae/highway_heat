using UnityEngine;

public class RoadMovement : MonoBehaviour
{
    [SerializeField] private float speedX = 20f; // TODO: use [SerializeField] with public Properties
    [SerializeField] private float speedY = 10f;
    [SerializeField] private float minY = -20f;
    [SerializeField] private float maxY = 20f;

    private void Update()
    {
        var playerDelta = new Vector3(Time.deltaTime * speedX, Input.GetAxis("Vertical") * speedY * Time.deltaTime, 0);
        transform.Translate(playerDelta);

        var roadPosition = transform.position;
        transform.position = new Vector3(roadPosition.x, Mathf.Clamp(roadPosition.y, minY, maxY), roadPosition.z);
    }
}