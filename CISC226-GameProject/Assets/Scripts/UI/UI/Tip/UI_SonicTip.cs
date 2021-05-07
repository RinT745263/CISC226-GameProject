using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SonicTip : MonoBehaviour
{
    public bool disable;
    public GameObject NPC;
    public static UI_SonicTip Instance;

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
    }
}
