using UnityEngine;

namespace Entities
{
    public class Parallax : MonoBehaviour
    {
        [SerializeField] private float layerSpeed;

        private Material _material;
        private Vector2 _textureOffset;

        private void Awake()
        {
            _material = GetComponent<MeshRenderer>().material;
            _textureOffset = _material.mainTextureOffset;
        }

        private void Update()
        {
            _textureOffset.Set((Time.time * layerSpeed / 100) % 1, 0);
            _material.mainTextureOffset = _textureOffset;
        }
    }
}