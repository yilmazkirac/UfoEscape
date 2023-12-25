using UnityEngine;

public class UIContr : MonoBehaviour
{
    public GameObject TopListPanel;
    public bool Toggle;

    public void ShowListPanel()
    {
       if (Toggle)
        {
            TopListPanel.SetActive(false);
            Toggle = false;
        }
       else
        {
            TopListPanel.SetActive(true);
            Toggle = true;       
            NetworkManager2.Instance.ShowScore();
            NetworkManager2.Instance.ScoreInformation.Clear();
        }
    }
}
