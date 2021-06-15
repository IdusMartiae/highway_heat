using UnityEngine;

public class RoadMovement : MonoBehaviour
{
    [SerializeField] private float horizontalSpeed = 30f;
    [SerializeField] private float verticalSpeed = 100f;
    [SerializeField] private float verticalMax = 20f;
    [SerializeField] private float verticalMin = -20f;
    [SerializeField] private InputHandler _inputHandler;

    private Vector3 _velocity;

    private void Awake()
    {
        _velocity = Vector3.zero;
    }

    private void Update()
    {
        MoveRoadBox();
        if (!Input.anyKey) return;
        MoveToMousePosition();
    }

    private void MoveRoadBox()
    {
        transform.Translate(Vector3.right * (Time.deltaTime * horizontalSpeed));
    }

    private void MoveToMousePosition()
    {
        var worldPoint = new Vector3(transform.position.x,
            ((verticalMax - verticalMin) * _inputHandler.MouseNormalizedY) + verticalMin, 0);

        transform.position = Vector3.SmoothDamp(
            transform.position,
            worldPoint,
            ref _velocity,
            _inputHandler.Sensitivity,
            verticalSpeed);
    }
}