using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Tip : MonoBehaviour
{
    private Transform[] tips;

    public static UI_Tip TInstance;
    public int numInterac;
    public LinkedList<GameObject> tipList;
    public Animator linkAnim;

    private void Awake()
    {
        TInstance = this;
    }

    void Start()
    {
        numInterac = 0;
        disableAllTips();
    }

    public void disableAllTips()
    {
        tips = transform.GetComponentsInChildren<Transform>();
        for (int i = 1; i < tips.Length; i++)
        {
            tips[i].gameObject.SetActive(false);
        }
    }

    public void enableTip(string name)
    {
        switch (name)
        {
            case "Sonic":
                UI_SonicTip.Instance.NPC.SetActive(true);
                break;
            case "MasterSword":
                UI_MasterSwordTip.Instance.NPC.SetActive(true);
                break;
            case "CloudsSword":
                UI_CloudsSwordTip.Instance.NPC.SetActive(true);
                break;
            case "PokemonBall":
                UI_PokemonBallTip.Instance.NPC.SetActive(true);
                break;
            case "Mario":
                UI_MarioTip.Instance.NPC.SetActive(true);
                break;
        }
    }

    public void disableTip(string name)
    {
        switch (name)
        {
            case "Sonic":
                UI_SonicTip.Instance.NPC.SetActive(false);
                break;
            case "MasterSword":
                UI_MasterSwordTip.Instance.NPC.SetActive(false);
                break;
            case "CloudsSword":
                UI_CloudsSwordTip.Instance.NPC.SetActive(false);
                break;
            case "PokemonBall":
                UI_PokemonBallTip.Instance.NPC.SetActive(false);
                break;
            case "Mario":
                UI_MarioTip.Instance.NPC.SetActive(false);
                break;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            displayUI();
        }
    }

    // Determine which dialog will be using.
    private void displayUI()
    {
        Transform[] dialogs = transform.GetComponentsInChildren<Transform>();
        if (dialogs.Length > 1)
        {
            switch (dialogs[1].name)
            {
                case "Mario":
                    AudioCont.instance.playSound("Tip");
                    numInterac++;
                    PlayerLevelOne.Instance.talking = true;
                    UI_MarioTip.Instance.disable = true;
                    UI_Dialog.Instance.dialog(GameManager.Instance.GetDialogConf(3), 0);
                    break;
                case "Sonic":
                    AudioCont.instance.playSound("Tip");
                    numInterac++;
                    PlayerLevelOne.Instance.talking = true;
                    UI_SonicTip.Instance.disable = true;
                    UI_Dialog.Instance.dialog(GameManager.Instance.GetDialogConf(4), 0);
                    break;
                case "MasterSword":
                    AudioCont.instance.playSound("Tip");
                    numInterac++;
                    PlayerLevelOne.Instance.talking = true;
                    UI_MasterSwordTip.Instance.disable = true;
                    UI_MasterSwordInfo.Instance.displayMSUI();
                    break;
                case "CloudSword":
                    AudioCont.instance.playSound("Tip");
                    numInterac++;
                    PlayerLevelOne.Instance.talking = true;
                    UI_CloudsSwordTip.Instance.disable = true;
                    UI_CloudsSwordInfo.Instance.displayCSUI();
                    break;
                case "PokeBall":
                    AudioCont.instance.playSound("Tip");
                    numInterac++;
                    PlayerLevelOne.Instance.talking = true;
                    UI_PokemonBallTip.Instance.disable = true;
                    UI_PokemonBallInfo.Instance.displayPBUI();
                    break;
                case "Link":
                    if (PlayerLevelOne.Instance.transform.position.x - UI_LinkTip.Instance.transform.position.x < 3.5f)
                    {
                        AudioCont.instance.playSound("Tip");
                        linkAnim.SetTrigger("music");
                        GameManager.Instance.linkBGM.mute = true;
                        GameManager.Instance.levelOneBGM.volume = 1.0f;
                        PlayerLevelOne.Instance.talking = true;
                        UI_LinkTip.Instance.disable = true;
                        UI_Dialog.Instance.dialog(GameManager.Instance.GetDialogConf(7), 0);
                    }
                    break;
            }
        }
    }
}
