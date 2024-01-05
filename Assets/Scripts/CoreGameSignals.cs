using System;
using UnityEngine;

public class CoreGameSignals : MonoBehaviour
{
    public static CoreGameSignals Instance;
    private void Awake()
    {
        if (Instance != null&&Instance!=this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }


    public Action OnRestartGame;
    public Action OnFailedLevel;

 }
