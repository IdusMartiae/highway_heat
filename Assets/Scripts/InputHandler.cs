using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private float verticalInputMargin = 50f;
    [SerializeField] private float sensitivity = 0.5f;
    
    public float MouseNormalizedY { get; private set; }

    public float Sensitivity => sensitivity;

    private void Update()
    {
        // TODO: you can just check for Input.GetMouseButtonDown(0)
        if (!Input.anyKey) return;
        MouseNormalizedY = GetMouseNormalizedY();
    }

    private float GetMouseNormalizedY()
    {
        var mouseY = Mathf.Clamp(Input.mousePosition.y, verticalInputMargin, Screen.height - verticalInputMargin);
        var normalizedMouseY = (mouseY - verticalInputMargin) / (Screen.height - 2 * verticalInputMargin);

        return normalizedMouseY;
    }
}