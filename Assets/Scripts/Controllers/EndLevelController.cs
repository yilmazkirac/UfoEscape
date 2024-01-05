using UnityEngine;
public class EndLevelController : MonoBehaviour
{
    private void Update()
    {
        if (GameManager.Instance.Player.GetComponent<PlayerMovementController>().CharacterSpeed > 0)
        {
            gameObject.transform.position = new Vector3(0, 0, GameManager.Instance.Player.transform.position.z - 50f);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EndLevel"))
        {
            GameObject stage = other.transform.parent.gameObject;
            GameManager.Instance.SetWay(stage);
            GameManager.Instance.SetStage(stage);
        }
        if (other.CompareTag("pool"))
        {
            other.gameObject.SetActive(false);
            other.gameObject.GetComponent<CionReset>().SetCoinReset();
        }
        if (other.CompareTag("cars"))
        {
            other.transform.parent.GetComponent<CarsController>().ResetCar();
        }
    }
}
