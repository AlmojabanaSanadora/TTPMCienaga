using UnityEngine;
using UnityEngine.AI;

public class DemonAI : MonoBehaviour
{
    private Animator animator;
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
        animator = GetComponent<Animator>();

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
        UpdateAnimation();
    }

    private void Patrol()
    {
        if (!walkPointState) PatrolArea();
        if (walkPointState)
        {
            agent.SetDestination(walkArea);
        }

        Vector3 rangeToWalk = transform.position - walkArea;
        if (rangeToWalk.magnitude < 4f)
        {
            walkPointState = false;
        }
    }

    private void PatrolArea()
{
    float randomX = Random.Range(-walkPointRadius, walkPointRadius);
    float randomZ = Random.Range(-walkPointRadius, walkPointRadius);

    Vector3 potentialWalkArea = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

    // Validate the position using NavMesh.SamplePosition
    NavMeshHit hit;
    if (NavMesh.SamplePosition(potentialWalkArea, out hit, 2f, NavMesh.AllAreas))
    {
        walkArea = hit.position; // Set the valid position
        walkPointState = true;
    }
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

    private void UpdateAnimation()
    {
        bool isMoving = agent.velocity.magnitude > 0.1f;
        animator.SetBool("isWalking", isMoving);
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
