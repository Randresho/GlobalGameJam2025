using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class BeatGenerator : Beat_Detector
{
    public enum BeatButtons
    {
        LEFT,
        UP,
        RIGHT,
        DOWN
    };

    [SerializeField]
    private BeatButtons targetButton;

    [SerializeField]
    private BeatBase beatPrefab;

    [SerializeField]
    private int beatScore;

    [SerializeField]
    private float beatTravelSpeed;

    private void OnEnable()
    {
        OnBeat += OnBeatDetected;
    }

    private void OnDisable()
    {
        OnBeat -= OnBeatDetected;
    }

    protected override void OnUpdate()
    {
        DetectBeat(bandFrequency);
    }

    private void OnBeatDetected(object sender, System.EventArgs e)
    {
        if (beatPrefab == null)
            return;

        var beat = Instantiate(beatPrefab, transform.position, quaternion.identity);

        beat.Initialize(beatTravelSpeed, beatScore);
    }
}
