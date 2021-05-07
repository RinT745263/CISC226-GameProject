using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_SonicHealInfo : MonoBehaviour, IPointerClickHandler
{
    private GameObject UI_SHInfo;
    private Animator SonicHealanim;
    private bool close;

    public static UI_SonicHealInfo Instance;
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

    // Start is called before the first frame update
    private void Awake()
    {
        close = true;
        Instance = this;
        UI_SHInfo = transform.GetComponent<Transform>().gameObject;
        UI_SHInfo.SetActive(false);
        SonicHealanim = GetComponent<Animator>();
    }

    public void displaySHUI()
    {
        UI_PlayerHP.Instance.EnableSonicHeal();
        UI_SHInfo.SetActive(true);
        SonicHealanim.Play("GeneralOpen");
    }

    public void closeSHUI()
    {
        SonicHealanim.Play("close");
        StartCoroutine(timera());
        UI_Dialog.Instance.dialog(GameManager.Instance.GetDialogConf(6), 0);
    }


    private IEnumerator timera()
    {
        yield return new WaitForSeconds(0.2f);
        UI_SHInfo.SetActive(false);
    }


    private void CloseGame() { }

}
