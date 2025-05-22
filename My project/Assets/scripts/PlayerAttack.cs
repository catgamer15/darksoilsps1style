using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int damage = 20; // урон игрока
    public float attackRange = 4f; // радиус атаки
    public KeyCode attackKey = KeyCode.Mouse0; // клавиша для атаки

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    void Attack()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange);
        foreach (var collider in hitColliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                Enemy enemyScript = collider.GetComponent<Enemy>();
                if (enemyScript != null)
                {
                    enemyScript.TakeDamage(damage);
                    Debug.Log("Игрок атаковал врага");
                }
            }
        }
    }
}