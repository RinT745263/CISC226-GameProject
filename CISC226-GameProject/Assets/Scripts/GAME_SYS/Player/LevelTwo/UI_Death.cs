using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Death : MonoBehaviour
{
    public static UI_Death instance;
    private void Awake()
    {
        instance = this;
        gameObject.SetActive(false);
    }

    private void StartRebirth()
    {
        UI_Dialog.Instance.dialog(GameManagerLevelTwo.Instance.GetDialogConf_Bug(1),0);
    }

    private void DeathSound()
    {
        AudioCont.instance.playSound("deathEffect");
    }
    private void Disable()
    {
        gameObject.SetActive(false);
    }
}
