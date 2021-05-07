using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEditor;

public class UI_End : MonoBehaviour, IPointerClickHandler
{
    private Animator UI_EndAnim;
    private bool close;

    private void Awake()
    {
        close = true;
        UI_EndAnim = GetComponent<Animator>();
    }

    private void AE_Open()
    {
        AudioCont.instance.playSound("UI_Open");
    }

    private void AE_Close()
    {
        AudioCont.instance.playSound("UI_Close");
    }

    private void CloseGame()
    {
        Application.Quit();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (close)
        {
            close = false;
            UI_EndAnim.SetTrigger("close");
        }
    }

}
