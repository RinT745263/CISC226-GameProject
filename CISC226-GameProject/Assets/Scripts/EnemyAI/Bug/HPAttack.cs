using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPAttack : MonoBehaviour
{

    public Animator camShake;

    private void SelfDisable()
    {
        gameObject.SetActive(false);
    }

    private void HPAttack_Player()
    {
        Player.PInstance.animationCont.SetTrigger("hpAttack");
    }

    private void StartDialog_TakeInUSB()
    {
        UI_Dialog.Instance.dialog(GameManagerLevelTwo.Instance.GetDialogConf_Bug(3), 0);
    }

    private void CameraShake()
    {
        camShake.SetTrigger("shake");
    }


}
