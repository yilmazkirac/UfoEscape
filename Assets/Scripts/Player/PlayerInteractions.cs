using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] private GameManager manager;
    [SerializeField] private GameObject magnet;
  
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + manager.CoinPrice);
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
            if (manager.Life < 2)
            {
                manager.Player.GetComponent<PlayerMovementController>().CharacterSpeed = 0;
                PlayerPrefs.SetInt("puan", manager.Scoreint);           
                NetworkManager2.Instance.ScoringSend();
                manager.RestartPanel.SetActive(true);
                for (int i = 0; i < manager.ObjePool.Length; i++)
                {
                    manager.ObjePool[i].transform.Find("Cars").transform.GetComponent<CarsController>().ResetCar();
                    manager.ObjePool[i].gameObject.SetActive(false);
                }              
            }
            else if (manager.Life > 1)
            {
                manager.Life -= 1;
                manager.Player.GetComponent<PlayerMovementController>().CharCont.Move(new Vector3( 0, 0, 100) * Time.deltaTime);
                manager.ReLife();
            }
        }

    }
}
