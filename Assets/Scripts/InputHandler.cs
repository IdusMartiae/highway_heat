using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private float horizontalSpeed = 20f;
    [SerializeField] private float verticalSpeed = 10f;
    [SerializeField] private float verticalMax = 20f;
    [SerializeField] private float verticalMin = -20f;

    private void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * horizontalSpeed);

        /*var topPoint = new Vector3(transform.position.x, verticalMax, transform.position.z);
        var bottomPoint = new Vector3(transform.position.x, verticalMin, transform.position.z);*/

        if (Input.anyKey)
        {
            var yNormalized = Input.mousePosition.y / Screen.height;
            var yPoint = new Vector3(transform.position.x, (verticalMax - verticalMin) * yNormalized, 0);
            transform.position = Vector3.MoveTowards(transform.position, yPoint, Time.deltaTime * verticalSpeed);
            /*
            if (transform.position.y < verticalMax)
            {
                transform.position = Vector3.MoveTowards(transform.position, , Time.deltaTime * verticalSpeed);
            }
        }

        {
            if (transform.position.y > verticalMin)
            {
                transform.position =
                    Vector3.MoveTowards(transform.position, , Time.deltaTime * verticalSpeed);
            }*/


        }
    }
}
