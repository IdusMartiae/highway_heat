using UnityEngine;

public class FrontPanel : MonoBehaviour
{
    private LineRendererScript _lineRendererScript;
    private Vector3 _velocity = Vector3.zero;

    private void Awake()
    {
        _lineRendererScript = transform.parent.gameObject.GetComponent<LineRendererScript>();
    }

    private void Update()
    {
        transform.eulerAngles = GetNewAngle();
        MoveFrontPanel();
    }

    private void MoveFrontPanel()
    {
        transform.position = Vector3.SmoothDamp(transform.position, 
            GetMouseWorldCoordinates(),
            ref _velocity, 
            _lineRendererScript.InputHandler.Sensitivity,
            _lineRendererScript.VerticalSpeed);
    }
    
    private Vector3 GetMouseWorldCoordinates()
    {
        var currentPosition = transform.position;
        
        return new Vector3(currentPosition.x,
            (_lineRendererScript.VerticalMax - _lineRendererScript.VerticalMin) * _lineRendererScript.InputHandler.MouseNormalizedY + _lineRendererScript.VerticalMin,
            currentPosition.z);
    }

    private Vector3 GetNewAngle()
    {
        var xRotation = GetMouseWorldCoordinates().y - transform.position.y;
        return new Vector3(- xRotation * 4, 90, 0);
    }
}