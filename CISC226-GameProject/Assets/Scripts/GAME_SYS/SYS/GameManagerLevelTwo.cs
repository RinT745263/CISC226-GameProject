using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class GameManagerLevelTwo : MonoBehaviour
{
    public static GameManagerLevelTwo Instance;

    private DialogConf[] dialogConfs1;
    private DialogConf[] dialogConfs2;

    void Awake()
    {
        Instance = this;
        dialogConfs1 = Resources.LoadAll<DialogConf>("LevelTwo/TheHero");
        dialogConfs2 = Resources.LoadAll<DialogConf>("LevelTwo/Bug");
    }

    public DialogConf GetDialogConf_TheHero(int index)
    {
        return dialogConfs1[index];
    }

    public DialogConf GetDialogConf_Bug(int index)
    {
        return dialogConfs2[index];
    }


}
