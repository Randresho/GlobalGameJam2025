using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioSpectrumDetector : MonoBehaviour
{
    public static AudioSpectrumDetector Instance;
    private const int BANDS = 8;

    [SerializeField]
    private AudioSource musicAudioSourceDetector;

    private float[] audioSpectrumLeft = new float[512];
    private float[] audioSpectrumRight = new float[512];

    private float[] frequencyBand = new float[BANDS];
    private float[] smoothedFrequencyBands = new float[BANDS]; // For smoothing
    private float smoothingFactor = 0.2f; // Adjust to control smoothing (closer to 1 = less smoothing)

    private float[] previousFrequencyBands = new float[BANDS]; // For onset detection
    private float onsetThreshold = 0.1f; // Adjust to set sensitivity for detecting onsets

    private float[] lastOnsetTime = new float[BANDS]; // Last time an onset was detected for each band

    public float minTimeBetweenBeats = 0.5f; // Minimum time interval between beats in seconds (300 BPM max)

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // Initialize frequency bands with small values to prevent divide-by-zero issues
        for (int i = 0; i < BANDS; i++)
        {
            smoothedFrequencyBands[i] = 0f;
            previousFrequencyBands[i] = 0f;
            lastOnsetTime[i] = -minTimeBetweenBeats; // Ensure beats can be detected initially
        }
    }

    private void Update()
    {
        GetSpectrumAudio();

        MakeFrequencyBands();
        SmoothFrequencyBands(); // Apply smoothing
        DetectOnset(); // Perform onset detection
    }

    private void GetSpectrumAudio()
    {
        musicAudioSourceDetector.GetSpectrumData(audioSpectrumLeft, 0, FFTWindow.Blackman);
        musicAudioSourceDetector.GetSpectrumData(audioSpectrumRight, 1, FFTWindow.Blackman);
    }

    public float[] SmoothedFrequencyBands()
    {
        return smoothedFrequencyBands;
    }

    public float GetSmoothedBandValue(int bandIndex)
    {
        if (bandIndex < 0 || bandIndex >= BANDS)
        {
            Debug.LogError(
                $"Band index {bandIndex} is out of range. Valid range is 0 to {BANDS - 1}."
            );
            return 0f;
        }

        return smoothedFrequencyBands[bandIndex];
    }

    #region Make Frequency Bands
    private void MakeFrequencyBands()
    {
        int count = 0;

        for (int i = 0; i < BANDS; i++)
        {
            float average = 0;
            int sampleCount = (int)Mathf.Pow(2, i) * 2;

            if (i == BANDS - 1) // Last band
            {
                sampleCount += 2;
            }

            for (int j = 0; j < sampleCount; j++)
            {
                average += (audioSpectrumLeft[count] + audioSpectrumRight[count]) * (count + 1);
                count++;
            }

            average /= count;
            frequencyBand[i] = average * 10;
        }
    }
    #endregion

    #region Smooth Frequency Bands
    private void SmoothFrequencyBands()
    {
        for (int i = 0; i < BANDS; i++)
        {
            smoothedFrequencyBands[i] = Mathf.Lerp(
                smoothedFrequencyBands[i],
                frequencyBand[i],
                smoothingFactor
            );
        }
    }
    #endregion

    #region Onset Detection
    private void DetectOnset()
    {
        float currentTime = Time.time;

        for (int i = 0; i < BANDS; i++)
        {
            // Calculate energy difference
            float energyDifference = smoothedFrequencyBands[i] - previousFrequencyBands[i];

            // Check if the energy difference exceeds the threshold
            if (
                energyDifference > onsetThreshold
                && currentTime - lastOnsetTime[i] >= minTimeBetweenBeats
            )
            {
                lastOnsetTime[i] = currentTime; // Update last onset time
            }

            // Update previous frequency band for the next frame
            previousFrequencyBands[i] = smoothedFrequencyBands[i];
        }
    }
    #endregion
}
