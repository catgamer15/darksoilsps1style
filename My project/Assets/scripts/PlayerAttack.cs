using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int damage = 10; // урон по врагам
    public float attackRange = 2f; // радиус атаки
    public float attackCooldown = 1f; // задержка между атаками

    private float lastAttackTime;

    void Update()
    {
        if (Input.GetButtonDown("Fire1")) // по умолчанию левая кнопка мыши или Ctrl
        {
            if (Time.time - lastAttackTime >= attackCooldown)
            {
                Attack();
                lastAttackTime = Time.time;
            }
        }
    }

    void Attack()
    {
        // Находим врагов в радиусе атаки
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange);
        foreach (var collider in hitColliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                EnemyAI enemy = collider.GetComponent<EnemyAI>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                    Debug.Log("Игрок атаковал врага");
                }
            }
        }
    }

    // Визуализация радиуса атаки в редакторе
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}