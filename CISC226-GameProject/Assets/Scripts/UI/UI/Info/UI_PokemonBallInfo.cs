using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_PokemonBallInfo : MonoBehaviour, IPointerClickHandler
{
    private GameObject UI_PBInfo;
    private Animator PBAnim;
    private bool close;

    public static UI_PokemonBallInfo Instance;

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
            closePBUI();
        }
    }

    private void Awake()
    {
        close = true;
        Instance = this;
        UI_PBInfo = GameObject.Find("PokemonBallInfo");
        UI_PBInfo.SetActive(false);
        PBAnim = GetComponent<Animator>();
    }

    public void displayPBUI()
    {
        UI_PBInfo.SetActive(true);
        PBAnim.Play("GeneralOpen");
    }

    public void closePBUI()
    {
        PBAnim.Play("close");
        StartCoroutine(timera());
    }


    private IEnumerator timera()
    {
        yield return new WaitForSeconds(0.2f);
        UI_PBInfo.SetActive(false);
        PlayerLevelOne.Instance.talking = false;
    }

    private void CloseGame() { }
}
