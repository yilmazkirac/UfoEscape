using System;
using UnityEngine;

public class CarsController : MonoBehaviour
{
    public bool Move;
    public Transform FirstPos;

    public void ResetCar()
    {
        Move = false;
        transform.position = FirstPos.position;
    }

    private void Update()
    {
        if (Move)
        {
            transform.position += new Vector3(0, 0, -1) * Time.deltaTime * 20;
        }      
    }
}
