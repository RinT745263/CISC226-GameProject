using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_PlayerHPDisable : MonoBehaviour
{
    public static UI_PlayerHPDisable Instance;
    public Transform[] PlayerHPDisables;

    private void Awake()
    {
        Instance = this;
        PlayerHPDisables = transform.GetComponentsInChildren<Transform>();

        if (SceneManager.GetActiveScene().name == "Village")
        {
            for (int i = 1; i < PlayerHPDisables.Length; i++)
            {
                PlayerHPDisables[i].gameObject.SetActive(false);
            }
        }
    }

    public void EnableAll()
    {
        for (int i = 1; i < PlayerHPDisables.Length; i++)
        {
            PlayerHPDisables[i].gameObject.SetActive(true);
        }
    }
}
