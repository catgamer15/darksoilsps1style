using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public int maxHealth = 20;
    private int currentHealth;

    public float attackRange = 1.5f; // радиус атаки
    public int damage = 20; // урон по игроку
    public float attackCooldown = 1.5f; // задержка между атаками

    private NavMeshAgent agent;
    private Transform player;

    private float lastAttackTime;

    void Start()
    {
        currentHealth = maxHealth;
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform; // предполагается, что у игрока тег "Player"
        lastAttackTime = -attackCooldown;
    }

    void Update()
    {
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance > attackRange)
            {
                // Идти к игроку
                agent.SetDestination(player.position);
            }
            else
            {
                // В пределах атаки
                if (Time.time - lastAttackTime >= attackCooldown)
                {
                    Attack();
                    lastAttackTime = Time.time;
                }
            }
        }
    }

    void Attack()
    {
        // Предполагается, что у игрока есть скрипт PlayerHealth с методом TakeDamage(int)
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
            Debug.Log("Враг атаковал игрока");
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Враг убит");
        Destroy(gameObject);
    }
}