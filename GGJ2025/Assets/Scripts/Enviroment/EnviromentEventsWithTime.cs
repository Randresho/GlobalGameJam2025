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
                
    }

    public void DebugString(string str)
    {
        Debug.Log(str);
    }

    public void StartGame() 
    {
        foreach (EventTimers timer in eventsTimers)
        {
            timer.StartGame();
        }
    }

    public void Pause()
    {
        foreach (EventTimers timer in eventsTimers)
        {
            timer.Pause();
        }
    }

    public void Resume()
    {
        foreach (EventTimers timer in eventsTimers)
        {
            timer.Resume();
        }
    }

    public void Stop()
    {
        foreach (EventTimers timer in eventsTimers)
        {
            timer.Stop();
        }
    }

    [System.Serializable]
    public class EventTimers
    {
        public string eventName;
        public float timer;
        public UnityEvent OnTimerEnd;

        public void StartGame()
        {
            if(timer <= GameManager.instance.totalSongTimer)
                Timing.RunCoroutine(TimerActive(), "Timer");
        }

        public void Pause()
        {
            Timing.PauseCoroutines("Timer");
        }

        public void Resume()
        {
            Timing.ResumeCoroutines("Timer");
        }

        public void Stop()
        {
            Timing.KillCoroutines("Timer");
        }

        private IEnumerator<float> TimerActive()
        {
            yield return Timing.WaitForSeconds(timer);
            OnTimerEnd?.Invoke();
        }
    }
}
