using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mo : MonoBehaviour
{

    public Transform tar;
    public float smoothing;

    public Vector2 leftPosition;
    public Vector2 rightPosition;

    // Update is called once per frame
    private void FixedUpdate()
    {
        Vector3 tarPosi = tar.position;
        tarPosi.x = Mathf.Clamp(tarPosi.x, leftPosition.x, rightPosition.x);
        tarPosi.y = Mathf.Clamp(tarPosi.y, leftPosition.y, rightPosition.y);
        transform.position = Vector3.Lerp(transform.position, tarPosi, Time.deltaTime * smoothing);
    }
}
