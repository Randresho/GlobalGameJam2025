using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Haptics;

public class InputManager : MonoBehaviour, InputActions.IPlayerActions, InputActions.IUIActions
{
    private InputActions inputActions;

    private static InputManager Instance;

    public UnityEvent OnLeftPressed = new();
    public UnityEvent OnLeftReleased = new();

    public UnityEvent OnRightPressed = new();
    public UnityEvent OnRightReleased = new();

    public UnityEvent OnUpPressed = new();
    public UnityEvent OnUpReleased = new();

    public UnityEvent OnDownPressed = new();
    public UnityEvent OnDownReleased = new();

    public UnityEvent OnPausePressed = new();
    public UnityEvent OnPauseReleased = new();

    public UnityEvent OnSubmitPressed = new();
    public UnityEvent OnSubmitReleased = new();

    public UnityEvent OnCancelPressed = new();
    public UnityEvent OnCancelReleased = new();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void OnEnable()
    {
        if (inputActions == null)
            inputActions = new InputActions();

        inputActions.Player.SetCallbacks(this);
        inputActions.UI.SetCallbacks(this);
    }

    private void Start()
    {
        SetInputType("ui");
    }

    public void SetInputType(string type)
    {
        switch (type)
        {
            case "player":
                inputActions.Player.Enable();
                inputActions.UI.Disable();
                break;
            case "ui":
                inputActions.UI.Enable();
                inputActions.Player.Disable();
                break;
            default:
                Debug.LogError($"The target input type ({type}) doesn't exist.");
                break;
        }
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
        inputActions.UI.Disable();
    }

    public void OnLeft(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                OnLeftPressed.Invoke();
                break;
            case InputActionPhase.Canceled:
                OnLeftReleased.Invoke();
                break;
        }
    }

    public void OnUp(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                OnUpPressed.Invoke();
                break;
            case InputActionPhase.Canceled:
                OnUpReleased.Invoke();
                break;
        }
    }

    public void OnRight(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                OnRightPressed.Invoke();
                break;
            case InputActionPhase.Canceled:
                OnRightReleased.Invoke();
                break;
        }
    }

    public void OnDown(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                OnDownPressed.Invoke();
                break;
            case InputActionPhase.Canceled:
                OnDownReleased.Invoke();
                break;
        }
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                OnPausePressed.Invoke();
                break;
            case InputActionPhase.Canceled:
                OnPauseReleased.Invoke();
                break;
        }
    }

    public void OnSubmit(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                OnSubmitPressed.Invoke();
                break;
            case InputActionPhase.Canceled:
                OnSubmitReleased.Invoke();
                break;
        }
    }

    public void OnCancel(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                OnCancelPressed.Invoke();
                break;
            case InputActionPhase.Canceled:
                OnCancelReleased.Invoke();
                break;
        }
    }
}
