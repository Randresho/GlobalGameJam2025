using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beat_Detector : MonoBehaviour
{
    public event EventHandler OnBeat;

    [Header("Parameters")]
    [Range(0, 7)] public int bandFrequency;
    [HideInInspector ,Range(0, 63)] public int bandFrequency64;
    [Range(0, 1)] public float beatThreshold;
    [HideInInspector] public float changeFactor;
    [HideInInspector] public bool use64 = false;
    public bool useRandom = false;
    public bool useRandomThreshold = false;

    [Header("XYZ")]
    public bool X;
    public bool Y;
    public bool Z;

    [HideInInspector] public int tX;
    [HideInInspector] public int tY;
    [HideInInspector] public int tZ;

    private int randomBand64;
    private int randomBand;
    private float randomThreshold;

    private void Awake()
    {
        OnRandom();
    }

    private void Update()
    {
        OnUpdate();
    }

    public virtual void OnUpdate()
    {

    }

    public void DetectBeat(int i)
    {
        if(use64)
        {
            if (AudioSpectrumDetector.Instance.AudioBandBuffer64()[i] >= beatThreshold)
            {
                OnBeating(i);
            }
            else if (changeFactor > 0)
            {
                changeFactor = 0;
            }
        }
        else
        {
            if (AudioSpectrumDetector.Instance.AudioBandBuffer()[i] >= beatThreshold)
            {
                OnBeating(i);
            }
            else if (changeFactor > 0)
            {
                changeFactor = 0;
            }
        }
    }

    private void OnBeating(int i)
    {
        OnBeat?.Invoke(this, EventArgs.Empty);
        if(use64)
        {
            changeFactor = AudioSpectrumDetector.Instance.AudioBandBuffer64()[i];

        }
        else
        {
            changeFactor = AudioSpectrumDetector.Instance.AudioBandBuffer()[i];
        }
    }

    private void OnRandom()
    {
        if(useRandom)
        {
            if (use64)
            {
                randomBand64 = UnityEngine.Random.Range(0, 63);
                bandFrequency64 = randomBand64;
            }
            else
            {
                randomBand = UnityEngine.Random.Range(0, 7);
                bandFrequency = randomBand;
            }

            if(useRandomThreshold)
            {
                randomThreshold = UnityEngine.Random.Range(0f, 1f);
                beatThreshold = randomThreshold;
            }
        }
    }
}
