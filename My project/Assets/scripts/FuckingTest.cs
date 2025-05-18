using UnityEngine;

public class SelfDamage : MonoBehaviour
{
    public int damageAmount = 10; // количество урона, наносимого себе
    public float damageInterval = 1f; // интервал между нанесением урона (в секундах)

    private float lastDamageTime;

    void Start()
    {
        lastDamageTime = -damageInterval;
    }

    void Update()
    {
        // Например, наносим урон при удержании клавиши "Q"
        if (Input.GetKey(KeyCode.Q))
        {
            ApplySelfDamage();
        }
    }

    void ApplySelfDamage()
    {
        if (Time.time - lastDamageTime >= damageInterval)
        {
            lastDamageTime = Time.time;
            // Здесь можно вызвать метод уменьшения здоровья
            PlayerHealth playerHealth = GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
                Debug.Log("Игрок получил урон от себя");
            }
            else
            {
                Debug.LogWarning("Нет компонента PlayerHealth");
            }
        }
    }
}