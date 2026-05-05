using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject objetoPrefab;
    public float tiempoEntreSpawns = 2f;
    public Vector2 rangoX = new Vector2(-2, 2);
    public Vector2 rangoZ = new Vector2(-2, 2);

    void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }

    IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            SpawnObjeto();
            yield return new WaitForSeconds(tiempoEntreSpawns);
        }
    }

    void SpawnObjeto()
    {
        float x = Random.Range(rangoX.x, rangoX.y);
        float z = Random.Range(rangoZ.x, rangoZ.y);

        Vector3 posicion = new Vector3(x, 1.01f, z); // altura sobre el plano

        Instantiate(objetoPrefab, posicion, Quaternion.identity);
    }
}