using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class SpawnerNavMesh : MonoBehaviour
{
    public GameObject enemigoPrefab;

    public float tiempoEntreSpawns = 2f;
    public float radioSpawn = 10f;

    void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }

    IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            SpawnEnNavMesh();

            yield return new WaitForSeconds(tiempoEntreSpawns);
        }
    }

    void SpawnEnNavMesh()
    {
        Vector3 puntoAleatorio = transform.position + Random.insideUnitSphere * radioSpawn;

        puntoAleatorio.y = 5f;

        NavMeshHit hit;

        if (NavMesh.SamplePosition(puntoAleatorio, out hit, 10f, NavMesh.AllAreas))
        {
            Instantiate(enemigoPrefab, hit.position + Vector3.up * 1f, Quaternion.identity);
        }
    }
}