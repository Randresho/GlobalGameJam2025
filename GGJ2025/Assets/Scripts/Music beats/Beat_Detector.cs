using System;
using UnityEngine;

public class Beat_Detector : MonoBehaviour
{
    public event EventHandler OnBeat;

    [Header("Parameters")]
    [Range(0, 7)]
    public int bandFrequency;

    [Range(0, 1)]
    public float beatThreshold;

    [HideInInspector]
    public float changeFactor;

    public bool useRandom = false;
    public bool useRandomThreshold = false;

    [Header("XYZ")]
    public bool X;
    public bool Y;
    public bool Z;

    [HideInInspector]
    public int tX;

    [HideInInspector]
    public int tY;

    [HideInInspector]
    public int tZ;

    private int randomBand;
    private float randomThreshold;

    private float lastBeatTime = -1f; // Last time a beat was triggered
    private float minTimeBetweenBeats; // Minimum time interval between beats (retrieved from AudioSpectrumDetector)

    private void Awake()
    {
        OnRandom();
    }

    private void Start()
    {
        minTimeBetweenBeats = AudioSpectrumDetector.Instance.minTimeBetweenBeats; // Retrieve from AudioSpectrumDetector
    }

    private void Update()
    {
        OnUpdate();
    }

    protected virtual void OnUpdate()
    {
        DetectBeat(bandFrequency);
    }

    public void DetectBeat(int bandIndex)
    {
        if (bandIndex < 0 || bandIndex >= 8)
        {
            Debug.LogWarning($"Band index {bandIndex} is out of range. Valid range is 0-7.");
            return;
        }

        float[] smoothedBands = AudioSpectrumDetector.Instance.SmoothedFrequencyBands();
        float currentTime = Time.time;

        // Check threshold and ensure minimum time between beats
        if (
            smoothedBands[bandIndex] >= beatThreshold
            && currentTime - lastBeatTime >= minTimeBetweenBeats
        )
        {
            OnBeating(bandIndex);
            lastBeatTime = currentTime; // Update last beat time
        }
        else if (changeFactor > 0)
        {
            changeFactor = 0;
        }
    }

    private void OnBeating(int bandIndex)
    {
        OnBeat?.Invoke(this, EventArgs.Empty);
        changeFactor = AudioSpectrumDetector.Instance.SmoothedFrequencyBands()[bandIndex];
    }

    private void OnRandom()
    {
        if (useRandom)
        {
            randomBand = UnityEngine.Random.Range(0, 8);
            bandFrequency = randomBand;

            if (useRandomThreshold)
            {
                randomThreshold = UnityEngine.Random.Range(0f, 1f);
                beatThreshold = randomThreshold;
            }
        }
    }
}
