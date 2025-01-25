using System.Collections;
using UnityEngine;

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

    private void Awake()
    {
        state = BubbleStages.IDLE;
        deathTriggered = false;
        moving = false;
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

    public void Initialize(float speed, int score)
    {
        travelSpeed = speed;
        scoreValue = score;
        moving = true;
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
        yield return new WaitForSeconds(deathDelay);
        Destroy(gameObject);
    }
}
