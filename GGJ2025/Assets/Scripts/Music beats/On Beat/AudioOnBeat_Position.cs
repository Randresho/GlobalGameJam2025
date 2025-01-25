using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioOnBeat_Position : Beat_Detector
{
    [Header("Audio Position")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveMultiplier;

    private Vector3 targetPos;
    private Vector3 startPos;
    private bool isMoving = false;

    private void Start()
    {
        OnBeat += AudioOnBeat_Position_OnBeatDetected;

        startPos = transform.position;
    }

    public override void OnUpdate()
    {
        tX = X ? 1 : 0;
        tY = Y ? 1 : 0;
        tZ = Z ? 1 : 0;

        if (use64)
        {
            DetectBeat(bandFrequency64);
            if (AudioSpectrumDetector.Instance.AudioBandBuffer64()[bandFrequency64] > 0)
            {
                Moving();
            }
        }
        else
        {
            DetectBeat(bandFrequency);
            if (AudioSpectrumDetector.Instance.AudioBandBuffer()[bandFrequency] > 0)
            {
                Moving();
            }
        }
    }

    private void Moving()
    {
        targetPos = new Vector3((changeFactor * moveMultiplier * tX) + startPos.x, (changeFactor * moveMultiplier * tY) + startPos.y, (changeFactor * moveMultiplier * tZ) + startPos.z);

        if(isMoving)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPos) < 0.1f)
            {
                isMoving = false;
                Timing.RunCoroutine(ResetPos());
            }
        }
    }

    private IEnumerator<float> ResetPos()
    {
        yield return Timing.WaitForSeconds(0.2f);

        while (Vector3.Distance(transform.position, startPos) > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, startPos, moveSpeed * Time.deltaTime);
            yield return Timing.WaitForOneFrame;
        }
        transform.position = startPos;
    }

    private void AudioOnBeat_Position_OnBeatDetected(object sender, System.EventArgs e)
    {
        isMoving = true;
    }
}
