using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField]
    private AudioSource generatorAudio;

    [SerializeField]
    private AudioSource playerAudio;

    [SerializeField]
    private float userDelay = 5;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Play()
    {
        playerAudio.time = userDelay;
        generatorAudio.Play();
        playerAudio.Play();
    }

    public void Pause()
    {
        generatorAudio.Pause();
        playerAudio.Pause();
    }

    public void Resume()
    {
        generatorAudio.Play();
        playerAudio.time = generatorAudio.time + userDelay;
        playerAudio.Play();
    }

    public void Reset()
    {
        generatorAudio.Stop();
        playerAudio.Stop();
        generatorAudio.time = 0;
        playerAudio.time = userDelay;
    }
}
