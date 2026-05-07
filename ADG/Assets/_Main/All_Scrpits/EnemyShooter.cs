using UnityEngine;
using System.Collections.Generic;

public class EnemyShooter : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;

    [Header("Disparo")]
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float bulletSpeed = 180f;

    [Header("Pool")]
    [SerializeField] private int poolSize = 20;

    private float timer;

    // Lista de balas reciclables
    private List<GameObject> bulletPool = new List<GameObject>();

    void Start()
    {
        CrearPool();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= fireRate)
        {
            Disparar();
            timer = 0f;
        }
    }

    void CrearPool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);

            // Desactivamos la bala
            bullet.SetActive(false);

            // Desactivamos física mientras está guardada
            Rigidbody rb = bullet.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.isKinematic = true;
            }

            bulletPool.Add(bullet);
        }
    }

    GameObject ObtenerBala()
    {
        for (int i = 0; i < bulletPool.Count; i++)
        {
            if (!bulletPool[i].activeInHierarchy)
            {
                return bulletPool[i];
            }
        }

        return null;
    }

    void Disparar()
    {
        GameObject bullet = ObtenerBala();

        if (bullet == null)
        {
            Debug.Log("No hay balas disponibles");
            return;
        }

        bullet.transform.position = firePoint.position;
        bullet.transform.rotation = firePoint.rotation;

        bullet.SetActive(true);

        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        if (rb != null)
        {
            // Activamos física
            rb.isKinematic = false;

            // Reiniciamos velocidades viejas
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            Vector3 direccion = firePoint.forward;

            // pequeña imperfección
            direccion.x += Random.Range(-0.01f, 0.01f);
            direccion.y += Random.Range(-0.01f, 0.01f);

            rb.linearVelocity = direccion.normalized * bulletSpeed;
        }
    }
}