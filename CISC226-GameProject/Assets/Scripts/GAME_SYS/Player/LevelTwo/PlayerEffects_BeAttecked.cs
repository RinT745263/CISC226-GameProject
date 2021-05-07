using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffects_BeAttecked : MonoBehaviour
{
    public static PlayerEffects_BeAttecked instance;

    private void Awake()
    {
        instance = this;
    }

    private void Disable()
    {
        AudioListener.volume = 1f;
        Time.timeScale = 1f;
        Player.PInstance.animationCont.SetBool("beAttacked", false);
        gameObject.SetActive(false);
    }
}
