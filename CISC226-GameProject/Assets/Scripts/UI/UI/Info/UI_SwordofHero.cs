using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_SwordofHero : MonoBehaviour, IPointerClickHandler
{
    private GameObject UI_SHInfo;
    private Animator SHAnim;
    private bool close;

    public static UI_SwordofHero Instance;

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
            closeSHUI();
        }
    }

    private void Awake()
    {
        close = true;
        Instance = this;
        UI_SHInfo = GameObject.Find("SwordofHeroInfo");
        UI_SHInfo.SetActive(false);
        SHAnim = GetComponent<Animator>();
    }

    public void displaySHUI()
    {
        UI_SHInfo.SetActive(true);
        SHAnim.Play("GeneralOpen");
    }

    public void closeSHUI()
    {
        SHAnim.Play("close");
        StartCoroutine(timera());
    }

    private IEnumerator timera()
    {
        yield return new WaitForSeconds(0.2f);
        UI_SHInfo.SetActive(false);
        PlayerLevelOne.Instance.Teleportation();
    }

    private void CloseGame() { }
}
