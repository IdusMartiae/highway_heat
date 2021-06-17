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
    }
}