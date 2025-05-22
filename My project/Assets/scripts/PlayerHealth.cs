using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    // UI элементы
    public Slider healthSlider;        // Полоса HP
    public Text healsText;             // Текст для хилок

    // Здоровье
    public int maxHealth = 100;        // Максимальное здоровье
    private int currentHealth;

    // Хилки
    public int maxHeals = 3;           // Максимальное число хилок
    private int currentHeals;

    // Радиус действия костра
    public float campfireRadius = 3f;

    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;

        currentHeals = maxHeals;
        UpdateHealsDisplay();
    }

    void UpdateHealsDisplay()
    {
        healsText.text = "Hills: " + currentHeals;
    }

    // Метод получения урона
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;
        UpdateHealthUI();

        if (currentHealth == 0)
        {
            Debug.Log("Игрок умер");
            // Можно добавить логику смерти, перезапуск уровня и т.п.
        }
    }

    // Метод восстановления здоровья по кнопке J или при подходе к костру
    public void RestoreHealth()
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += 20; // Восстановление на 20, можно изменить
            if (currentHealth > maxHealth) currentHealth = maxHealth;
            UpdateHealthUI();
            Debug.Log("Восстановлено здоровье");
        }
    }

    // Использование хилки (например, по кнопке R)
    public void UseHeal()
    {
        if (currentHeals > 0 && currentHealth < maxHealth)
        {
            currentHeals--;
            currentHealth += 30; // Восстановление на 30, можно изменить
            if (currentHealth > maxHealth) currentHealth = maxHealth;
            UpdateHealthUI();
            UpdateHealsDisplay();
            Debug.Log("Использована хилка");
        }
        else
        {
            Debug.Log("Нет хилок или здоровье уже максимум");
        }
    }

    void UpdateHealthUI()
    {
        healthSlider.value = currentHealth;
    }

    // Восстановление при подходе к костру
    public void RestAtCampfire()
    {
        currentHealth = maxHealth;
        currentHeals = maxHeals;
        UpdateHealthUI();
        UpdateHealsDisplay();
        Debug.Log("Восстановлено здоровье и хилки");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            TakeDamage(20);
            
        }
        else if (other.CompareTag("Campfire"))
        {
            RestAtCampfire();
        }
    }

    private void Update()
    {
        // Восстановление при подходе к костру на радиусе
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, campfireRadius);
        bool nearCampfire = false;

        foreach (var collider in hitColliders)
        {
            if (collider.CompareTag("Campfire"))
            {
                nearCampfire = true;
                break;
            }
        }

        if (nearCampfire)
        {
            RestAtCampfire();
        }

        // Восстановление здоровья по клавише J
        if (Input.GetKeyDown(KeyCode.J))
        {
            RestoreHealth();
        }

        // Для теста: нажмите R чтобы использовать хилку вручную
        if (Input.GetKeyDown(KeyCode.R))
        {
            UseHeal();
        }
    }
}