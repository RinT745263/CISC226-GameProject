using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class UI_NextDialog : MonoBehaviour, IPointerClickHandler
{
    private DialogEventModel selection;

    public void OnPointerClick(PointerEventData eventData)
    {
        UI_Dialog.Instance.ParseDialogEvent(selection.dialogEvent, selection.args);
    }

    public void InitNextDialog(DialogEventModel model)
    {
        selection = model;
    }
}
