using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    
    [SerializeField] private GameObject magnet;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + GameManager.Instance.CoinPrice);
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("Magnet"))
        {
            magnet.gameObject.SetActive(true);
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("JetPack"))
        {
            gameObject.GetComponentInParent<PlayerMovementController>().isJetpack = true;
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("cars"))
        {
            other.transform.parent.GetComponent<CarsController>().Move = true;
        }

        if (other.gameObject.CompareTag("Obstacle"))
        {
            gameObject.GetComponentInParent<PlayerMovementController>().CharacterSpeed = 0;
            PlayerPrefs.SetInt("puan", GameManager.Instance.Scoreint);
            NetworkManager.Instance.ScoringSend();
            UIManager.Instance.AllPanelDeActive(UIManager.Instance.EndGamePanel);
 

            for (int i = 0; i < GameManager.Instance.ObjePool.Length; i++)
            {
                GameManager.Instance.ObjePool[i].transform.Find("Cars").transform.GetComponent<CarsController>().ResetCar();
                GameManager.Instance.ObjePool[i].gameObject.SetActive(false);
            }
        }
    }
}
