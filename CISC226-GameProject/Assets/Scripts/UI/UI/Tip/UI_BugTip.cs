using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UI_BugTip : MonoBehaviour
{
    public Transform Trigger;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            StopAllCoroutines();
            SceneFadeInOut.instance.EndScene();
            AudioCont.instance.playSound("Tip");
            Destroy(Trigger.gameObject);
            Destroy(gameObject);
        }
    }
}
