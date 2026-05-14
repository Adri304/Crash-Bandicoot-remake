using UnityEngine;
using UnityEngine.AI;

public class SpawnerEnemigos : MonoBehaviour
{
    [Header("Prefab del enemigo")]
    public GameObject enemigoPrefab;

    [Header("Cantidad máxima")]
    public int maxEnemigos = 5;

    [Header("Tiempo entre spawns")]
    public float tiempoSpawn = 5f;

    [Header("Radio del spawn")]
    public float radioSpawn = 20f;

    private float contador;

    void Update()
    {
        contador += Time.deltaTime;

        if (contador >= tiempoSpawn)
        {
            contador = 0;

            if (GameObject.FindGameObjectsWithTag("Enemigo").Length < maxEnemigos)
            {
                SpawnEnemigo();
            }
        }
    }

    void SpawnEnemigo()
    {
        Vector3 posicionAleatoria = Random.insideUnitSphere * radioSpawn;

        posicionAleatoria += transform.position;

        NavMeshHit hit;

        if (NavMesh.SamplePosition(posicionAleatoria, out hit, radioSpawn, NavMesh.AllAreas))
        {
            Instantiate(enemigoPrefab, hit.position, Quaternion.identity);
        }
    }
}