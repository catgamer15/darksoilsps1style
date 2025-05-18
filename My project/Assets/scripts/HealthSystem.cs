using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    // Максимальное здоровье
    public float maxHealth = 100f;
    // Текущее здоровье
    private float currentHealth;

    // Максимальное количество хилок
    public int maxHills = 5;
    // Текущее количество хилок
    private int currentHills;

    // UI элементы
    public Slider healthSlider;      // Полоса здоровья
    public Text hillsText;           // Текст для отображения хилок

    // Время восстановления хилок и здоровья при подходе к костру
    public float restoreAmount = 20f;   // Количество восстанавливаемого HP
    public int restoreHills = 1;        // Количество восстанавливаемых хилок

    void Start()
    {
        currentHealth = maxHealth;
        currentHills = maxHills;
        UpdateUI();
    }

    void Update()
    {
        // Для теста: нажмите H, чтобы получить урон
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(20f);
        }
        // Для теста: нажмите R, чтобы восстановить здоровье и хилки при подходе к костру
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestAtCampfire();
        }
        // Для теста: нажмите Q, чтобы использовать хилки для восстановления HP
        if (Input.GetKeyDown(KeyCode.Q))
        {
            UseHills();
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
            currentHealth = 0;

        UpdateUI();

        if (currentHealth == 0)
        {
            Debug.Log("Игрок умер");
            // Можно добавить логику смерти здесь
        }
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        UpdateUI();
    }

    public void UseHills()
    {
        if (currentHills > 0 && currentHealth < maxHealth)
        {
            currentHills--;
            Heal(20f); // Восстановление на 20 HP за одну хилку
            UpdateUI();
        }
        else
        {
            Debug.Log("Нет хилок или здоровье уже полное");
        }
    }

    public void RestAtCampfire()
    {
        // Восстановление HP и хилок при подходе к костру
        Heal(restoreAmount);
        currentHills += restoreHills;
        if (currentHills > maxHills)
            currentHills = maxHills;

        UpdateUI();
    }

    private void UpdateUI()
    {
        healthSlider.value = currentHealth / maxHealth;
        hillsText.text = "Хилки: " + currentHills;
    }

    // Метод для получения урона от врага, вызываемый из другого скрипта или триггера
    public void ReceiveDamageFromEnemy(float damage)
    {
        TakeDamage(damage);
    }
}