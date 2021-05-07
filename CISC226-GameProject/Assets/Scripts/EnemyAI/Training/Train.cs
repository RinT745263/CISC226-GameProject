using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    public static Train instance;

    private bool turnLeft;
    private float CD;
    private Animator anim;

    private void Awake()
    {
        turnLeft = false;
        instance = this;
        anim = transform.GetComponent<Animator>();
    }

    private void Update()
    {
        if (CD > 0)
        {
            CD -= Time.deltaTime;
            if (turnLeft)
            {
                anim.SetInteger("direction", 1);
            }
            else
            {
                anim.SetInteger("direction", 2);
            }
        }
        else
        {
            anim.SetInteger("direction", 0);
        }
    }

    public void ChangeDirection(float tartPosi)
    {
        turnLeft =  tartPosi <= -14.21f;
        CD = 0.4f;
    }

}
