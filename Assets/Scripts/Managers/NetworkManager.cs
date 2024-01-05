using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManager : MonoBehaviour
{
    [Header("GET SCORE")]
    public List<string> ScoreInformation;
    string scroeInformation;
    int counter;

    public static NetworkManager Instance;
    private void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void ScoringSend()
    {
        StartCoroutine(ScoringSendCt());
    }
 

    public void ShowScore()
    {
        counter = 0;
        StartCoroutine(ShowScoreCt());
    }

    public void SignUp()
    {
        StartCoroutine(SignUpCt());
    }
    IEnumerator ShowScoreCt()
    {
        using (UnityWebRequest request = UnityWebRequest.Get("runner44.000webhostapp.com/siralama.php"))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                scroeInformation = request.downloadHandler.text;
                Debug.Log(scroeInformation);
                if (scroeInformation != "")
                {
                    for (int i = 0; i < scroeInformation.Split('*').Length-1; i++)
                    {
                        ScoreInformation.Add(scroeInformation.Split('*')[i]);
                    }
                    Debug.Log(scroeInformation);


                    for (int j = 0; j <UIManager.Instance.ScoreList.Count; j++)
                    {
                        UIManager.Instance.ScoreList[j].GetComponent<TextMeshProUGUI>().text = ScoreInformation[counter].ToString() + " : " + ScoreInformation[counter + 1].ToString();
                        UIManager.Instance.ScoreList[j].SetActive(true);
                            counter += 2;                     
                    }
                }
                else
                {
                    Debug.Log("Not Connect");
                }
            }
            else
            {
                Debug.Log("Failed");
            }
        }
    }
    IEnumerator ScoringSendCt()
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("kullaniciadi", PlayerPrefs.GetString("Name")));
        formData.Add(new MultipartFormDataSection("puan", PlayerPrefs.GetInt("puan").ToString()));

        using (UnityWebRequest request = UnityWebRequest.Post("runner44.000webhostapp.com/PuanSet.php", formData))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                if (request.downloadHandler.text == "basarili")
                {
                    Debug.Log(request.downloadHandler.text);
                }
                else if (request.downloadHandler.text == "basarisiz")
                {
                    Debug.Log("basarisiz");
                }
            }
            else
            {
                Debug.Log("Failed");
            }
        }
    }

    IEnumerator SignUpCt()
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();

        formData.Add(new MultipartFormDataSection("kullaniciadi", UIManager.Instance.NickName.text));      
        formData.Add(new MultipartFormDataSection("puan", 0.ToString()));
        using (UnityWebRequest request = UnityWebRequest.Post("runner44.000webhostapp.com/index.php", formData))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                if (request.downloadHandler.text == "basarili")
                {
                    PlayerPrefs.SetString("Name", UIManager.Instance.NickName.text);
                    UIManager.Instance.AllPanelDeActive(UIManager.Instance.PlayPanel);
                   
                    Debug.Log("Correct");
                }
                else if (request.downloadHandler.text == "basarisiz")
                {                    
                    Debug.Log("Not Connected");
                }
            }
            else
            {              
                Debug.Log("Failed");
            }
        }
    }
}

