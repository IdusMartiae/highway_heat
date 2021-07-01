using Configurations;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private GameConfiguration gameConfiguration;
    [SerializeField] private float verticalInputMargin = 50f;
    [SerializeField] private float sensitivity = 0.5f;
    
    public float MouseNormalizedY { get; private set; }

    public float Sensitivity => sensitivity;

    private void Update()
    {
        if (gameConfiguration.Paused) return;
        
        if (Input.GetMouseButton(0))
        {
            MouseNormalizedY = GetMouseNormalizedY();
        }
    }

    private float GetMouseNormalizedY()
    {
        var mouseY = Mathf.Clamp(Input.mousePosition.y, verticalInputMargin, Screen.height - verticalInputMargin);
        var normalizedMouseY = (mouseY - verticalInputMargin) / (Screen.height - 2 * verticalInputMargin);

        return normalizedMouseY;
    }
}