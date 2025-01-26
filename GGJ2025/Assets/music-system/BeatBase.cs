using System.Collections;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.Events;

public class BeatBase : MonoBehaviour
{
    public enum BubbleStages
    {
        IDLE,
        MOVING,
        EARLY,
        PERFECT,
        LATE,
        DEATH
    }

    [SerializeField]
    private BubbleStages state = BubbleStages.IDLE;

    [SerializeField]
    private float deathDelay;

    private float travelSpeed;
    private int scoreValue;
    private float earlyPenalty;
    private float latePenalty;

    private bool moving = false;
    private bool deathTriggered = false;

    private UnityEvent pressedCallback;
    private UnityEvent onEarlyPress;
    private UnityEvent onPerfectPress;
    private UnityEvent onLatePress;
    private UnityEvent onDeath;

    private void Awake()
    {
        state = BubbleStages.IDLE;
        deathTriggered = false;
        moving = false;
    }

    private void OnDisable()
    {
        if (pressedCallback != null)
            pressedCallback.RemoveListener(OnPressed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var hitTag = other.gameObject.tag;

        OnZoneChange(hitTag);
    }

    private void Update()
    {
        if (moving)
            Move();
    }

    public void Initialize(float speed, UnityEvent handler)
    {
        travelSpeed = speed;
        moving = true;
        state = BubbleStages.MOVING;
        pressedCallback = handler;
        pressedCallback.AddListener(OnPressed);
    }

    public void SetScoreSettings(int score, float early, float late)
    {
        scoreValue = score;
        earlyPenalty = early;
        latePenalty = late;
    }

    public void SetCallbacks(
        UnityEvent early,
        UnityEvent perfect,
        UnityEvent late,
        UnityEvent death
    )
    {
        onEarlyPress = early;
        onPerfectPress = perfect;
        onLatePress = late;
        onDeath = death;
    }

    protected virtual void OnPressed()
    {
        switch (state)
        {
            case BubbleStages.EARLY:
                GameManager.instance.UpdateScore(Mathf.CeilToInt(scoreValue * earlyPenalty));
                Cleanup();
                onEarlyPress.Invoke();
                Destroy(gameObject);
                break;
            case BubbleStages.PERFECT:
                GameManager.instance.UpdateScore(Mathf.CeilToInt(scoreValue));
                Cleanup();
                onPerfectPress.Invoke();
                Destroy(gameObject);
                break;
            case BubbleStages.LATE:
                GameManager.instance.UpdateScore(Mathf.CeilToInt(scoreValue * latePenalty));
                Cleanup();
                onLatePress.Invoke();
                Destroy(gameObject);
                break;
        }
    }

    protected virtual void Move()
    {
        if(GameManager.instance.StartGame())
            transform.Translate(Vector3.up * -1 * travelSpeed * Time.deltaTime);
    }

    protected virtual void OnZoneChange(string tag)
    {
        switch (tag)
        {
            case "early":
                state = BubbleStages.EARLY;
                break;
            case "perfect":
                state = BubbleStages.PERFECT;
                break;
            case "late":
                state = BubbleStages.LATE;
                break;
            case "death":
                state = BubbleStages.DEATH;
                StartDeathSequence();
                break;
        }
    }

    protected virtual void StartDeathSequence()
    {
        if (!deathTriggered)
            StartCoroutine("BaseDeathSequence");
    }

    protected virtual IEnumerator BaseDeathSequence()
    {
        Cleanup();
        yield return new WaitForSeconds(deathDelay);
        onDeath.Invoke();
        Destroy(gameObject);
    }

    protected virtual void Cleanup()
    {
        deathTriggered = true;
        pressedCallback.RemoveListener(OnPressed);
        pressedCallback = null;
    }
}
