using UnityEngine;
using System.Collections;

public class ThunderLight : MonoBehaviour
{
    [Header("Light")]
    [SerializeField] private Light directionalLight;

    [Header("Thunder Intensity")]
    [SerializeField] private float thunderIntensity = 3.5f;

    [Header("Time Between Thunders (seconds)")]
    [SerializeField] private float minDelay = 5f;
    [SerializeField] private float maxDelay = 15f;

    [Header("Thunder Rise Time (seconds)")]
    [SerializeField] private float minRiseTime = 0.03f;
    [SerializeField] private float maxRiseTime = 0.08f;

    [Header("Thunder Fall Time (seconds)")]
    [SerializeField] private float minFallTime = 0.1f;
    [SerializeField] private float maxFallTime = 0.25f;

    [Header("Thunder Sounds")]
    [SerializeField] private AudioClip thunderSound1;
    [SerializeField] private AudioClip thunderSound2;
    [SerializeField] private AudioClip thunderSound3;

    [SerializeField, Range(0f, 1f)]
    private float thunderVolume = 0.8f;

    private float originalIntensity;
    private AudioSource audioSource;
    private AudioClip lastPlayedClip;

    private void Awake()
    {
        if (directionalLight == null)
            directionalLight = GetComponent<Light>();

        originalIntensity = directionalLight.intensity;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0f; // 2D (orage global)
    }

    private void Start()
    {
        StartCoroutine(ThunderRoutine());
    }

    private IEnumerator ThunderRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));

            float riseTime = Random.Range(minRiseTime, maxRiseTime);
            float fallTime = Random.Range(minFallTime, maxFallTime);

            // Montée progressive
            yield return StartCoroutine(
                LerpIntensity(originalIntensity, thunderIntensity, riseTime)
            );

            // Son déclenché au pic de lumière
            PlayThunderSound();

            // Descente progressive
            yield return StartCoroutine(
                LerpIntensity(thunderIntensity, originalIntensity, fallTime)
            );
        }
    }

    private void PlayThunderSound()
    {
        AudioClip[] clips = { thunderSound1, thunderSound2, thunderSound3 };

        // Filtrer les clips valides
        clips = System.Array.FindAll(clips, clip => clip != null);

        if (clips.Length == 0)
            return;

        AudioClip selectedClip;

        // Empêcher la répétition consécutive
        do
        {
            selectedClip = clips[Random.Range(0, clips.Length)];
        }
        while (clips.Length > 1 && selectedClip == lastPlayedClip);

        lastPlayedClip = selectedClip;

        audioSource.PlayOneShot(selectedClip, thunderVolume);
    }

    private IEnumerator LerpIntensity(float from, float to, float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            directionalLight.intensity = Mathf.Lerp(from, to, t);
            yield return null;
        }

        directionalLight.intensity = to;
    }
}