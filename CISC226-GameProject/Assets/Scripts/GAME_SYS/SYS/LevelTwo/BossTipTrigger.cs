using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTipTrigger : MonoBehaviour
{
    public Transform bossTip;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Tips") && bossTip != null)
        {
            bossTip.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Tips") && bossTip != null)
        {
            bossTip.gameObject.SetActive(false);
        }
    }
}
