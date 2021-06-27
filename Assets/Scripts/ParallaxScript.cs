using UnityEngine;

// TODO: don't you Script in script's name
// TODO: let's try make parallax effect via material offset
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
        var pos = _rectTransform.anchoredPosition;
        var destPoint = new Vector2(pos.x - 2000, pos.y);
        
        _rectTransform.anchoredPosition = Vector2.Lerp(pos, destPoint, layerSpeed / 1000);
        
        if (_rectTransform.anchoredPosition.x <= -_rectTransform.rect.width)
        {
            var yPosition = _rectTransform.anchoredPosition.y;
            _rectTransform.anchoredPosition = new Vector2(0, yPosition);
        }
    }
}
