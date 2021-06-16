using System.Collections.Generic;
using UnityEngine;

public class RoadRenderer : MonoBehaviour
{
    [SerializeField] private GameConfiguration gameConfiguration;
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private float lineLength = 30f;
    [SerializeField] private int pointsPerLine = 25;
    [SerializeField] private List<LineRendererWrapper> lineRendererWrappers;

    private Vector3[] _positionPoints;
    private Vector3 _velocity;

    private void Awake()
    {
        InitializeFields();
        SetPositionToAllRenderers();
    }

    private void Update()
    {
       UpdatePositionPoints();
       SetPositionToAllRenderers();
    }

    private void InitializeFields()
    {
        _velocity = Vector3.zero;
        InitializeLineRenderers();
        InitializePositionPoints();
    }

    private void InitializeLineRenderers()
    {
        foreach (var wrapper in lineRendererWrappers)
        {
            InitializeLineRenderer(wrapper);
        }
    }

    private void InitializeLineRenderer(LineRendererWrapper wrapper)
    {
        wrapper.lineRenderer.positionCount = pointsPerLine;
        wrapper.lineRenderer.startWidth = wrapper.lineThickness;
        wrapper.lineRenderer.endWidth = wrapper.lineThickness;
    }

    private void InitializePositionPoints()
    {
        _positionPoints = new Vector3[pointsPerLine];

        var startingPoint = transform.position;
        var segmentLength = lineLength / (pointsPerLine - 1);

        for (var i = 0; i < pointsPerLine; i++)
        {
            _positionPoints[i] = new Vector3(startingPoint.x - segmentLength * i, startingPoint.y, startingPoint.z);
        }
    }

    private void SetPositionToAllRenderers()
    {
        foreach (var lineRendererWrapper in lineRendererWrappers)
        {
            var offset = lineRendererWrapper.lineOffset;
            lineRendererWrapper.lineRenderer.SetPositions(GetPositionPointsWithOffset(offset));
        }
    }

    private Vector3[] GetPositionPointsWithOffset(Vector3 offset)
    {
        var offsetPositionPoints = new Vector3[_positionPoints.Length];

        for (var i = 0; i < _positionPoints.Length; i++)
        {
            offsetPositionPoints[i] = new Vector3(_positionPoints[i].x + offset.x,
                _positionPoints[i].y + offset.y,
                _positionPoints[i].z + offset.z);
        }

        return offsetPositionPoints;
    }

    private void UpdatePositionPoints()
    {
        for (var i = pointsPerLine - 1; i > 0; i--)
        {
            _positionPoints[i] = new Vector3(_positionPoints[i].x, _positionPoints[i - 1].y, _positionPoints[i].z);
        }

        var point = Input.anyKey
            ? GetMouseWorldCoordinates(_positionPoints[0])
            : _positionPoints[0];

        _positionPoints[0] = Vector3.SmoothDamp(_positionPoints[0],
            point,
            ref _velocity,
            inputHandler.Sensitivity,
            gameConfiguration.VerticalSpeed);
    }

    private Vector3 GetMouseWorldCoordinates(Vector3 anchorPoint)
    {
        return new Vector3(anchorPoint.x,
            (gameConfiguration.VerticalMax - gameConfiguration.VerticalMin) * inputHandler.MouseNormalizedY +
            gameConfiguration.VerticalMin,
            anchorPoint.z);
    }
}