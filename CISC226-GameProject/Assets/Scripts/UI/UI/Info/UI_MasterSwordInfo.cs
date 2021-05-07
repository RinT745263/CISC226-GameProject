using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_MasterSwordInfo : MonoBehaviour, IPointerClickHandler
{
    private GameObject UI_MSInfo;
    private Animator MSAnim;
    private bool close;

    public static UI_MasterSwordInfo Instance;

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
            closeMSUI();
        }
    }
    
    private void Awake()
    {
        close = true;
        Instance = this;
        UI_MSInfo = GameObject.Find("MasterSwordInfo");
        UI_MSInfo.SetActive(false);
        MSAnim = GetComponent<Animator>();
    }

    public void displayMSUI()
    {
        UI_MSInfo.SetActive(true);
        MSAnim.Play("GeneralOpen");
    }

    public void closeMSUI()
    {
        MSAnim.Play("close");
        StartCoroutine(timera());
    }

    private IEnumerator timera()
    {
        yield return new WaitForSeconds(0.2f);
        UI_MSInfo.SetActive(false);
        PlayerLevelOne.Instance.talking = false;
    }

    private void CloseGame() { }
}
