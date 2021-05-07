using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]

public class DialogModel
{
    [HideLabel]
    public NPCConf NPCconf;

    [Required(ErrorMessage = "Say something")]
    [HideLabel, Multiline(4)]
    public string dialogText;
    
    [LabelText("Events")]
    public List<DialogEventModel> dialogEventModels;

    [LabelText("PlayerSelection")]
    public List<DialogSelection> dialogSelections;

}

public enum DialogEvent
{
    [LabelText("Next Dialog")]
    NextDialog,
    [LabelText("Exit Dialog")]
    ExitDialog,
    [LabelText("Jump Dialog")]
    JumpDialog,
    [LabelText("Special Event - Key Input")]
    KeyInput
}

[Serializable]

public class DialogEventModel
{
    [HideLabel, HorizontalGroup("Events"), LabelWidth(25)]
    public DialogEvent dialogEvent;
    [LabelText("Paremeter"), HorizontalGroup("Events")]
    public string args;
}

[Serializable]

public class DialogSelection
{
    [LabelText("Selection Dialog"), MultiLineProperty(2)]
    public string dialogSelection;
    [LabelText("Event")]
    public List<DialogEventModel> dialogEventModels;
}
