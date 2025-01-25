using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float totalSongTimer;
    public float curSongTimer;
<<<<<<< HEAD
    [SerializeField] private bool isSongOver;
    [SerializeField] private bool startGame;
=======

    [SerializeField]
    private bool isSongOver;

    [field: SerializeField]
    public int Score { get; private set; }
>>>>>>> 525394219ca70c10fc9a3364bdeba579b6a69394

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
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
<<<<<<< HEAD
        InputManager.Instance.SetInputType("pause");
        startGame = false;
=======
        Score = 0;
        InputManager.Instance.SetInputType("ui");
>>>>>>> 525394219ca70c10fc9a3364bdeba579b6a69394
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< HEAD
        if(startGame)
=======
        if (curSongTimer >= totalSongTimer && !isSongOver)
>>>>>>> 525394219ca70c10fc9a3364bdeba579b6a69394
        {
            if (curSongTimer >= totalSongTimer && !isSongOver)
            {
                isSongOver = true;
                startGame = false;
            }
            else if (curSongTimer < totalSongTimer && !isSongOver)
            {
                curSongTimer += Time.deltaTime;
            }
        }
    }

    public bool IsSongOver()
    {
        return isSongOver;
    }

<<<<<<< HEAD
    public bool StartGame()
    {
        return startGame;
    }

    public void StartTheGame()
    {
        curSongTimer = 0f;
        startGame = true;
    }

    public void ChangeGameStatus()
    {
        startGame = !startGame;
=======
    public void UpdateScore(int modifier)
    {
        Score += modifier;
>>>>>>> 525394219ca70c10fc9a3364bdeba579b6a69394
    }
}
