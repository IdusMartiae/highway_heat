using UnityEngine;

public class ParallaxScript : MonoBehaviour
{
    [SerializeField] private float layerSpeed;

    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        transform.Translate(Vector3.left * (layerSpeed * Time.deltaTime));
        
        if (_rectTransform.anchoredPosition.x <= -_rectTransform.rect.width)
        {
            _rectTransform.anchoredPosition = Vector2.zero;
        }
    }
    
    
}
