using UnityEngine;

public class EndLevelController : MonoBehaviour
{
    public GameManager GameManager;

    private void Update()
    {
        if (GameManager.Player.GetComponent<PlayerMovementController>().CharacterSpeed>0)
        {
            gameObject.transform.position = new Vector3(0,0, GameManager.Player.transform.position.z-20f);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EndLevel"))
        {
            GameObject stage = other.transform.parent.gameObject;
            GameManager.SetWay(stage);
            GameManager.SetStage(stage);
        
        }
        if (other.CompareTag("pool"))
        {
            other.gameObject.SetActive(false);
            other.gameObject.GetComponent<CionReset>().SetReset();
        }

        if (other.CompareTag("cars"))
        {
            other.transform.parent.GetComponent<CarsController>().ResetCar();
        }
    }
}
