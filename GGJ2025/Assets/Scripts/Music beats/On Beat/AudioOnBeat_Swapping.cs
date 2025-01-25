using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioOnBeat_Swapping : Beat_Detector
{
    [Header("Audio Swapping")]
    [SerializeField] private bool usePosition;
    [SerializeField] private Vector3 newPos;
    private Vector3 starPos;
    [Space]
    [SerializeField] private bool useRotation;
    [SerializeField] private Quaternion newRotation;
    private Quaternion startRotation;
    [Space]
    [SerializeField] private bool useSetActive;

    // Start is called before the first frame update
    void Start()
    {
        OnBeat += AudioOnBeat_Swapping_OnBeat;
        starPos = transform.position;
        startRotation = transform.localRotation;
    }

    public override void OnUpdate()
    {
        if (use64)
        {
            DetectBeat(bandFrequency64);
        }
        else
        {
            DetectBeat(bandFrequency);
        }
    }

    private void AudioOnBeat_Swapping_OnBeat(object sender, System.EventArgs e)
    {
        Debug.Log("Beat!");

        if(usePosition)
        {
            if(transform.position == newPos)
            {
                transform.position = starPos;
            }
            else if(transform.position == starPos)
            {
                transform.position = newPos;
            }
        }

        if(useRotation)
        {
            if(transform.rotation == newRotation)
            {
                transform.localRotation = startRotation;
            }
            else if(transform.localRotation == startRotation)
            {
                transform.localRotation = newRotation;
            }
        }

        if(useSetActive)
        {
            if(gameObject.activeSelf)
            {
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(true);
            }
        }
    }
}
