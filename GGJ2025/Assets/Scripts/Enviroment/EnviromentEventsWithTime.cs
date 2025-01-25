using UnityEngine;
using UnityEngine.Events;
using MEC;
using System.Collections.Generic;
using System;

public class EnviromentEventsWithTime : MonoBehaviour
{
    [SerializeField] private EventTimers[] eventsTimers;

    void Start()
    {
        foreach (EventTimers timer in eventsTimers)
        {
            timer.TimerToActive();
        }        
    }

    public void DebugString(string str)
    {
        Debug.Log(str);
    }


    [System.Serializable]
    public class EventTimers
    {
        public float timer;
        public UnityEvent OnTimerEnd;

        public void TimerToActive()
        {
            if(timer <= GameManager.instance.totalSongTimer)
                Timing.RunCoroutine(TimerActive(), "Timer");
        }

        private IEnumerator<float> TimerActive()
        {
            yield return Timing.WaitForSeconds(timer);
            OnTimerEnd?.Invoke();
        }
    }
}
