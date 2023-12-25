using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviour
{

    public TMP_InputField NickName;
    public TMP_InputField Password;
    public List<string> Information = new List<string>();
    string CharacterInformation;

    public SceneCheck sceneCheck;

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

    public void SignUp()
    {
        StartCoroutine(SignUpCt());
    }
 
    public void Login()
    {
        StartCoroutine(LoginCt());
    }

  
    IEnumerator SignUpCt()
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();

        formData.Add(new MultipartFormDataSection("kullaniciadi", NickName.text));
        formData.Add(new MultipartFormDataSection("sifre", Password.text));
        formData.Add(new MultipartFormDataSection("puan", 0.ToString()));
        using (UnityWebRequest request = UnityWebRequest.Post("runner44.000webhostapp.com/index.php", formData))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                if (request.downloadHandler.text == "basarili")
                {
                    PlayerPrefs.SetString("Name", NickName.text);
                    UIManager.Instance.LoginPanel.transform.Find("WarningText").gameObject.SetActive(false);
                  
                    sceneCheck.Check();


                }
                else if (request.downloadHandler.text == "basarisiz")
                {
                    UIManager.Instance.LoginPanel.transform.Find("WarningText").gameObject.SetActive(true);
                }
            }
            else
            {

                UIManager.Instance.LoginPanel.transform.Find("WarningText").gameObject.SetActive(true);
            }
        }
    }

    IEnumerator LoginCt()
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();

        formData.Add(new MultipartFormDataSection("kullaniciadi", NickName.text));
        formData.Add(new MultipartFormDataSection("sifre", Password.text));

        using (UnityWebRequest request = UnityWebRequest.Post("runner44.000webhostapp.com/asd.php", formData))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                CharacterInformation = request.downloadHandler.text;
                if (CharacterInformation != "")
                {
                    UIManager.Instance.LoginPanel.transform.Find("WarningText").gameObject.SetActive(false);
                    for (int i = 0; i < CharacterInformation.Split('*').Length; i++)
                    {
                        Information.Add(CharacterInformation.Split('*')[i]);
                    }
                    PlayerPrefs.SetString("Name", Information[0]);
                    sceneCheck.Check();
                }
                else
                {
                    UIManager.Instance.LoginPanel.transform.Find("WarningText").gameObject.SetActive(true);
                }
            }
            else
            {
                UIManager.Instance.LoginPanel.transform.Find("WarningText").GetComponent<TextMeshProUGUI>().text = "Sunucuya Baglanilamiyor...";
                UIManager.Instance.LoginPanel.transform.Find("WarningText").gameObject.SetActive(true);
            }
        }
    }

}

