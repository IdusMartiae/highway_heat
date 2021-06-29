using UnityEngine;

namespace Entities
{
    public class Star : MonoBehaviour
    {
        [SerializeField] private int scoreValue = 10;
        public int ScoreValue => scoreValue;
        
        public void Deinitialize()
        {
            gameObject.SetActive(false);
        }
    }
}