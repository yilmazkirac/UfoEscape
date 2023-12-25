using DG.Tweening;
using System;
using System.Collections;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public GameObject LoginPanel;
    public GameObject MainPanel;
    TextMeshProUGUI warninText;
    CanvasGroup LoginPanelCG;
   public static UIManager Instance;
    private void Awake()
    {
        if (Instance != null&&Instance!=this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    private void Start()
    {
        warninText = LoginPanel.transform.Find("WarningText").gameObject.GetComponent<TextMeshProUGUI>();
        HideScreen(false, true);
        LoginPanelCG = LoginPanel.GetComponent<CanvasGroup>();
    }
   public void HideScreen(bool isLogin,bool isMain)
    {
        LoginPanel.SetActive(isLogin);
        MainPanel.SetActive(isMain);
        warninText.gameObject.SetActive(false);
    }

    public void LoginScreen(string buttonText,string WarningText, Action actions=null)
    {
        HideScreen(false,false);     
        warninText.text = WarningText;

        Transform buttonOK = LoginPanel.transform.Find("Button");
        buttonOK.GetChild(0).GetComponent<TextMeshProUGUI>().text=buttonText;
        buttonOK.GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
        buttonOK.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => { actions?.Invoke(); });       

        LoginPanelCG.alpha = 0f;
        LoginPanel.SetActive(true);
        LoginPanelCG.DOFade(1, .3f).SetEase(Ease.Linear);
    }
    public void Back()
    {
        StartCoroutine(BackBg());
    }
    IEnumerator BackBg()
    {
        LoginPanelCG.alpha = 1f;
        LoginPanelCG.DOFade(0, .3f).SetEase(Ease.Linear);
        yield return new WaitForSeconds(.3f);
        HideScreen(false, true);
    }
}
