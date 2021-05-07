using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport_BlackLigth : MonoBehaviour
{
    public static Teleport_BlackLigth instance;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }

    private void ChangeCameraTarget()
    {
        CamCont.instance.ChangeTarget(Bug.instance.transform);
    }

    private void BugAnimation_TakeIn()
    {
        Bug.instance.TakeInBegin();
    }

}
