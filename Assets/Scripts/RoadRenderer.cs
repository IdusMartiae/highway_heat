using System;
using System.Collections.Generic;
using Entities;
using UnityEngine;

public class RoadRenderer : MonoBehaviour
{
    [SerializeField] private GameConfiguration gameConfiguration;
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private float updateInterval = 0.01f;
    [SerializeField] private Transform frontPanel;
    [SerializeField] private float lineLength = 30f;
    [SerializeField] public int pointsPerLine = 25;
    [SerializeField] private List<LineRendererWrapper> lineRendererWrappers;
    [SerializeField] private Rigidbody colliderPrefab;
    [SerializeField] private int numberOfColliders;
    
    private Vector3[] _positionPoints;
    private Vector3 _velocity = Vector3.zero;
    private Rigidbody[] _roadSegments;
    private float _timer;
    
    private void OnValidate()
    {
        lineLength = Mathf.Clamp(lineLength, 0f, Single.PositiveInfinity);
        pointsPerLine = Mathf.Clamp(pointsPerLine, 0, 999);
        numberOfColliders = Mathf.Clamp(numberOfColliders, 0, pointsPerLine);
    }

    private void Awake()
    {
        InitializeLineRenderers();
        InitializePositionPoints();
        InitializeColliders();
        
        SetPositionToAllRenderers();
    }
    
    private void Update()
    {
        _timer += Time.deltaTime;
        
        if (_timer >= updateInterval)
        {
            UpdatePositionPoints();
            UpdateCollidersPosition();
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

    private void InitializeColliders()
    {
        var colliderWidth = lineLength / (pointsPerLine - 1);
        _roadSegments = new Rigidbody[numberOfColliders];

        for (var i = 0; i < numberOfColliders; i++)
        {
            InitializeCollider(i, colliderWidth);
        }
    }

    private void InitializeCollider(int index, float width)
    {
        _roadSegments[index] = Instantiate(colliderPrefab);

        var localScale = _roadSegments[index].transform.localScale;
        localScale = new Vector3(width, localScale.y, localScale.z);
        
        _roadSegments[index].transform.position = _positionPoints[index];
        _roadSegments[index].transform.localScale = localScale;
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

    private void UpdateCollidersPosition()
    {
        for (var i = 0; i < numberOfColliders; i++)
        {
            _roadSegments[i].MovePosition(_positionPoints[i]);
        }
    }

    private void UpdateFrontPanelTransform()
    {
        frontPanel.position = _positionPoints[0];
    }
}