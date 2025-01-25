using System.Collections;
using System.Collections.Generic;
using MEC;
using UnityEngine;

public class AudioOnBeat_Scale : Beat_Detector
{
    [Header("Audio Scale")]
    [SerializeField]
    private float scaleMultiplier;
    private Vector3 startScale;

    private void Start()
    {
        OnBeat += AudioOnBeat_Position_OnBeatDetected;

        startScale = transform.localScale;
    }

    protected override void OnUpdate()
    {
        tX = X ? 1 : 0;
        tY = Y ? 1 : 0;
        tZ = Z ? 1 : 0;

        DetectBeat(bandFrequency);
        float currentBandValue = AudioSpectrumDetector.Instance.GetSmoothedBandValue(bandFrequency);

        if (currentBandValue > 0)
        {
            transform.localScale = new Vector3(
                (changeFactor * scaleMultiplier * tX) + startScale.x,
                (changeFactor * scaleMultiplier * tY) + startScale.y,
                (changeFactor * scaleMultiplier * tZ) + startScale.z
            );
        }
    }

    private void AudioOnBeat_Position_OnBeatDetected(object sender, System.EventArgs e) { }
}
