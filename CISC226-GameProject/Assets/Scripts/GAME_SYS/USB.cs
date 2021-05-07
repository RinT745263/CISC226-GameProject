using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class USB : MonoBehaviour
{
    public static USB instance;
    public float deltaVelocity;
    public float cameraVelocity;
    public Camera cam;

    private bool fade;

    private void Awake()
    {
        instance = this;
        fade = true;
    }
    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        float dis = Mathf.Abs(transform.position.x - Bug.instance.transform.position.x);
        transform.position = Vector3.Lerp(transform.position, Bug.instance.transform.position, Time.deltaTime * deltaVelocity);
        
        if (dis < 5f)
        {
            Time.timeScale = 1f;
            if (fade)
            {
                fade = false;
                SceneFadeInOut.instance.EndScene();
            }
        }
        else if (dis < 6f)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 7f, cameraVelocity * Time.deltaTime);
            Time.timeScale = 0.1f;
        }
    }

    public void EnableStopTime()
    {
        gameObject.SetActive(true);
    }
}
