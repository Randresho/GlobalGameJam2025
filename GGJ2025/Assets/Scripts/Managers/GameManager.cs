using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using MEC;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public UnityEvent OnSongEnd;

    public float totalSongTimer;
    public float curSongTimer;

    [SerializeField]
    private bool isSongOver;

    [SerializeField]
    private bool startGame;

    [SerializeField]
    private AudioClip audioSong;

    [SerializeField]
    private TextMeshProUGUI scorePoints;

    [field: SerializeField]
    public int Score { get; private set; }

    private void Awake()
    {
        if (instance == null && instance != this)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("There one or more instance in the scene");
        }

        totalSongTimer = audioSong.length;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InputManager.Instance.SetInputType("ui");
        startGame = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (startGame)
        {
            if (curSongTimer >= totalSongTimer && !isSongOver)
            {
                isSongOver = true;
                startGame = false;
                OnSongEnd?.Invoke();
            }
            else if (curSongTimer < totalSongTimer && !isSongOver)
            {
                curSongTimer += Time.deltaTime;
            }
        }

        scorePoints.text = Score + "";
    }

    public bool IsSongOver()
    {
        return isSongOver;
    }

    public bool StartGame()
    {
        return startGame;
    }

    public void StartTheGame()
    {
        curSongTimer = 0f;
        Score = 0;
        isSongOver = false;
        ChangeGameStatus(true);
    }

    public void ChangeGameStatus(bool state)
    {
        startGame = state;
    }

    public void UpdateScore(int modifier)
    {
        Score += modifier;
    }

    public void ResetGame(string SceneName)
    {
        Timing.RunCoroutine(ResetScene(SceneName));
    }

    private IEnumerator<float> ResetScene(string SceneName)
    {
        yield return Timing.WaitForSeconds(0.75f);
        SceneManager.LoadScene(SceneName);  
    }

    public void SetPrincipalButton(Button button)
    {
        button.Select();
    }
}
