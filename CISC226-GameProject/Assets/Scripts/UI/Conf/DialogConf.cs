using System.Collections;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialog", menuName = "DialogCong/New Dialog")]

public class DialogConf: ScriptableObject
{
    [LabelText("Dialogs")]
    [ListDrawerSettings(ShowIndexLabels = true, AddCopiesLastElement = true)]
    public List<DialogModel> dialogModels;
}
