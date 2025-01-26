using DG.Tweening;
using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager_Menu : MonoBehaviour
{
    //public AudioClip winClip;
    //public AudioClip finishClip;
    [SerializeField] private AudioSource m_SourceMusic;
    [SerializeField] private AudioSource m_SourceSfx;
    [SerializeField] private AudioMixer mixer;

    private void Awake()
    {
    }

    public void PlayAudio(AudioClip clip)
    {
        m_SourceSfx.PlayOneShot(clip);
    }

    public void PlayFade()
    {
        Timing.RunCoroutine(Fade());
    }
    private IEnumerator<float> Fade()
    {
        yield return Timing.WaitForSeconds(0f);
        mixer.DOSetFloat("MenusMusic", -80f, 0f);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
