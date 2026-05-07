using UnityEngine;
using UnityEngine.AI;

public class Enemigo1 : MonoBehaviour
{
    // REFERENCIAS
    public Transform target;
    public NavMeshAgent agent;
    public Animator ani;

    // DISTANCIAS
    public float rangoVision = 5f;
    public float rangoPatrulla = 10f;

    // TIEMPO
    public float tiempoEspera = 4f;
    private float cronometro;

    // ESTADO
    private bool persiguiendo;

    void Start()
    {
        // OBTENER COMPONENTES
        agent = GetComponent<NavMeshAgent>();
        ani = GetComponent<Animator>();

        // BUSCAR JUGADOR
        target = GameObject.Find("Link").transform;

        // PRIMER PUNTO ALEATORIO
        NuevoPunto();
    }

    void Update()
    {
        float distancia = Vector3.Distance(transform.position, target.position);

        // SI EL JUGADOR ESTÁ CERCA
        if (distancia <= rangoVision)
        {
            PersseguirJugador();
        }
        else
        {
            Patrullar();
        }

        Animaciones();
    }

    void PersseguirJugador()
    {
        persiguiendo = true;

        // MÁS VELOCIDAD AL CORRER
        agent.speed = 3.5f;

        // PERSEGUIR
        agent.SetDestination(target.position);
    }

    void Patrullar()
    {
        if (persiguiendo)
        {
            persiguiendo = false;
            NuevoPunto();
        }

        // VELOCIDAD NORMAL
        agent.speed = 2f;

        // SI YA LLEGÓ AL DESTINO
        if (!agent.pathPending && agent.remainingDistance <= 0.5f)
        {
            cronometro += Time.deltaTime;

            if (cronometro >= tiempoEspera)
            {
                NuevoPunto();
                cronometro = 0;
            }
        }
    }

    void NuevoPunto()
    {
        // POSICIÓN ALEATORIA
        Vector3 randomDirection = Random.insideUnitSphere * rangoPatrulla;

        randomDirection += transform.position;

        NavMeshHit hit;

        // BUSCAR PUNTO VÁLIDO EN EL NAVMESH
        if (NavMesh.SamplePosition(randomDirection, out hit, rangoPatrulla, 1))
        {
            Vector3 finalPosition = hit.position;

            agent.SetDestination(finalPosition);
        }
    }

    void Animaciones()
    {
        // SI SE ESTÁ MOVIENDO
        if (agent.velocity.magnitude > 0.1f)
        {
            ani.SetBool("walk", !persiguiendo);
            ani.SetBool("run", persiguiendo);
        }
        else
        {
            ani.SetBool("walk", false);
            ani.SetBool("run", false);
        }
    }
}