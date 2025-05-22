using UnityEngine;
using UnityEngine.AI;

public class EnemyGuard : MonoBehaviour
{
    public Transform guardPoint; // Точка охраны
    public float patrolRadius = 5f; // Радиус патрулирования
    public float detectionRange = 10f; // Радиус обнаружения игрока
    public float attackRange = 2f; // Радиус атаки
    public float patrolSpeed = 3.5f;
    public float chaseSpeed = 5f;
    public int damageAmount = 10; // Количество урона за атаку
    public float attackCooldown = 1.5f; // Время между атаками

    private NavMeshAgent agent;
    private Transform player;
    private Vector3 patrolTarget;
    private float lastAttackTime = -Mathf.Infinity;

    private enum State { Patrolling, Chasing, Attacking }
    private State currentState = State.Patrolling;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        SetNewPatrolPoint();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        switch (currentState)
        {
            case State.Patrolling:
                Patrol();
                if (distanceToPlayer <= detectionRange)
                {
                    currentState = State.Chasing;
                }
                break;

            case State.Chasing:
                ChasePlayer();
                if (distanceToPlayer <= attackRange)
                {
                    currentState = State.Attacking;
                }
                else if (distanceToPlayer > detectionRange)
                {
                    currentState = State.Patrolling;
                    SetNewPatrolPoint();
                }
                break;

            case State.Attacking:
                AttackPlayer();
                if (distanceToPlayer > attackRange)
                {
                    currentState = State.Chasing;
                }
                break;
        }
    }

    void Patrol()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            SetNewPatrolPoint();
        }
        agent.speed = patrolSpeed;
        agent.SetDestination(patrolTarget);
    }

    void SetNewPatrolPoint()
    {
        Vector2 randomDirection = Random.insideUnitCircle * patrolRadius;
        Vector3 newTarget = guardPoint.position + new Vector3(randomDirection.x, 0, randomDirection.y);
        NavMeshHit hit;
        if (NavMesh.SamplePosition(newTarget, out hit, patrolRadius, NavMesh.AllAreas))
        {
            patrolTarget = hit.position;
        }
        else
        {
            patrolTarget = guardPoint.position; // Если не удалось найти точку — остаемся у точки охраны
        }
    }

    void ChasePlayer()
    {
        agent.speed = chaseSpeed;
        agent.SetDestination(player.position);
    }

    void AttackPlayer()
    {
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            lastAttackTime = Time.time;

            // Попытка нанести урон игроку
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
            }
            else
            {
                Debug.LogWarning("У игрока нет компонента PlayerHealth");
            }

            Debug.Log("Атака игрока! Нанесено " + damageAmount + " урона.");
        }
    }
}