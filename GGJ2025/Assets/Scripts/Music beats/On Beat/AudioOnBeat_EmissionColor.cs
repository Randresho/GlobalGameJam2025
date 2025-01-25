using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioOnBeat_EmissionColor : Beat_Detector
{
    private const string COLOREMISSION = "_Emission";

    [Header("Audio Emission Color")]
    [SerializeField]
    private Material mat_Emissive;

    private void Start()
    {
        OnBeat += AudioOnBeat_EmissionColor_OnBeat;
    }

    protected override void OnUpdate()
    {
        DetectBeat(bandFrequency);
        float currentBandValue = AudioSpectrumDetector.Instance.GetSmoothedBandValue(bandFrequency);

        if (currentBandValue > 0)
        {
            ChangeEmissionColor(currentBandValue);
        }
    }

    private void ChangeEmissionColor(float i)
    {
        Color color_ = new Color(i, i, i);
        mat_Emissive.SetColor(COLOREMISSION, color_);
    }

    private void AudioOnBeat_EmissionColor_OnBeat(object sender, System.EventArgs e)
    {
        //throw new System.NotImplementedException();
    }
}
