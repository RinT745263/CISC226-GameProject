using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_USBInfor : MonoBehaviour, IPointerClickHandler
{
    private GameObject USBUI;
    private Animator USBanim;
    private bool close;

    public static UI_USBInfor USBInstance;
    private void AE_Open()
    {
        AudioCont.instance.playSound("UI_Open");
    }

    private void AE_Close()
    {
        AudioCont.instance.playSound("UI_Close");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (close)
        {
            close = false;
            closeUSBUI();
        }
    }

    // Start is called before the first frame update
    private void Awake()
    {
        close = true;
        USBInstance = this;
        USBUI = transform.GetComponent<Transform>().gameObject;
        USBUI.SetActive(false);
        USBanim = GetComponent<Animator>();
    }

    public void displayUSBUI()
    {
        USBUI.SetActive(true);
        USBanim.Play("open");
    }

    public void closeUSBUI() {

        USBanim.Play("close");
        StartCoroutine(timera());
        GameManager.Instance.ThirdSection();
    }
    

    private IEnumerator timera()
    {
        yield return new WaitForSeconds(0.5f);
        USBUI.SetActive(false);
    }
    private void CloseGame() { }



}
