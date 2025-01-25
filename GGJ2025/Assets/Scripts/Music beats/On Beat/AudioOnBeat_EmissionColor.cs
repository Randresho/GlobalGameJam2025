using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioOnBeat_EmissionColor : Beat_Detector
{
    private const string COLOREMISSION = "_Emission";
    [Header("Audio Emission Color")]
    [SerializeField] private Material mat_Emissive;

    private void Start()
    {
        OnBeat += AudioOnBeat_EmissionColor_OnBeat;
    }

    public override void OnUpdate()
    {
        if (use64)
        {
            DetectBeat(bandFrequency64);
            if (AudioSpectrumDetector.Instance.AudioBandBuffer64()[bandFrequency64] > 0)
            {
                ChangeEmissionColor64(bandFrequency64);
            }
        }
        else
        {
            DetectBeat(bandFrequency);
            if (AudioSpectrumDetector.Instance.AudioBandBuffer()[bandFrequency] > 0)
            {
                ChangeEmissionColor(bandFrequency);
            }
        }
    }

    private void ChangeEmissionColor(int i)
    {
        Color color_ = new Color(AudioSpectrumDetector.Instance.AudioBandBuffer()[i], AudioSpectrumDetector.Instance.AudioBandBuffer()[i], AudioSpectrumDetector.Instance.AudioBandBuffer()[i]);
        mat_Emissive.SetColor(COLOREMISSION, color_);
    }

    private void ChangeEmissionColor64(int i)
    {
        Color color_ = new Color(AudioSpectrumDetector.Instance.AudioBandBuffer64()[i] , AudioSpectrumDetector.Instance.AudioBandBuffer64()[i], AudioSpectrumDetector.Instance.AudioBandBuffer64()[i]);
        mat_Emissive.SetColor(COLOREMISSION, color_);
    }

    private void AudioOnBeat_EmissionColor_OnBeat(object sender, System.EventArgs e)
    {
        //throw new System.NotImplementedException();
    }
}
