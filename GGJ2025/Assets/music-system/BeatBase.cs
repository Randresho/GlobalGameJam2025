using System.Collections;
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
    private float deathDelay;

    [SerializeField]
    private BubbleStages state = BubbleStages.IDLE;

    private float travelSpeed;
    private float scoreValue;

    private bool moving = false;
    private bool deathTriggered = false;

    private UnityEvent pressedCallback;

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

    public void Initialize(float speed, int score, UnityEvent handler)
    {
        travelSpeed = speed;
        scoreValue = score;
        moving = true;
        state = BubbleStages.MOVING;
        pressedCallback = handler;
        pressedCallback.AddListener(OnPressed);
    }

    protected virtual void OnPressed()
    {
        switch (state)
        {
            case BubbleStages.EARLY:
                GameManager.instance.UpdateScore(Mathf.CeilToInt(scoreValue * .75f));
                break;
            case BubbleStages.PERFECT:
                GameManager.instance.UpdateScore(Mathf.CeilToInt(scoreValue));
                break;
            case BubbleStages.LATE:
                GameManager.instance.UpdateScore(Mathf.CeilToInt(scoreValue * .5f));
                break;
        }
    }

    protected virtual void Move()
    {
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
        deathTriggered = true;
        pressedCallback.RemoveListener(OnPressed);
        pressedCallback = null;
        yield return new WaitForSeconds(deathDelay);
        Destroy(gameObject);
    }
}
