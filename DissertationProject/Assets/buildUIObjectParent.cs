using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildUIObjectParent : MonoBehaviour
{
    Transform padTransform;
    GameObject buildPad;

    public void setBuildPad(GameObject pad)
    {
        buildPad = pad;
        padTransform = pad.transform;
    }

    public GameObject getBuildPad()
    {
        return buildPad;
    }

    public Transform getBuildPadTransform()
    {
        return padTransform;
    }
}
