using UnityEngine;

namespace Configurations
{
     [CreateAssetMenu(menuName = "Input Configuration", fileName = "InputConfiguration")]
     public class InputConfiguration : ScriptableObject
     {
          [SerializeField] private float verticalMax = 20f;
          [SerializeField] private float verticalMin = -20f;
          [SerializeField] private float verticalSpeed = 100f;
          
          public float VerticalMax => verticalMax;
          public float VerticalMin => verticalMin;
          public float VerticalSpeed => verticalSpeed;
      
     }
}