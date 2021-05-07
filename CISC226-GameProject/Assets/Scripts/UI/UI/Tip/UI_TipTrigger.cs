using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_TipTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Tips") && !PlayerLevelOne.Instance.talking)
        {
            UI_Tip.TInstance.disableAllTips();
            UI_Tip.TInstance.enableTip(collision.name);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Tips"))
        {
            UI_Tip.TInstance.disableTip(collision.name);
        }
    }
}
