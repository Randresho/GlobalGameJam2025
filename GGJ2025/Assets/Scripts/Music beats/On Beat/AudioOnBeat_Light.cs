using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioOnBeat_Light : Beat_Detector
{
    [Header("Light")]
    [SerializeField]
    private float minIntencity;

    [SerializeField]
    private float maxIntencity;
    private Light lightIntencity;

    private void Awake()
    {
        lightIntencity = GetComponent<Light>();
    }
    //lightIntencity.intensity = (AudioSpectrumDetector.Instance.AudioBandBuffer()[bandFrequency] * (maxIntencity - minIntencity) + minIntencity);
}
