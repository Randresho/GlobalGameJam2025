using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float totalSongTimer;
    public float curSongTimer;
    [SerializeField] private bool isSongOver;
    [SerializeField] private bool startGame;

    private void Awake()
    {
        if(instance == null && instance != this) 
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
        InputManager.Instance.SetInputType("pause");
        startGame = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(startGame)
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
    }
}
