using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManager2 : MonoBehaviour
{
    public List<string> ScoreInformation = new List<string>();
    public List<GameObject> ScoreList = new List<GameObject>();

    string scroeInformation;
    int counter;

    public static NetworkManager2 Instance;
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
                    for (int i = 0; i < scroeInformation.Split('*').Length; i++)
                    {
                        ScoreInformation.Add(scroeInformation.Split('*')[i]);
                    }

                    for (int j = 0; j < ScoreList.Count; j++)
                    {
                        for (int i = 0; i < ScoreInformation.Count - 1; i+= 2)
                        {                        
                            ScoreList[j].GetComponent<TextMeshProUGUI>().text = ScoreInformation[counter].ToString() + " : " + ScoreInformation[counter + 1].ToString();
                            ScoreList[j].SetActive(true);
                            counter += 2;
                            break;
                        }
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
    

}

