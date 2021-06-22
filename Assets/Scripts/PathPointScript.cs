using UnityEngine;

public class PathPointScript : MonoBehaviour
{
    [SerializeField] private RoadRenderer roadRenderer;

    private int _stickIndex;
    private float _yOffset;
    private float _angle;
    
    private void Start()
    {
        StickToRoad();
    }

    private void LateUpdate()
    {
        UpdatePositionWithOffset();
    }

    private void StickToRoad()
    {
        _stickIndex = 0;
        var pointPosition = transform.position;
        
        for (var i = 1; i < roadRenderer.PositionPoints.Length; i++)
        {
            var stickDistance = Vector3.Distance(pointPosition, roadRenderer.PositionPoints[_stickIndex]);
            var currentDistance = Vector3.Distance(pointPosition, roadRenderer.PositionPoints[i]);

            if (currentDistance < stickDistance)
            {
                _stickIndex = i;
            }
            else
            {
                _yOffset = pointPosition.y - roadRenderer.PositionPoints[_stickIndex].y;
                return;
            }
        }

        
    }

    private void UpdatePositionWithOffset()
    {

        
        transform.position = new Vector3(transform.position.x,
            roadRenderer.PositionPoints[_stickIndex].y + _yOffset,
            transform.position.z);
    }
}
