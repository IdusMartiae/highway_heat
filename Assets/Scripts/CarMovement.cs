using System.Linq;
using Spawners;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [SerializeField] private RoadSegmentSpawner roadSegmentSpawner;
    [SerializeField] private float xOffset;
    [SerializeField] private float yOffset;
    [SerializeField] private float zOffset;

    private void FixedUpdate()
    {
        if (roadSegmentSpawner.CapturedColliderQueue.Count < 2) return;
        ChangeCarTransform();
    }

    private void ChangeCarTransform()
    {
        transform.position = GetNewPositionVector();
        transform.eulerAngles = new Vector3(GetXAxisAngle(), 90, 0);
    }

    private Vector3 GetNewPositionVector()
    {
        var frontPoint = roadSegmentSpawner.CapturedColliderQueue.Last().transform.position;
        var backPoint = roadSegmentSpawner.CapturedColliderQueue[0].transform.position;

        var newX = (frontPoint.x + backPoint.x) / 2 + xOffset;
        // Fix car clipping through the road if xOffset != 0
        var newY = (frontPoint.y + backPoint.y) / 2 + yOffset - xOffset * Mathf.Tan(GetXAxisAngle() / 180 * Mathf.PI);
        var newZ = zOffset;

        return new Vector3(newX, newY, newZ);
    }

    private float GetXAxisAngle()
    {
        var frontPoint = roadSegmentSpawner.CapturedColliderQueue.Last().transform.position;
        var backPoint = roadSegmentSpawner.CapturedColliderQueue[0].transform.position;

        var deltaX = frontPoint.x - backPoint.x;
        var deltaY = frontPoint.y - backPoint.y;

        var xAxisAngle = -Mathf.Atan2(deltaY, deltaX) * 180 / Mathf.PI;
        return xAxisAngle;
    }
}