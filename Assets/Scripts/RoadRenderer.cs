using System.Collections.Generic;
using Entities;
using UnityEngine;
using static System.Single;

public class RoadRenderer : MonoBehaviour
{
    [SerializeField] private GameConfiguration gameConfiguration;
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private float updateInterval = 0.01f;
    [SerializeField] private Transform frontPanel;
    [SerializeField] private float lineLength = 30f;
    [SerializeField] public int pointsPerLine = 25;
    [SerializeField] private List<LineRendererWrapper> lineRendererWrappers;

    private Vector3[] _positionPoints;
    private Vector3 _velocity = Vector3.zero;
    private float _timer;
    
    private void OnValidate()
    {
        lineLength = Mathf.Clamp(lineLength, 0f, PositiveInfinity);
        pointsPerLine = Mathf.Clamp(pointsPerLine, 0, int.MaxValue);
    }

    private void Awake()
    {
        InitializeLineRenderers();
        InitializePositionPoints();

        SetPositionToAllRenderers();
    }
    
    private void Update()
    {
        _timer += Time.deltaTime;
        
        if (_timer >= updateInterval)
        {
            UpdatePositionPoints();
            UpdateFrontPanelTransform();

            SetPositionToAllRenderers();
            
            _timer = 0;
        }
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
        wrapper.lineRenderer.transform.position = wrapper.linePosition;
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
        foreach (var wrapper in lineRendererWrappers)
        {
            wrapper.lineRenderer.SetPositions(wrapper.GetPositionPointsWithOffset(_positionPoints));
        }
    }
    
    private void UpdatePositionPoints()
    {
        for (var i = pointsPerLine - 1; i > 0; i--)
        {
            _positionPoints[i].y = _positionPoints[i - 1].y;
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
    
    private void UpdateFrontPanelTransform()
    {
        frontPanel.position = _positionPoints[0];
    }
}