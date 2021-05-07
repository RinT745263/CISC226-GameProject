using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI_Dialog : MonoBehaviour
{
    public static UI_Dialog Instance;
    private Text nameText;
    private Text mainText;
    private RectTransform content;
    private Transform Options;
    private GameObject prefab_OptionItem;
    private GameObject prefab_NextDialog;
    private GameObject mainContent;

    private DialogConf currConf;
    private int currIndex;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        mainContent = GameObject.Find("Main");
        nameText = transform.Find("Main/Name").GetComponent<Text>();
        mainText = transform.Find("Main/Scroll View/Viewport/Content/MainText").GetComponent<Text>();
        content = transform.Find("Main/Scroll View/Viewport/Content").GetComponent<RectTransform>();
        Options = transform.Find("Options");
        prefab_OptionItem = Resources.Load<GameObject>("Options_Item");
        prefab_NextDialog = Resources.Load<GameObject>("NextDialog");
        mainContent.SetActive(false);
    }

    private void StartDialog(DialogConf conf, int index)
    {
        DialogModel model = conf.dialogModels[index];

        // Change the name of speaker
        nameText.text = model.NPCconf.speakerName;
        // Start to talk
        StopAllCoroutines();
        StartCoroutine(DoMainTextEF(model.dialogText));

        // Delete Children of Options
        Transform[] preOps = Options.GetComponentsInChildren<Transform>();
        for (int i = 1; i < preOps.Length; i++)
        {
            Destroy(preOps[i].gameObject);
        }

        // Next Dialog
        for (int i = 0; i < model.dialogEventModels.Count; i++)
        {
            UI_NextDialog preOp1 = GameObject.Instantiate<GameObject>(prefab_NextDialog, Options).GetComponent<UI_NextDialog>();
            preOp1.InitNextDialog(model.dialogEventModels[i]);
        }

        StartCoroutine(delaySeconds(model));
    }

    private IEnumerator delaySeconds(DialogModel model)
    {
        yield return new WaitForSeconds(0.7f);
        // Player Selections
        for (int i = 0; i < model.dialogSelections.Count; i++)
        {
            UI_Options_Item preOp2 = GameObject.Instantiate<GameObject>(prefab_OptionItem, Options).GetComponent<UI_Options_Item>();
            preOp2.InitOpt(model.dialogSelections[i]);
        }
    }

    public void ParseDialogEvent(DialogEvent dialogEvent, string args)
    {
        switch (dialogEvent)
        {
            case DialogEvent.NextDialog:
                NextDialogEvent();
                break;
            case DialogEvent.ExitDialog:
                ExitDialogEvent(args);
                break;
            case DialogEvent.JumpDialog:
                JumpDialogEvent(int.Parse(args));
                break;
            case DialogEvent.KeyInput:
                StartCoroutine(KeyInput(args));
                break;
        }
    }

    private IEnumerator KeyInput(string key)
    {
        yield return new WaitUntil(() => Input.GetKeyDown(key));
        NextDialogEvent();
    }
    // Go to next dialog
    private void NextDialogEvent()
    {
        currIndex += 1;
        StartDialog(currConf, currIndex);
    }
    // End the Conversation
    private void ExitDialogEvent(string section)
    {
        Transform[] preOps = Options.GetComponentsInChildren<Transform>();
        for (int i = 1; i < preOps.Length; i++)
        {
            Destroy(preOps[i].gameObject);
        }
        mainContent.SetActive(false);
        switch (section)
        {
            case "SectionOne":
                GameManager.Instance.FirstSection();
                break;
            case "SectionTwo":
                GameManager.Instance.SecondSection();
                break;
            case "SectionFour":
                GameManager.Instance.ForthSection();
                break;
            case "SectionFive":
                UI_HealthBarInfo.Instance.displayHPUI();
                break;
            case "SectionSix":
                UI_SonicHealInfo.Instance.displaySHUI();
                break;
            case "SectionSeven":
                GameManager.Instance.ExtraSection();
                break;
            case "SectionEight":
                GameManager.Instance.EndGame(1.0f);
                break;
            case "SectionNine":
                UI_SwordofHero.Instance.displaySHUI();
                break;
            case "TheHero":
                Bug.instance.EnableAttackToTheHero();
                break;
            case "afterTakeIn":
                Bug.instance.TakeInFin();
                break;
            case "goon":
                Player.PInstance.animationCont.SetBool("rebirth", true);
                Transform[] a = UI_PlayerHP.Instance.HPs;
                for (int i = 1; i < a.Length; i++)
                {
                    a[i].gameObject.SetActive(true);
                }
                break;
            case "giveup":
                Application.Quit();
                break;
            case "tooManyDeath":
                Bug.instance.DestroyHPSystem();
                break;
            case "StartTakeInUSB":
                Bug.instance.TakeInUSB();
                break;
            case "GivePowerFin":
                NPC.instance.DisableAllNPCs();
                break;
        }
    }

    // After selection, jump the dialog
    private void JumpDialogEvent(int index)
    {
        currIndex = index;
        StartDialog(currConf, currIndex);
    }



    IEnumerator DoMainTextEF(string txt)
    {
        
        float addHeight = txt.Length / 23 + 1;
        content.sizeDelta = new Vector2(content.sizeDelta.x, addHeight*25);

        string currStr ="";
        for (int i = 0; i < txt.Length; i++)
        {
            currStr += txt[i];
            yield return new WaitForSeconds(0.02f);
            mainText.text = currStr;
            if (i>23*3&&i % 23 == 0)
            {
                content.anchoredPosition = new Vector2(content.anchoredPosition.x, content.anchoredPosition.y+25);
            }
        }
    }

    public void dialog(DialogConf conf, int index)
    {
        mainContent.SetActive(true);
        currConf = conf;
        currIndex = index;
        StartDialog(currConf, index);
    }

    public void ExitDialog()
    {
        mainContent.SetActive(false);
        Transform[] preOps = Options.GetComponentsInChildren<Transform>();
        for (int i = 1; i < preOps.Length; i++)
        {
            Destroy(preOps[i].gameObject);
        }
    }
}
