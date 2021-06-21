using System;
using UnityEngine;

namespace Entities
{
    [Serializable]
    public class LineRendererWrapper
    {
        public string name;
    
        public LineRenderer lineRenderer;
        public Vector3 linePosition;
        public float lineThickness;
        
        public Vector3[] GetPositionPointsWithOffset(Vector3[] initialPoints)
        {
            var length = initialPoints.Length;
            var offsetPoints = new Vector3[length];

            for (var i = 0; i < length; i++)
            {
                offsetPoints[i].Set(initialPoints[i].x + linePosition.x,
                    initialPoints[i].y + linePosition.y,
                    initialPoints[i].z + linePosition.z);
            }

            return offsetPoints;
        }
    }
}