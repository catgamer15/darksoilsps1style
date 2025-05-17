using UnityEngine;

public class PlaySoundOnKeyPress : MonoBehaviour
{
    public AudioSource audioSource; // компонент AudioSource
    public AudioClip clickClip;     // звук при нажатии

    void Update()
    {
        // Проверка на нажатие нужной клавиши (например, пробела)
        if (Input.GetMouseButtonDown(0))
        {
            if (clickClip != null)
            {
                audioSource.PlayOneShot(clickClip);
            }
        }
    }

}