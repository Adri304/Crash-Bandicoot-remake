using UnityEngine;

public class Enemigo : MonoBehaviour
{
    private void OnDestroy()
    {
        SpawnerNavMesh.enemigosActuales--;
    }
}