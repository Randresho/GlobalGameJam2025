using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioSpectrumDetector : MonoBehaviour
{
    public static AudioSpectrumDetector Instance;
    private const int BANDS = 8; 
    private const int BANDS64 = 64; 

    private enum Channel { Stereo, Left, Right, };
    [SerializeField] private Channel channel = new Channel();

    [SerializeField] private AudioSource musicAudioSourceDetector;    

    private float[] audioSpectrumLeft = new float[512];
    private float[] audioSpectrumRight = new float[512];

    private float[] frequencyBand = new float[8];
    private float[] bandBuffer = new float[8];
    private float[] bufferDecrease = new float[8];

    private float[] frequencyBandHighest = new float[8];
    private float[] audioBand = new float[8];
    private float[] audioBandBuffer = new float[8];

    private float amplitude;
    private float amplitudeBuffer;
    private float amplitudeHighest;

    private float audioProfile = 5f;

    //Audio 64
    private float[] frequencyBand64 = new float[64];
    private float[] bandBuffer64 = new float[64];
    private float[] bufferDecrease64 = new float[64];

    private float[] frequencyBandHighest64 = new float[64];
    private float[] audioBand64 = new float[64];
    private float[] audioBandBuffer64 = new float[64];

    #region Getters
    #region Frequency Band
    public float[] FrequencyBand()
    {
        return frequencyBand;
    }

    public float[] FrequencyBand64() 
    {
        return frequencyBand64;
    }
    #endregion

    #region Band Buffer
    public float[] BandBuffers()
    {
        return bandBuffer;
    }

    public float[] BandBuffers64()
    {
        return bandBuffer64;
    }
    #endregion

    #region Frequency Band Highest
    public float[] FrequencyBandHighest() 
    {
        return frequencyBandHighest;
    }

    public float[] FrequencyBandHighest64() 
    {
        return frequencyBandHighest64;
    }
    #endregion

    #region Audio Band
    public float[] AudioBand()
    {
        return audioBand;
    }

    public float[] AudioBand64()
    {
        return audioBand64;
    }
    #endregion

    #region Audio Band Buffer
    public float[] AudioBandBuffer()
    {
        return audioBandBuffer;
    }
    public float[] AudioBandBuffer64()
    {
        return audioBandBuffer64;
    }
    #endregion

    #region Amplitude
    public float Amplitude()
    {
        return amplitude;
    }

    public float AmplitudeBuffer()
    {
        return amplitudeBuffer;
    }
    #endregion
    #endregion

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        AudioProfile(audioProfile);
    }

    private void Update()
    {
        GetSpectrumAudio();

        MakeFrequencyBands();
        MakeFrequencyBands64();

        BandBuffer();
        BandBuffer64();

        CreateAudioBand();
        CreateAudioBand64();

        GetAmplitude();
    }

    private void GetSpectrumAudio()
    {
        musicAudioSourceDetector.GetSpectrumData(audioSpectrumLeft, 0, FFTWindow.Blackman);
        musicAudioSourceDetector.GetSpectrumData(audioSpectrumRight, 1, FFTWindow.Blackman);
    }

    #region Make Frequency Band
    private void MakeFrequencyBands()
    {
        int count = 0;

        for (int i = 0; i < BANDS; i++)
        {
            float average = 0;
            int sampleCount = (int)Mathf.Pow(2, i) * 2;

            if (i == 7)
            {
                sampleCount += 2;
            }

            for (int j = 0; j < sampleCount; j++)
            {
                switch (channel)
                {
                    case Channel.Stereo:
                        average += (audioSpectrumLeft[count] + audioSpectrumRight[count]) * (count + 1);
                        break;
                    case Channel.Left:
                        average += (audioSpectrumLeft[count]) * (count + 1);
                        break;
                    case Channel.Right:
                        average += (audioSpectrumRight[count]) * (count + 1);
                        break;
                    default:
                        break;
                }
                count++;
            }
            average /= count;
            frequencyBand[i] = average * 10;
        }
    }

    private void MakeFrequencyBands64()
    {
        int count = 0;
        int sampleCount = 1;
        int power = 0;

        for (int i = 0; i < BANDS64; i++)
        {
            float average = 0;

            if (i == 16 || i == 32 || i == 40 || i == 48 || i == 56)
            {
                power++;
                sampleCount = (int)Mathf.Pow(2, power);

                if(power == 3)
                {
                    sampleCount -= 2;
                }
            }

            for (int j = 0; j < sampleCount; j++)
            {
                switch (channel)
                {
                    case Channel.Stereo:
                        average += (audioSpectrumLeft[count] + audioSpectrumRight[count]) * (count + 1);
                        break;
                    case Channel.Left:
                        average += (audioSpectrumLeft[count]) * (count + 1);
                        break;
                    case Channel.Right:
                        average += (audioSpectrumRight[count]) * (count + 1);
                        break;
                    default:
                        break;
                }
                count++;
            }

            average /= count;
            frequencyBand64[i] = average * 80;
        }
    }
    #endregion

    #region Band Buffer
    private void BandBuffer()
    {
        for (int i = 0; i < BANDS; i++)
        {
            if (frequencyBand[i] > bandBuffer[i])
            {
                bandBuffer[i] = frequencyBand[i];
                bufferDecrease[i] = 0.005f;              
            }

            if (frequencyBand[i] < bandBuffer[i])
            {
                bandBuffer[i] -= bufferDecrease[i];
                bufferDecrease[i] *= 1.2f;
            }
        }
    }

    private void BandBuffer64()
    {
        for (int i = 0; i < BANDS64; i++)
        {
            if (frequencyBand64[i] > bandBuffer64[i])
            {
                bandBuffer64[i] = frequencyBand64[i];
                bufferDecrease64[i] = 0.005f;
            }

            if (frequencyBand64[i] < bandBuffer64[i])
            {
                bandBuffer64[i] -= bufferDecrease64[i];
                bufferDecrease64[i] *= 1.2f;
            }
        }
    }
    #endregion

    #region Create Audio Bands
    private void CreateAudioBand()
    {
        for (int i = 0; i < BANDS; i++)
        {
            if (frequencyBand[i] > frequencyBandHighest[i])
            {
                frequencyBandHighest[i] = frequencyBand[i];
            }

            audioBand[i] = (frequencyBand[i] / frequencyBandHighest[i]);

            audioBandBuffer[i] = (bandBuffer[i] / frequencyBandHighest[i]);
        }
    }

    private void CreateAudioBand64()
    {
        for (int i = 0; i < BANDS64; i++)
        {
            if (frequencyBand64[i] > frequencyBandHighest64[i])
            {
                frequencyBandHighest64[i] = frequencyBand64[i];
            }

            audioBand64[i] = (frequencyBand64[i] / frequencyBandHighest64[i]);

            audioBandBuffer64[i] = (bandBuffer64[i] / frequencyBandHighest64[i]);
        }
    }
    #endregion

    private void GetAmplitude()
    {
        float curAmplitude = 0;
        float curAmplitudeBuffer = 0;

        for (int i = 0; i < BANDS; i++) 
        {
            curAmplitude += audioBand[i];
            curAmplitudeBuffer += audioBandBuffer[i];
        }

        if(curAmplitude > amplitudeHighest)
        {
            amplitudeHighest = curAmplitude;
        }

        amplitude = curAmplitude / amplitudeHighest;
        amplitudeBuffer = curAmplitudeBuffer / amplitudeHighest;
    }   

    private void AudioProfile(float audioProfile)
    {
        for (int i = 0; i < BANDS; i++)
        {
            frequencyBandHighest[i] = audioProfile;
        }
    }
}
