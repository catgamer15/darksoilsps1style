using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoStart : MonoBehaviour
{
    void Start()
    {
        // Замените "StartScene" на название вашей сцены
        SceneManager.LoadScene("MainMenu");
    }
}