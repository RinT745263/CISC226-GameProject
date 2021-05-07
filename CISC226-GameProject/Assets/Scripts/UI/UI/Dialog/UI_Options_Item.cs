using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Options_Item : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
{
    private Image image;
    private Text text;
    private bool isSelect;
    private bool firstSelect;

    private Color lightColor = new Color32(25, 126, 164, 98);
    private Color deepColor = new Color32(2, 23, 42, 119);

    private DialogSelection selection;


    public bool IsSelect { 
        get => isSelect;
        set
        {
            isSelect = value;
            if (isSelect)
            {
                image.color = lightColor;
            }
            else
            {
                image.color = deepColor;
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        IsSelect = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        IsSelect = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        AudioCont.instance.playSound("Tip");
        if (SceneManager.GetActiveScene().name != "Bug" || UI_PlayerHP.Instance.GoodEndSteps >= 0 || selection.dialogEventModels[0].args == "giveup")
        {
            for (int i = 0; i < selection.dialogEventModels.Count; i++)
            {
                UI_Dialog.Instance.ParseDialogEvent(selection.dialogEventModels[i].dialogEvent, selection.dialogEventModels[i].args);
            }
        }
        else
        {
            if (firstSelect)
            {
                firstSelect = false;
                ErrorMessages.instance.EnableErrorMessages();
                StartCoroutine(Wait());
            }
        }
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(3f);
        ErrorMessages.instance.DisableErrorMessages();
        UI_Dialog.Instance.ExitDialog();
        UI_Dialog.Instance.dialog(GameManagerLevelTwo.Instance.GetDialogConf_Bug(2), 0);
    }
    

    public void InitOpt(DialogSelection model)
    {
        image = GetComponent<Image>();
        text = transform.Find("Text").GetComponent<Text>();
        selection = model;
        text.text = model.dialogSelection;

        IsSelect = false;
        firstSelect = true;

    }


}
