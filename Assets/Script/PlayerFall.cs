using UnityEngine;

public class TeleportToPoint : MonoBehaviour
{
    [SerializeField] private Transform teleportPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            collision.transform.position = teleportPoint.position;
    }
}