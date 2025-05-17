using UnityEngine;

public class AreaSoundController : MonoBehaviour
{
    public AudioSource audioSource;

    private void OnTriggerEnter(Collider other)
    {
        // Можно добавить условие, например, если игрок входит в зону
        if (other.CompareTag("Player"))
        {
            if (!audioSource.isPlaying)
                audioSource.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Остановить звук при выходе из зоны
        if (other.CompareTag("Player"))
        {
            if (audioSource.isPlaying)
                audioSource.Stop();
        }
    }
}