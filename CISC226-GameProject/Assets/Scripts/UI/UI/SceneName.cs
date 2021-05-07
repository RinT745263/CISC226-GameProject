using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneName : MonoBehaviour
{
    public static SceneName instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Village")
        {
            gameObject.SetActive(false);
        }
    }
    private void Disable()
    {
        if (SceneManager.GetActiveScene().name == "Village")
        {
            PlayerLevelOne.Instance.talking = false;
        }
        gameObject.SetActive(false);
    }
}
