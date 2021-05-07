using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffects_BeAtteckeds : MonoBehaviour
{
    public static PlayerEffects_BeAtteckeds instance;

    private Transform[] effects;
    private void Awake()
    {
        instance = this;
        effects = transform.GetComponentsInChildren<Transform>();
    }
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 1; i < effects.Length; i++)
        {
            effects[i].gameObject.SetActive(false);
        }
    }

    public void EnableRandomEffect()
    {
        effects[Random.Range(1,4)].gameObject.SetActive(true);
    }
}
