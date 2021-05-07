using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamCont : MonoBehaviour
{
    public static CamCont instance;

    // Following this object
    public Transform tar;
    // How smooth the camera follows the object
    public float smoothing;

    public Vector2 leftPosition;
    public Vector2 rightPosition;

    private Vector3 tarPosi;

    private void Awake()
    {
        instance = this;
        tarPosi.y = leftPosition.y;
    }

    private void FixedUpdate()
    {
        tarPosi.x = Mathf.Clamp(tar.position.x, leftPosition.x, rightPosition.x);

        // Moving towards the target position smoothly
        transform.position = Vector3.Lerp(transform.position, tarPosi, Time.deltaTime * smoothing);
    }

    public void ChangeTarget(Transform target)
    {
        tar = target;
    }

}