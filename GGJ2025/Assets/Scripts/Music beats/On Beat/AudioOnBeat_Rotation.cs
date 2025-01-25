using System.Collections;
using System.Collections.Generic;
using MEC;
using UnityEngine;
using UnityEngine.UIElements;

public class AudioOnBeat_Rotation : Beat_Detector
{
    [Header("Audio Rotation")]
    [SerializeField]
    private float rotationSpeed;

    [SerializeField]
    private float rotationMultiplier;

    private Vector3 rotationAngle;
    private Quaternion startRotation;

    private void Start()
    {
        OnBeat += AudioSpectrum_OnBeat;
        startRotation = transform.rotation;
    }

    private void AudioSpectrum_OnBeat(object sender, System.EventArgs e)
    {
        RotateOnBeat();
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
            RotateOnBeat();
        }
    }

    private void RotateOnBeat()
    {
        rotationAngle = new Vector3(
            (changeFactor * rotationMultiplier * tX) + startRotation.x,
            (changeFactor * rotationMultiplier * tY) + startRotation.y,
            (changeFactor * rotationMultiplier * tZ) + startRotation.z
        );

        transform.Rotate(rotationAngle, Space.Self);
        Timing.RunCoroutine(ReturnOrigin());
    }

    private IEnumerator<float> ReturnOrigin()
    {
        while (Quaternion.Angle(transform.rotation, startRotation) > 0.01f)
        {
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                startRotation,
                rotationSpeed * Time.deltaTime
            );
            yield return Timing.WaitForOneFrame;
        }

        transform.rotation = startRotation;
    }
}
