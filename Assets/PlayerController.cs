using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speedX = 10f; // TODO: use [SerializeField] with public Properties
    public float speedY = 5f;
    public float minY;
    public float maxY;
    public Camera camera;

    private void Update()
    {
        var playerDelta = new Vector3(Time.deltaTime * speedX, Input.GetAxis("Vertical") * speedY * Time.deltaTime, 0);
        var roadDelta = Vector3.right * (Time.deltaTime * speedX);
        
        transform.Translate(playerDelta);
        // camera.transform.position = new Vector3(transform.position.x - 15, 1, transform.position.z - 5);

        camera.transform.Translate(roadDelta); // - почему не работает?
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, minY, maxY), transform.position.z);

        Debug.Log($"playerDelta = {playerDelta.x}, roadDelta = {roadDelta.x}");
    }
}