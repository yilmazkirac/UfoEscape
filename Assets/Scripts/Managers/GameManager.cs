using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int CoinPrice;
    public GameObject RestartPanel;
    public GameObject Player;
    public GameObject Cam;
    public GameObject GamePanel;
    public int Life;
    public int Scoreint;
    public GameObject[] ObjePool;
    public GameObject[] WayPool;
    public GameObject SpawnPoint;


    [SerializeField] private TextMeshProUGUI scoreText;
    private float Score;

    private void Start()
    {
        Score = 0;
        PlayerPrefs.SetInt("Score", 0);
        Life = 2;
        Restart();
    }

    private void Update()
    {
        if (Player.GetComponent<PlayerMovementController>().CharacterSpeed > 0)
        {
            Score += 30 * Time.deltaTime;
            Scoreint = (int)Score + PlayerPrefs.GetInt("Score");
            scoreText.text = "Score : " + Scoreint.ToString();
        }
    }

    public void Restart()
    {
        RestartPanel.SetActive(false);
        GamePanel.SetActive(true);
        Score = 0;
        PlayerPrefs.SetInt("Score", 0);
        Life = 2;
        Player.GetComponent<PlayerMovementController>().wayIndex = 1;
        Player.GetComponent<PlayerMovementController>().CharacterSpeed = 0;
        Player.transform.position = SpawnPoint.transform.position;
        Cam.GetComponent<CameraController>().isFirstScene = false;
        WayPool[0].transform.position = new Vector3(0, 0, 0);
        WayPool[1].transform.position = new Vector3(0, 0, 100.7f);
        WayPool[2].transform.position = new Vector3(0, 0, 201.4f);
        SetStage(WayPool[0]);
        SetStage(WayPool[1]);
        SetStage(WayPool[2]);
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

    public void ReLife()
    {
        StartCoroutine(ReLifeIE());
    }

    IEnumerator ReLifeIE()
    {
        yield return new WaitForSeconds(5f);
        Life = 2;
    }
    public void StartGame()
    {
        Player.GetComponent<PlayerMovementController>().CharacterSpeed = 5;
        Invoke("Speed", 0.4f);
        Cam.GetComponent<CameraController>().isFirstScene = true;
        GamePanel.gameObject.SetActive(false);
    }
    void Speed()
    {
        Player.GetComponent<PlayerMovementController>().CharacterSpeed = 15;
    }
}


