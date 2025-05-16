using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Slider healthSlider; // ссылка на Slider
    public int maxHealth = 100;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
            currentHealth = 0;
        UpdateHealthBar();
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        healthSlider.value = (float)currentHealth / maxHealth;
    }
}