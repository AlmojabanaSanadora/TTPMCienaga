using UnityEngine;
using UnityEngine.AI;

public class DemonAI : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform player;
    public LayerMask WhatIsGround, WhatIsPlayer;

    public Vector3 walkArea;
    public float walkPointRadius, sightRadius;
    bool walkPointState;
    bool playerInSightRadius;

    public float[] speedByLevel = { 3.5f, 4.5f, 5.5f, 6.5f, 8f }; // Velocidad por nivel de agresividad

    private int aggressionLevel = 0; // Nivel actual de agresividad (0 a 4)

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        agent = GetComponent<NavMeshAgent>();
        UpdateAggression(); // Inicializar con nivel 0
    }

    private void Update()
    {
        playerInSightRadius = Physics.CheckSphere(transform.position, sightRadius, WhatIsPlayer);

        if (playerInSightRadius)
        {
            SearchingPlayer();
        }
        else
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        if (!walkPointState) PatrolArea();
        if (walkPointState)
        {
            agent.SetDestination(walkArea);
        }

        Vector3 rangeToWalk = transform.position - walkArea;
        if (rangeToWalk.magnitude < 1f)
        {
            walkPointState = false;
        }
    }

    private void PatrolArea()
    {
        float randomX = Random.Range(-walkPointRadius, walkPointRadius);
        float randomZ = Random.Range(-walkPointRadius, walkPointRadius);

        walkArea = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkArea, -transform.up, 2f, WhatIsGround))
            walkPointState = true;
    }

    private void SearchingPlayer()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new(directionToPlayer.x, 0, directionToPlayer.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * agent.angularSpeed);

        agent.SetDestination(transform.position);
    }

    public void IncreaseAggression()
    {
        aggressionLevel = Mathf.Clamp(aggressionLevel + 1, 0, speedByLevel.Length - 1);
        UpdateAggression();
    }

    private void UpdateAggression()
    {
        agent.speed = speedByLevel[aggressionLevel];
        Debug.Log($"Nivel de agresividad del demonio: {aggressionLevel + 1}. Velocidad: {agent.speed}");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sightRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, walkPointRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 2f);

    }
}
