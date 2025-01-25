using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatDetector : MonoBehaviour
{
    public static BeatDetector Instance;
    public event EventHandler OnBeatDetected;

    public enum Beats { HighBeat, MediumBeat, LowBeat, };
    public Beats beatType;
    [SerializeField] private float timeSteps = 0.15f;
    [Space]
    [SerializeField] private float highBeat = 0.5f;
    [SerializeField] private float mediumBeat = 0.2f;
    [SerializeField] private float lowBeat = 0.05f;
    [Space]

    public float timeToBeat = 0.05f;
    public float resetSmoothTime = 2f;

    private float beat = 0.15f;
    private float timer;
    private float audioValue;
    private float previousAudioValue;

    protected bool isBeat;

    private void Awake()
    {
        Instance = this;
    }

    private void FixedUpdate()
    {
        DetectBeats();
    }

    public virtual void DetectBeats()
    {
        previousAudioValue = audioValue;
        //audioValue = AudioSpectrumDetector.SpectrumValue;

        if (previousAudioValue > beat && audioValue <= beat)
        {
            if(timer > timeSteps)
            {
                OnBeat();
            }
        }

        if (previousAudioValue < beat && audioValue > beat)
        {
            if (timer > timeSteps)
            {
                OnBeat();
            }
        }

        timer += Time.fixedDeltaTime;

        switch (beatType)
        {
            case Beats.HighBeat:
                beat = highBeat;
                break;
            case Beats.MediumBeat:
                beat = mediumBeat;
                break;
            case Beats.LowBeat:
                beat = lowBeat;
                break;
            default:
                break;
        }
    }

    private void OnBeat()
    {
        OnBeatDetected?.Invoke(this, EventArgs.Empty);
        timer = 0f;
        isBeat = true;
        Debug.Log("Beat!");
    }

    protected float Null()
    {
        return 0f;
    }
}
