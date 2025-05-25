using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Метод вызывается при нажатии на кнопку "Старт"
    public void StartGame()
    {
        // Замените "GameScene" на название вашей игровой сцены
        SceneManager.LoadScene("SampleScene");
        
    }
}