using System;
using System.Collections.Generic;
using Entities;
using UnityEngine;

public class RoadRenderer : MonoBehaviour
{
    [SerializeField] private GameConfiguration gameConfiguration;
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private Transform frontPanel;

    [SerializeField] private float lineLength = 30f;
    [SerializeField] public int pointsPerLine = 25;
    [SerializeField] private List<LineRendererWrapper> lineRendererWrappers;

    [SerializeField] private int numberOfColliders;
    [SerializeField] private Rigidbody roadSegment;

    private Vector3[] _positionPoints;
    private Vector3 _velocity;
    private Rigidbody[] _roadSegments;

    private void OnValidate()
    {
        lineLength = Mathf.Clamp(lineLength, 0f, Single.PositiveInfinity);
        pointsPerLine = Mathf.Clamp(pointsPerLine, 0, 999);
        numberOfColliders = Mathf.Clamp(numberOfColliders, 0, pointsPerLine);
    }

    private void Awake()
    {
        // TODO: move all methods invocations here so it's more visible
        InitializeAll();
    }
    
    // TODO: let's introduce interval counter and move this logic to Update()
    private void FixedUpdate()
    {
        // TODO: move all methods invocations here so it's more visible
        UpdateAll();
    }

    private void InitializeAll()
    {
        _velocity = Vector3.zero;

        InitializeLineRenderers();
        InitializePositionPoints();
        InitializeColliders();

        SetPositionToAllRenderers();
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
        _roadSegments[index] = Instantiate(roadSegment);

        var localScale = _roadSegments[index].transform.localScale;
        localScale = new Vector3(width, localScale.y, localScale.z);
        _roadSegments[index].transform.localScale = localScale;

        SetColliderPosition(index);
    }

    // TODO: make 4 line method for 1 line logic kinda useless
    private void SetColliderPosition(int index)
    {
        _roadSegments[index].transform.position = _positionPoints[index];
    }

    private void SetPositionToAllRenderers()
    {
        foreach (var lineRenderer in lineRendererWrappers)
        {
            lineRenderer.lineRenderer.SetPositions(GetPositionPointsWithOffset(lineRenderer));
        }
    }

    // TODO: calculation might be performed in wrapper itself
    private Vector3[] GetPositionPointsWithOffset(LineRendererWrapper wrapper)
    {
        var offsetPoints = new Vector3[pointsPerLine];

        for (var i = 0; i < pointsPerLine; i++)
        {
            offsetPoints[i].Set(_positionPoints[i].x + wrapper.linePosition.x,
                _positionPoints[i].y + wrapper.linePosition.y,
                _positionPoints[i].z + wrapper.linePosition.z);
        }

        return offsetPoints;
    }

    private void UpdateAll()
    {
        UpdatePositionPoints();
        UpdateCollidersPosition();
        UpdateFrontPanelTransform();

        SetPositionToAllRenderers();
    }

    private void UpdatePositionPoints()
    {
        for (var i = pointsPerLine - 1; i > 0; i--)
        {
            // TODO: why not like this: _positionPoints[i].y = _positionPoints[i - 1].y; ?
            _positionPoints[i].Set(_positionPoints[i].x, _positionPoints[i - 1].y, _positionPoints[i].z);
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