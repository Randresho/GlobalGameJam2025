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
    private AudioSource baseAudio;

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
        generatorAudio.time = userDelay;
        generatorAudio.Play();
        playerAudio.Play();
        baseAudio.Play();
    }

    public void Pause()
    {
        generatorAudio.Pause();
        playerAudio.Pause();
        baseAudio.Pause();
    }

    public void Resume()
    {
        playerAudio.Play();
        baseAudio.Play();
        generatorAudio.time = playerAudio.time + userDelay;
        generatorAudio.Play();
    }

    public void Reset()
    {
        generatorAudio.Stop();
        generatorAudio.time = userDelay;
        playerAudio.Stop();
        playerAudio.time = 0;
        baseAudio.Stop();
        baseAudio.time = 0;
    }
}
