using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoStart : MonoBehaviour
{
    void Start()
    {
        // �������� "StartScene" �� �������� ����� �����
        SceneManager.LoadScene("MainMenu");
    }
}