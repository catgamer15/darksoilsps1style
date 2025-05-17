using UnityEngine;

public class Footsteps : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] footstepClips; // массив звуков шагов
    public float stepInterval = 0.5f; // интервал между шагами
    private float stepTimer;

    void Update()
    {
        // Предположим, что у вас есть условие движения персонажа:
        bool isMoving = Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0;

        if (isMoving)
        {
            stepTimer -= Time.deltaTime;
            if (stepTimer <= 0f)
            {
                PlayFootstep();
                stepTimer = stepInterval;
            }
        }
    }

    void PlayFootstep()
    {
        if (footstepClips.Length > 0)
        {
            // выбираем случайный звук из массива
            AudioClip clip = footstepClips[Random.Range(0, footstepClips.Length)];
            audioSource.PlayOneShot(clip);
        }
    }
}