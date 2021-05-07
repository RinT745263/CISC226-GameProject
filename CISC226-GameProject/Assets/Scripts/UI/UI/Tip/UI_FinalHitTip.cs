using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_FinalHitTip : MonoBehaviour
{
    public static UI_FinalHitTip instance;

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
            Time.timeScale = 1f;
            Player.PInstance.animationCont.SetBool("final", true);
            gameObject.SetActive(false);
        }
    }
}
