using UnityEngine;

public class CionReset : MonoBehaviour
{
   [SerializeField] private GameObject[] coins;
    [SerializeField] private Transform[] points;
    private void Start()
    {
        SetReset();
    }
    public void SetReset()
    {
        for (int i = 0; i < points.Length; i++)
        {   
            coins[i].transform.position= points[i].transform.position;
            coins[i].SetActive(true);
        }
    }
}
