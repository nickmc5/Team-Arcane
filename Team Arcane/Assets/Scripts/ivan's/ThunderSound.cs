using UnityEngine;

public class ThunderSound : MonoBehaviour
{
    public AudioSource thunderAudioSource;
    public float minDelay = 5f;  // Minimum time between thunders
    public float maxDelay = 15f; // Maximum time between thunders

    private void Start()
    {
        StartCoroutine(PlayThunderAtRandomIntervals());
    }

    private System.Collections.IEnumerator PlayThunderAtRandomIntervals()
    {
        while (true)
        {
            float delay = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(delay);

            thunderAudioSource.Play();
        }
    }
}
