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
    private Animator animator;

    private float lastAttackTime;

    private bool playerInRange = false;

    private AudioSource audioSource; // ссылка на компонент AudioSource

    public AudioClip deathSound; // добавьте сюда ваш звук смерти

    void Start()
    {
        currentHealth = maxHealth;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform; // предполагается, что у игрока тег "Player"
        lastAttackTime = -attackCooldown;

        audioSource = GetComponent<AudioSource>(); // получаем компонент AudioSource
        if (audioSource == null)
        {
            Debug.LogError("Отсутствует компонент AudioSource на враге");
        }
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
                animator.SetBool("isWalking", true);
                animator.SetBool("isAttacking", false);
            }
            else
            {
                // В пределах атаки
                animator.SetBool("isWalking", false);

                if (Time.time - lastAttackTime >= attackCooldown && playerInRange)
                {
                    Attack();
                    lastAttackTime = Time.time;
                }
            }
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }

    void Attack()
    {
        animator.SetTrigger("attack");
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
        animator.SetTrigger("die");
        
        // Воспроизведение звука смерти
        if (audioSource != null && deathSound != null)
        {
            audioSource.PlayOneShot(deathSound);
        }

        Destroy(gameObject, 1f); // удаляем объект через 2 секунды после смерти
    }

    // Методы для триггеров
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}