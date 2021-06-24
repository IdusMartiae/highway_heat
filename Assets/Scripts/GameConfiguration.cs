using UnityEngine;

[CreateAssetMenu(menuName = "Game Configuration", fileName = "GameConfiguration")]
public class GameConfiguration : ScriptableObject
{
     // TODO: ADD HORIZONTAL SPEED TO GLOBAL GAME CONFIGURATION
     [SerializeField] private float verticalMax = 20f;
     [SerializeField] private float verticalMin = -20f;
     [SerializeField] private float verticalSpeed = 100f;
     
     public float VerticalMax => verticalMax;
     public float VerticalMin => verticalMin;
     public float VerticalSpeed => verticalSpeed;
}