using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG_Movement : MonoBehaviour
{
    public Transform clouds;
    public Transform nearHills;
    public Transform farHills;
    
    public float cloudLayerSpeedModifier;
    public float nearHillLayerSpeedModifier;
    public float farHillLayerSpeedModifier;

    public Transform camPosi;

    private Vector3 lastCamPos;

    void Start()
    {
        lastCamPos = camPosi.position;
    }

    void Update()
    {
        float xPosDiff = lastCamPos.x - camPosi.position.x;

        adjustParallaxPositionsForArray(clouds, cloudLayerSpeedModifier, xPosDiff);
        adjustParallaxPositionsForArray(nearHills, nearHillLayerSpeedModifier, xPosDiff);
        adjustParallaxPositionsForArray(farHills, farHillLayerSpeedModifier, xPosDiff);

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