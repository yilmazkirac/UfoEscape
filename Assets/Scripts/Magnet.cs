using System.Collections;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private void Start()
    {
        StartCoroutine(MagnetTimer());
    }

    private void Update()
    {
        if (gameObject.activeSelf)
          gameObject.transform.position = player.transform.position;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            other.transform.position = Vector3.Lerp(other.transform.position, gameObject.transform.position, .3f);
        }
    }

  IEnumerator MagnetTimer()
    {
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);
    }
}
