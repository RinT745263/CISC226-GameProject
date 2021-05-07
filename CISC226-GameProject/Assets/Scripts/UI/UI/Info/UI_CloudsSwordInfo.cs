using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_CloudsSwordInfo : MonoBehaviour, IPointerClickHandler
{
    private GameObject UI_CSInfo;
    private Animator CSAnim;
    private bool close;

    public static UI_CloudsSwordInfo Instance;
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
            closeCSUI();
        }
    }

    private void Awake()
    {
        close = true;
        Instance = this;
        UI_CSInfo = GameObject.Find("CloudsSwordInfo");
        UI_CSInfo.SetActive(false);
        CSAnim = GetComponent<Animator>();
    }

    public void displayCSUI()
    {
        UI_CSInfo.SetActive(true);
        CSAnim.Play("GeneralOpen");
    }

    public void closeCSUI()
    {
        CSAnim.Play("close");
        StartCoroutine(timera());
    }


    private IEnumerator timera()
    {
        yield return new WaitForSeconds(0.2f);
        UI_CSInfo.SetActive(false);
        PlayerLevelOne.Instance.talking = false;
    }


    private void CloseGame() { }
}
