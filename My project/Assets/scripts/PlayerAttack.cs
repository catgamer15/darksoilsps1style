using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int attackDamage = 20; // урон за атаку
    public float attackRange = 2f; // радиус атаки
    public float attackCooldown = 1f; // задержка между атаками

    private float lastAttackTime;

    void Start()
    {
        lastAttackTime = -attackCooldown;
    }

    void Update()
    {
        // Например, атака по нажатию клавиши Space
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    void Attack()
    {
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            lastAttackTime = Time.time;

            // Находим врагов в радиусе атаки
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange);
            foreach (var collider in hitColliders)
            {
                if (collider.CompareTag("Enemy"))
                {
                    // Наносим урон врагу
                    EnemyAI enemy = collider.GetComponent<EnemyAI>();
                    if (enemy != null)
                    {
                        enemy.TakeDamage(attackDamage);
                        Debug.Log("Игрок нанес урон врагу");
                    }
                }
            }
        }
    }

    // Для визуализации радиуса атаки в редакторе
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}