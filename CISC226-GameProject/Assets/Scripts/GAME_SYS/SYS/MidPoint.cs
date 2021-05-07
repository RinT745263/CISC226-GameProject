using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidPoint : MonoBehaviour
{
    public Transform player;
    public Transform boss;
    public Transform cameraPosition;
    public static MidPoint instance;

    private void Awake()
    {
        instance = this;
    }

    public void ChangePosition()
    {
        float p = player.position.x;
        float b = boss.position.x;
        float mid = 0f;
        if (p > b)
        {
            mid = (p - b) / 2 + b;
        }
        else
        {
            mid = (b - p) / 2 + p;
        }
        transform.position = new Vector3(mid, cameraPosition.position.y);
    }
}
