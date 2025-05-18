using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    // ������������ ��������
    public float maxHealth = 100f;
    // ������� ��������
    private float currentHealth;

    // ������������ ���������� �����
    public int maxHills = 5;
    // ������� ���������� �����
    private int currentHills;

    // UI ��������
    public Slider healthSlider;      // ������ ��������
    public Text hillsText;           // ����� ��� ����������� �����

    // ����� �������������� ����� � �������� ��� ������� � ������
    public float restoreAmount = 20f;   // ���������� ������������������ HP
    public int restoreHills = 1;        // ���������� ����������������� �����

    void Start()
    {
        currentHealth = maxHealth;
        currentHills = maxHills;
        UpdateUI();
    }

    void Update()
    {
        // ��� �����: ������� H, ����� �������� ����
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(20f);
        }
        // ��� �����: ������� R, ����� ������������ �������� � ����� ��� ������� � ������
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestAtCampfire();
        }
        // ��� �����: ������� Q, ����� ������������ ����� ��� �������������� HP
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
            Debug.Log("����� ����");
            // ����� �������� ������ ������ �����
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
            Heal(20f); // �������������� �� 20 HP �� ���� �����
            UpdateUI();
        }
        else
        {
            Debug.Log("��� ����� ��� �������� ��� ������");
        }
    }

    public void RestAtCampfire()
    {
        // �������������� HP � ����� ��� ������� � ������
        Heal(restoreAmount);
        currentHills += restoreHills;
        if (currentHills > maxHills)
            currentHills = maxHills;

        UpdateUI();
    }

    private void UpdateUI()
    {
        healthSlider.value = currentHealth / maxHealth;
        hillsText.text = "�����: " + currentHills;
    }

    // ����� ��� ��������� ����� �� �����, ���������� �� ������� ������� ��� ��������
    public void ReceiveDamageFromEnemy(float damage)
    {
        TakeDamage(damage);
    }
}