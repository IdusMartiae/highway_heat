using System.Collections.Generic;
using Configurations;
using Entities.Wrappers;
using UnityEngine;
using static System.Single;

public class RoadRenderer : MonoBehaviour
{
    [SerializeField] private GameConfiguration gameConfiguration;
    [SerializeField] private InputConfiguration inputConfiguration;
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private float waveResponseTime = 0.01f;
    [SerializeField] private float roadTextureSpeed = 5f;
    [SerializeField] private Transform frontPanel;
    [SerializeField] private float lineLength = 30f;
    [SerializeField] public int pointsPerLine = 25;
    [SerializeField] private List<LineRendererWrapper> lineRendererWrappers;
    [SerializeField] private float velocityClampThreshold = 0.001f;
    
    public Vector3[] PositionPoints => _positionPoints;

    private Vector3[] _positionPoints;
    private Vector3[] _pointVelocities;
    private Vector2 _textureOffset = Vector2.zero;

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
        SetPositionToAllRenderers();
        UpdateFrontPanelTransform();
        UpdateRoadTexture();
    }

    private void FixedUpdate()
    {
        UpdatePositionPoints();
    }

    private void InitializeLineRenderers()
    {
        foreach (var wrapper in lineRendererWrappers)
        {
            InitializeLineRenderer(wrapper);
        }
    }

    // TODO: WRAPPER CAN INITIALIZE ITSELF BY INVOKING WRAPPER'S METHOD AND PASSING REQUIRED PARAMETERS TO IT
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
        _pointVelocities = new Vector3[pointsPerLine];

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
        var pointNewPosition = new Vector3();

        for (var i = pointsPerLine - 1; i > 0; i--)
        {
            pointNewPosition.Set(_positionPoints[i].x, _positionPoints[i - 1].y, _positionPoints[i].z);
            _positionPoints[i] = Vector3.SmoothDamp(_positionPoints[i],
                pointNewPosition,
                ref _pointVelocities[i],
                waveResponseTime);
        }
        
        pointNewPosition = Input.GetMouseButton(0) && !gameConfiguration.Paused
            ? GetMouseWorldCoordinates(_positionPoints[0])
            : _positionPoints[0];

        _positionPoints[0] = Vector3.SmoothDamp(_positionPoints[0],
            pointNewPosition,
            ref _pointVelocities[0],
            inputHandler.Sensitivity,
            inputConfiguration.VerticalSpeed);
    }

    private Vector3 GetMouseWorldCoordinates(Vector3 anchorPoint)
    {
        return new Vector3(anchorPoint.x,
            (inputConfiguration.VerticalMax - inputConfiguration.VerticalMin) * inputHandler.MouseNormalizedY +
            inputConfiguration.VerticalMin,
            anchorPoint.z);
    }

    private void UpdateFrontPanelTransform()
    {
        frontPanel.position = _positionPoints[0];
    }

    private void UpdateRoadTexture()
    {
        _textureOffset.Set(-(Time.time * roadTextureSpeed % 5), 0);
        lineRendererWrappers[0].lineRenderer.material.mainTextureOffset = _textureOffset;
    }

    public float GetPointVerticalVelocity(int index)
    {
        var velocity = _pointVelocities[index].y;
        return Mathf.Abs(velocity) < velocityClampThreshold ? 0 : velocity * 10;
    }

    public float GetRoadThickness()
    {
        return lineRendererWrappers[1].lineThickness;
    }
}