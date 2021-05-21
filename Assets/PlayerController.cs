using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speedX = 10f;

    public float speedY = 5f;

    public float minY;
    
    public float maxY;

    public Camera camera;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Time.deltaTime * speedX, Input.GetAxis("Vertical") * speedY * Time.deltaTime, 0);
        camera.transform.position = new Vector3(transform.position.x - 15, 1, transform.position.z - 5);
        
        // camera.transform.Translate(Vector3.right * (Time.deltaTime * speedX)); - почему не работает?
        // transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, minY, maxY), transform.position.z);
    }
}
