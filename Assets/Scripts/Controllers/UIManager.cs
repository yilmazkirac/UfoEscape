using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Panels")]
    public GameObject SingUpPanel;
    public GameObject TopListPanel;
    public GameObject PlayPanel;
    public GameObject EndGamePanel;
    public GameObject InGamePanel;

    [Header("TEXT-------------")]
    public TextMeshProUGUI NickName;
    public TextMeshProUGUI ScoreText;

    [Header("SCORE LIST")]
    public List<GameObject> ScoreList;

    public bool Toggle;
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
        CoreGameSignals.Instance.OnRestartGame += AllPanelDeActive2;
    }

    public void AllPanelDeActive2()
    {
        SingUpPanel.SetActive(false);
        TopListPanel.SetActive(false);
        PlayPanel.SetActive(false);
        EndGamePanel.SetActive(false);
        InGamePanel.SetActive(false);

        PlayPanel.SetActive(true);
    }

    private void UnSubscribe()
    {
        CoreGameSignals.Instance.OnRestartGame -= AllPanelDeActive2;
    }
    private void OnDisable()
    {
        UnSubscribe();
    }



   

   
    private void Start()
    {
        if (!PlayerPrefs.HasKey("Name"))
        {          
            AllPanelDeActive(SingUpPanel);           
        }
        else
        {           
            AllPanelDeActive(PlayPanel);   
        }
    }

    public void AllPanelDeActive(GameObject panel=null, GameObject panel2 = null)
    {
        SingUpPanel.SetActive(false);
        TopListPanel.SetActive(false);
        PlayPanel.SetActive(false);
        EndGamePanel.SetActive(false);
        InGamePanel.SetActive(false);

        
        panel?.SetActive(true);
        panel2?.SetActive(true);
    }
    public void ShowListPanel()
    {
       if (Toggle)
        {
            NetworkManager.Instance.ScoreInformation.Clear();
            TopListPanel.SetActive(false);
            Toggle = false;
        }
       else
        {
            NetworkManager.Instance.ShowScore();
            TopListPanel.SetActive(true);
            Toggle = true;                  
        }
    }
}
