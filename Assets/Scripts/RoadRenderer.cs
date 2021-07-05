using System.Collections.Generic;
using Systems.UI;
using Configurations;
using Entities.Enums;
using Entities.Wrappers;
using UnityEngine;
using Zenject;
using static System.Single;

public class RoadRenderer : MonoBehaviour
{
    
    [SerializeField] private float waveResponseTime = 0.01f;
    [SerializeField] private float roadTextureSpeed = 5f;
    [SerializeField] private Transform frontPanel;
    [SerializeField] private float lineLength = 30f;
    [SerializeField] private int pointsPerLine = 25;
    [SerializeField] private List<LineRendererWrapper> lineRendererWrappers;
    [SerializeField] private float velocityClampThreshold = 0.001f;
    [SerializeField] private float idleAmplitudeMultiplier = 5f;
    
    public Vector3[] PositionPoints => _positionPoints;
    
    private GameConfiguration _gameConfiguration;
    private InputConfiguration _inputConfiguration;
    private InputHandler _inputHandler;
    private ScreenSwitch _screenSwitch;
    private Vector3[] _positionPoints;
    private Vector3[] _pointVelocities;
    private Vector2 _textureOffset = Vector2.zero;

    [Inject]
    private void Initialize(GameConfiguration gameConfiguration,
        InputConfiguration inputConfiguration,
        InputHandler inputHandler,
        ScreenSwitch screenSwitch)
    {
        _gameConfiguration = gameConfiguration;
        _inputConfiguration = inputConfiguration;
        _inputHandler = inputHandler;
        _screenSwitch = screenSwitch;

    }
    
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

        if (_gameConfiguration.Paused)
        {
            if (_screenSwitch.ActiveScreen == ScreenEnum.MainMenu)
            {
                pointNewPosition = GetNewIdlePosition(_positionPoints[0]);
            }
        }
        else
        {
            pointNewPosition = Input.GetMouseButton(0)
                ? GetMouseWorldCoordinates(_positionPoints[0])
                : _positionPoints[0];
        }

        _positionPoints[0] = Vector3.SmoothDamp(_positionPoints[0],
            pointNewPosition,
            ref _pointVelocities[0],
            _inputHandler.Sensitivity,
            _inputConfiguration.VerticalSpeed);
    }

    private Vector3 GetNewIdlePosition(Vector3 anchorPoint)
    {
        return new Vector3(anchorPoint.x,
             Mathf.Sin(Time.time) * idleAmplitudeMultiplier,
            anchorPoint.z);
    }
    
    private Vector3 GetMouseWorldCoordinates(Vector3 anchorPoint)
    {
        return new Vector3(anchorPoint.x,
            (_inputConfiguration.VerticalMax - _inputConfiguration.VerticalMin) * _inputHandler.MouseNormalizedY +
            _inputConfiguration.VerticalMin,
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