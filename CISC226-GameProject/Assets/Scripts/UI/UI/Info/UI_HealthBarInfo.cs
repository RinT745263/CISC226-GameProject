using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_HealthBarInfo : MonoBehaviour, IPointerClickHandler
{
    private GameObject UI_HPInfo;
    private Animator HPanim;
    private bool close;

    public static UI_HealthBarInfo Instance;

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
            closeHPUI();
        }
    }

    // Start is called before the first frame update
    private void Awake()
    {
        close = true;
        Instance = this;
        UI_HPInfo = GameObject.Find("HealthBar");
        UI_HPInfo.SetActive(false);
        HPanim = GetComponent<Animator>();
    }

    public void displayHPUI()
    {
        UI_HPInfo.SetActive(true);
        UI_PlayerHP.Instance.EnableHPFirstLevel();
        HPanim.Play("GeneralOpen");
    }

    public void closeHPUI()
    {
        HPanim.Play("close");
        StartCoroutine(timera());
        UI_Dialog.Instance.dialog(GameManager.Instance.GetDialogConf(5), 0);
    }


    private IEnumerator timera()
    {
        yield return new WaitForSeconds(0.2f);
        UI_HPInfo.SetActive(false);
    }

    private void CloseGame() { }

}