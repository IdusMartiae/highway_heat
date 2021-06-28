using UnityEngine;

namespace Entities
{
    public class Star : MonoBehaviour
    {
        public void Deinitialize()
        {
            gameObject.SetActive(false);
        }
    }
}