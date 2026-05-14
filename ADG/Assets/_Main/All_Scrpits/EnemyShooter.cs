using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;

    [Header("Jugador")]
    [SerializeField] private Transform player;

    [Header("Disparo")]
    [SerializeField] private float detectionRange = 10f;
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float bulletSpeed = 40f;

    private float timer;

    private void Update()
    {
        if (player == null)
            return;

        // Distancia al jugador
        float distance = Vector3.Distance(
            transform.position,
            player.position
        );

        // Si está lejos NO dispara
        if (distance > detectionRange)
            return;

        // Mirar al jugador
        Vector3 direction =
            player.position - transform.position;

        direction.y = 0f;

        transform.rotation =
            Quaternion.LookRotation(direction);

        // Temporizador
        timer += Time.deltaTime;

        if (timer >= fireRate)
        {
            Shoot();
            timer = 0f;
        }
    }

    private void Shoot()
    {
        Debug.Log("DISPARANDO");

        GameObject bullet = Instantiate(
            bulletPrefab,
            firePoint.position,
            firePoint.rotation
        );

        Rigidbody rb =
            bullet.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.linearVelocity =
                firePoint.forward * bulletSpeed;
        }

        Destroy(bullet, 5f);
    }

    // SOLO para ver el rango en escena
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(
            transform.position,
            detectionRange
        );
    }
}