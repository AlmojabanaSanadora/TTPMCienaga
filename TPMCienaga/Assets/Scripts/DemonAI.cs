using UnityEngine;
using UnityEngine.AI;

public class DemonAI : MonoBehaviour
{
    public Transform player; // El jugador a perseguir
    public float[] speedByLevel = { 3.5f, 4.5f, 5.5f, 6.5f, 8f }; // Velocidad por nivel de agresividad

    private int aggressionLevel = 0; // Nivel actual de agresividad (0 a 4)
    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        UpdateAggression(); // Inicializar con nivel 0
    }

    private void Update()
    {
        // Siempre seguir al jugador
        if (player != null)
            agent.SetDestination(player.position);
    }

    public void IncreaseAggression()
    {
        aggressionLevel = Mathf.Clamp(aggressionLevel + 1, 0, speedByLevel.Length - 1);
        UpdateAggression();
    }

    private void UpdateAggression()
    {
        agent.speed = speedByLevel[aggressionLevel];
        // Aquí puedes añadir sonidos, partículas, o animaciones para mostrar que se vuelve más peligroso
        Debug.Log($"Nivel de agresividad del demonio: {aggressionLevel + 1}. Velocidad: {agent.speed}");
    }
}
