using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;

    [Header("Configuración")]
    [SerializeField] private float fireRate = 2f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= fireRate)
        {
            Disparar();
            timer = 0f;
        }
    }

    void Disparar()
    {
        if (bulletPrefab == null || firePoint == null) return;

        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}