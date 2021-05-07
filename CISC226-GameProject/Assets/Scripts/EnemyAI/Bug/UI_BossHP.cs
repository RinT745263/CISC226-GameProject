using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_BossHP : MonoBehaviour
{
    public Image redHP;
    public Image whiteHP;
    public float decreaseCD;
    private float totalHP;
    private bool coolDown;
    private float CD;

    private void Awake()
    {
        if (decreaseCD == 0f)
        {
            decreaseCD = 0.8f;
        }
        coolDown = false;
    }

    private void Start()
    {
        CD = decreaseCD;
        totalHP = Bug.instance.HP;
    }

    // Update is called once per frame
    void Update()
    {
        redHP.fillAmount = Bug.instance.HP / totalHP;

        if (Bug.instance.HP / totalHP <= whiteHP.fillAmount)
        {
            coolDown = true;
            if (CD <= 0f)
            {
                whiteHP.fillAmount = (whiteHP.fillAmount -= 0.5f * Time.deltaTime) * whiteHP.fillAmount / whiteHP.fillAmount;
            }
        }
        else
        {
            coolDown = false;
            CD = decreaseCD;
        }

        if (coolDown)
        {
            CD -= Time.deltaTime;
        }
    }
}
