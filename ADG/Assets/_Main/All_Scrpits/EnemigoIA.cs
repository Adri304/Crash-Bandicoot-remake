using UnityEngine;
using UnityEngine.AI;

public class EnemigoIA : MonoBehaviour
{
    public Transform jugador;

    private NavMeshAgent agent;

    public float rangoVision = 6f;
    public float rangoPatrulla = 10f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        NuevoDestino();
    }

    void Update()
    {
        float distancia = Vector3.Distance(transform.position, jugador.position);

        // PERSEGUIR
        if (distancia <= rangoVision)
        {
            agent.SetDestination(jugador.position);
        }
        // PATRULLAR
        else
        {
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                NuevoDestino();
            }
        }
    }

    void NuevoDestino()
    {
        Vector3 puntoAleatorio = Random.insideUnitSphere * rangoPatrulla;

        puntoAleatorio += transform.position;

        NavMeshHit hit;

        if (NavMesh.SamplePosition(puntoAleatorio, out hit, rangoPatrulla, 1))
        {
            agent.SetDestination(hit.position);
        }
    }
}