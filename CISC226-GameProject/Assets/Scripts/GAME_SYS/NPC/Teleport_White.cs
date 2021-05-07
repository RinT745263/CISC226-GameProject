using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport_White : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false);
    }
    private void Disable()
    {
        gameObject.SetActive(false);
    }

    private void DisableNPC()
    {
        Transform[] a = GetComponentsInChildren<Transform>();
        a[1].gameObject.SetActive(false);
    }

    private void AE_Teleport()
    {
        AudioCont.instance.playSound("Teleport");
    }
    void AE_Teleport_PlayerSet()
    {
        AudioCont.instance.playSound("Teleport_PlayerSet");
    }
}
