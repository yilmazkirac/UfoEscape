using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCheck : MonoBehaviour
{
    private void Start()
    {
        Check();
    }
    public void Check()
    {
        if (PlayerPrefs.HasKey("Name"))
        {
            SceneManager.LoadScene("GameScene");
        }
        else
        {
            UIManager.Instance.MainPanel.SetActive(true);
        }
    }
}
