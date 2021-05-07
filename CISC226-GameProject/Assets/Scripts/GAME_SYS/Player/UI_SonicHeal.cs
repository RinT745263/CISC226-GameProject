using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SonicHeal : MonoBehaviour
{
    public static UI_SonicHeal Instance;

    private void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
    }
}
