using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_USBTip : MonoBehaviour
{
    public static UI_USBTip instance;

    private void Awake()
    {
        instance = this;  
    }
    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            AudioCont.instance.playSound("Tip");
            Player.PInstance.animationCont.SetTrigger("throwUSB");
            USB.instance.EnableStopTime();
            Time.timeScale = 1f;
            gameObject.SetActive(false);
        }
    }
}
