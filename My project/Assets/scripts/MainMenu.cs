using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // ����� ���������� ��� ������� �� ������ "�����"
    public void StartGame()
    {
        // �������� "GameScene" �� �������� ����� ������� �����
        SceneManager.LoadScene("SampleScene");
        
    }
}