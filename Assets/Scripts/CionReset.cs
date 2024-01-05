using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class CionReset : MonoBehaviour
{
    [SerializeField] private GameObject[] coins;
    [SerializeField] private Transform[] points;
    private void Start()
    {
        SetCoinReset();
    }
    public void SetCoinReset()
    {
        for (int i = 0; i < points.Length; i++)
        {
            coins[i].transform.position = points[i].transform.position;
            coins[i].SetActive(true);
        }
    }
}
