using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_LinkTip : MonoBehaviour
{
    public bool disable;
    public GameObject NPC;
    public static UI_LinkTip Instance;

    private void Awake()
    {
        Instance = this;
        disable = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (disable)
        {
            NPC.SetActive(false);
        }
        else if (UI_Tip.TInstance.numInterac == 5)
        {
            NPC.SetActive(true);
        }
    }
}
