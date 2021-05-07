using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG_Movement_Bug : MonoBehaviour
{
    public Transform close1;
    public Transform close11;
    public Transform close2;
    public Transform close3;
    public Transform close4;

    public float close1Modifier;
    public float close11Modifier;
    public float close2Modifier;
    public float close3Modifier;
    public float close4Modifier;

    public Transform camPosi;

    private Vector3 lastCamPos;

    void Start()
    {
        lastCamPos = camPosi.position;
    }

    void Update()
    {
        float xPosDiff = lastCamPos.x - camPosi.position.x;

        adjustParallaxPositionsForArray(close1, close1Modifier, xPosDiff);
        adjustParallaxPositionsForArray(close11, close11Modifier, xPosDiff);
        adjustParallaxPositionsForArray(close2, close2Modifier, xPosDiff);
        adjustParallaxPositionsForArray(close3, close3Modifier, xPosDiff);
        adjustParallaxPositionsForArray(close4, close4Modifier, xPosDiff);

        lastCamPos = camPosi.position;
    }

    private void adjustParallaxPositionsForArray(Transform layerArray, float layerSpeedModifier, float xPosDiff)
    {
        //layerArray.localPosition += new Vector3(xPosDiff * layerSpeedModifier, layerArray.position.y);

        Vector3 objPos = layerArray.position;
        objPos.x += xPosDiff * layerSpeedModifier;
        layerArray.position = objPos;
    }
}
