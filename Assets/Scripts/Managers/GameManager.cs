using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("SCORE--------------")]
    public int CoinPrice;
    public int Scoreint;
    private float Score;

    [Header("POOLS--------------")]
    public GameObject[] ObjePool;
    public GameObject[] WayPool;

    [Header("PLAYER--------------")]
    public GameObject Player;
    private void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void OnEnable()
    {
        Subscribe();
    }
    private void Subscribe()
    {
        CoreGameSignals.Instance.OnRestartGame += Res;
    }
    private void Res()
    {
        Score = 0;
        PlayerPrefs.SetInt("Score", 0);

        WayPool[0].transform.position = new Vector3(0, 0, 0);
        WayPool[1].transform.position = new Vector3(0, 0, 100.7f);
        WayPool[2].transform.position = new Vector3(0, 0, 201.4f);

        SetStage(WayPool[0]);
        SetStage(WayPool[1]);
        SetStage(WayPool[2]);

        foreach (var item in ObjePool)
        {
            item.GetComponent<CionReset>().SetCoinReset();
        }
    }
    private void UnSubscribe()
    {
        CoreGameSignals.Instance.OnRestartGame -= Res;
    }
    private void OnDisable()
    {
        UnSubscribe();
    }
    private void Start()
    {
        Restart();
    }

    private void Update()
    {
        UpdateScore();
    }
    private void UpdateScore()
    {
        if (Player.GetComponent<PlayerMovementController>().CharacterSpeed > 0)
        {
            Score += 30 * Time.deltaTime;
            Scoreint = (int)Score + PlayerPrefs.GetInt("Score");
            UIManager.Instance.ScoreText.text = "Score : " + Scoreint.ToString();
        }
    }
    public void Restart()
    {
       CoreGameSignals.Instance.OnRestartGame?.Invoke();
    }

    public void SetWay(GameObject stage)
    {
        stage.transform.position = new Vector3(stage.transform.position.x, stage.transform.position.y, stage.transform.position.z + 302.1f);
    }

    public void SetStage(GameObject stage)
    {
        for (int i = 0; i < 2; i++)
        {
            foreach (GameObject obj in ObjePool)
            {
                if (!obj.activeInHierarchy)
                {
                    obj.transform.position = stage.transform.Find("Cube" + i).position;
                    obj.SetActive(true);
                    break;
                }
            }
        }
    }
    public void StartGame()
    {
        Player.GetComponent<PlayerMovementController>().CharacterSpeed = 15;
        UIManager.Instance.AllPanelDeActive(UIManager.Instance.InGamePanel);
    }
}


