using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Daño")]
    [SerializeField] private float damage = 10f;

    [Header("Tiempo")]
    [SerializeField] private float lifeTime = 3f;

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        CancelInvoke();

        Invoke(nameof(Desactivar), lifeTime);
    }

    private void OnDisable()
    {
        CancelInvoke();

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            // Dormimos física mientras está guardada
            rb.isKinematic = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerPowerUps player = other.GetComponent<PlayerPowerUps>();

        if (player != null)
        {
            player.RecibirDanio(damage);

            Debug.Log("Bala golpeó jugador");
        }

        Desactivar();
    }

    void Desactivar()
    {
        gameObject.SetActive(false);
    }
}