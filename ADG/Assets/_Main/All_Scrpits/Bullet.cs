using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float damage = 10f;

    private void OnTriggerEnter(Collider other)
    {
        PlayerPowerUps player =
            other.GetComponent<PlayerPowerUps>();

        if (player != null)
        {
            player.RecibirDanio(damage);
        }

        Destroy(gameObject);
    }
}