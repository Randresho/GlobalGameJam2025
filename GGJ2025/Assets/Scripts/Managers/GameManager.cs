using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float totalSongTimer;
    public float curSongTimer;

    [SerializeField]
    private bool isSongOver;

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
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Score = 0;
        InputManager.Instance.SetInputType("ui");
    }

    // Update is called once per frame
    void Update()
    {
        if (curSongTimer >= totalSongTimer && !isSongOver)
        {
            isSongOver = true;
        }
        else if (curSongTimer < totalSongTimer && !isSongOver)
        {
            curSongTimer += Time.deltaTime;
        }
    }

    public bool IsSongOver()
    {
        return isSongOver;
    }

    public void UpdateScore(int modifier)
    {
        Score += modifier;
    }
}
