using UnityEngine;
using UnityEngine.Events;

public class ButtonEventListener : MonoBehaviour
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
    private UnityEvent OnButtonPressed = new();

    [SerializeField]
    private UnityEvent OnButtonReleased = new();

    private void OnEnable()
    {
        switch (targetButton)
        {
            case BeatButtons.LEFT:
                InputManager.Instance.OnLeftPressed.AddListener(OnPressed);
                InputManager.Instance.OnLeftReleased.AddListener(OnReleased);

                break;
            case BeatButtons.UP:
                InputManager.Instance.OnUpPressed.AddListener(OnPressed);
                InputManager.Instance.OnUpReleased.AddListener(OnReleased);

                break;
            case BeatButtons.RIGHT:
                InputManager.Instance.OnRightPressed.AddListener(OnPressed);
                InputManager.Instance.OnRightReleased.AddListener(OnReleased);

                break;
            case BeatButtons.DOWN:
                InputManager.Instance.OnDownPressed.AddListener(OnPressed);
                InputManager.Instance.OnDownReleased.AddListener(OnReleased);
                break;
        }
    }

    private void OnDisable()
    {
        switch (targetButton)
        {
            case BeatButtons.LEFT:
                InputManager.Instance.OnLeftPressed.RemoveListener(OnPressed);
                InputManager.Instance.OnLeftReleased.RemoveListener(OnReleased);
                break;
            case BeatButtons.UP:
                InputManager.Instance.OnUpPressed.RemoveListener(OnPressed);
                InputManager.Instance.OnUpReleased.RemoveListener(OnReleased);
                break;
            case BeatButtons.RIGHT:
                InputManager.Instance.OnRightPressed.RemoveListener(OnPressed);
                InputManager.Instance.OnRightReleased.RemoveListener(OnReleased);
                break;
            case BeatButtons.DOWN:
                InputManager.Instance.OnDownPressed.RemoveListener(OnPressed);
                InputManager.Instance.OnDownReleased.RemoveListener(OnReleased);
                break;
        }
    }

    public void OnPressed()
    {
        Debug.Log($"{gameObject.name} pressed");
        OnButtonPressed.Invoke();
    }

    public void OnReleased()
    {
        Debug.Log($"{gameObject.name} released");
        OnButtonReleased.Invoke();
    }
}
