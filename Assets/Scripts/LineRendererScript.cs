using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class LineRendererScript : MonoBehaviour
{
    [SerializeField] private float lineLength = 15f;
    [SerializeField] private int pointsPerLine = 10;
    [SerializeField] private InputHandler inputHandler;

    [SerializeField] private GameConfiguration gameConfiguration;
    [SerializeField] private float verticalMax = 20f;
    [SerializeField] private float verticalMin = -20f;
    [SerializeField] private float verticalSpeed = 100f;

    [SerializeField] private List<CustomTrail> trails;

    public InputHandler InputHandler => inputHandler;
    public float VerticalSpeed => verticalSpeed;
    public float VerticalMin => verticalMin;
    public float VerticalMax => verticalMax;
    
    private List<Vector3[]> _positionsList;
    private Vector3[] _velocitiesList;

    private void Start()
    {
        InitializeLineRenderers();
        InitializeStartingPositions();
        InitializeVelocities();
        
        SetAllPositions();
    }

    private void Update()
    {
        UpdateAllPositions();
        SetAllPositions();
    }

    private void InitializeLineRenderers()
    {
        foreach (var trail in trails)
        {
            InitializeLineRenderer(trail.lineRenderer);
        }
    }

    private void InitializeStartingPositions()
    {
        _positionsList = new List<Vector3[]>();

        foreach (var trail in trails)
        {
            _positionsList.Add(InitializePositionArray(trail.lineRenderer, trail.trailOffset));
        }
    }

    private void InitializeVelocities()
    {
        _velocitiesList = new Vector3[_positionsList.Count];
        
        for (var i = 0; i < _positionsList.Count; i++)
        {
            _velocitiesList[i] = Vector3.zero;
        }
    }

    private void InitializeLineRenderer(LineRenderer lineRenderer)
    {
        lineRenderer.positionCount = pointsPerLine;
    }

    private Vector3[] InitializePositionArray(LineRenderer lineRenderer, Vector3 offset)
    {
        var startingPoint = lineRenderer.transform.position;
        var segmentLength = lineLength / (pointsPerLine - 1);

        var points = new Vector3[pointsPerLine];

        for (var i = 0; i < pointsPerLine; i++)
        {
            points[i] = new Vector3(startingPoint.x + offset.x - segmentLength * i, startingPoint.y + offset.y, startingPoint.z + offset.z);
        }

        return points;
    }

    private void SetAllPositions()
    {
        for (var i = 0; i < trails.Count; i++)
        {
            trails[i].lineRenderer.SetPositions(_positionsList[i]);
        }
    }

    private void UpdateAllPositions()
    {
        for (var i = 0; i < _positionsList.Count; i++)
        {
            MovePositionToMouse(i, trails[i].trailOffset);
        }
    }
    
    private void MovePositionToMouse(int index, Vector3 offset)
    {
        for (var i = pointsPerLine - 1; i > 0; i--)
        {
            _positionsList[index][i] = new Vector3(_positionsList[index][i].x, _positionsList[index][i - 1].y, _positionsList[index][i].z);
        }

        _positionsList[index][0] = Vector3.SmoothDamp(_positionsList[index][0], 
            GetMouseWorldCoordinates(_positionsList[index][0], offset),
            ref _velocitiesList[index], 
            inputHandler.Sensitivity,
            verticalSpeed);
    }

    private Vector3 GetMouseWorldCoordinates(Vector3 anchorPoint, Vector3 offset)
    {
        return new Vector3(anchorPoint.x + offset.x,
            (verticalMax - verticalMin) * inputHandler.MouseNormalizedY + verticalMin + offset.y,
           anchorPoint.z + offset.z);
    }
}