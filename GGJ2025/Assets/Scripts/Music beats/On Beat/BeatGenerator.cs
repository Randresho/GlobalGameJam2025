using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
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

    private UnityEvent OnButtonPressed = new();

    private void OnEnable()
    {
        OnBeat += OnBeatDetected;

        switch (targetButton)
        {
            case BeatButtons.LEFT:
                InputManager.Instance.OnLeftPressed.AddListener(ButtonPressedHandler);
                break;
            case BeatButtons.UP:
                InputManager.Instance.OnUpPressed.AddListener(ButtonPressedHandler);
                break;
            case BeatButtons.RIGHT:
                InputManager.Instance.OnRightPressed.AddListener(ButtonPressedHandler);
                break;
            case BeatButtons.DOWN:
                InputManager.Instance.OnDownPressed.AddListener(ButtonPressedHandler);
                break;
        }
    }

    private void OnDisable()
    {
        OnBeat -= OnBeatDetected;

        switch (targetButton)
        {
            case BeatButtons.LEFT:
                InputManager.Instance.OnLeftPressed.RemoveListener(ButtonPressedHandler);
                break;
            case BeatButtons.UP:
                InputManager.Instance.OnUpPressed.RemoveListener(ButtonPressedHandler);
                break;
            case BeatButtons.RIGHT:
                InputManager.Instance.OnRightPressed.RemoveListener(ButtonPressedHandler);
                break;
            case BeatButtons.DOWN:
                InputManager.Instance.OnDownPressed.RemoveListener(ButtonPressedHandler);
                break;
        }
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

        beat.Initialize(beatTravelSpeed, beatScore, OnButtonPressed);
    }

    private void ButtonPressedHandler()
    {
        OnButtonPressed.Invoke();
    }
}
