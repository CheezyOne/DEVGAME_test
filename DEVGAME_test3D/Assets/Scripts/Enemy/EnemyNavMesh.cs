using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyNavMesh : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    public Transform Player;
    [SerializeField] private float _speed;
    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
        _navMeshAgent.speed = _speed;
    }
    private void Update()
    {
        if (Player == null)
            return;
        _navMeshAgent.destination= Player.transform.position;
    }
}
