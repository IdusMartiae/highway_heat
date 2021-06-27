using UnityEngine;

// TODO: namespace
public class Star : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // TODO: better use GetComponent instead of CompareTag
        // TODO: normally I would say it's better for player (i.e. it's component) to detect collision with pickups, but here functional is small enough so it's fine
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }
}
