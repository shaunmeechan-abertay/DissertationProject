using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildPad : MonoBehaviour
{
    public GameObject buildUIObject;

    private void OnMouseUp()
    {
        GameObject buildUI = Instantiate(buildUIObject, transform.position, transform.rotation);
        buildUI.GetComponent<buildUIObjectParent>().setBuildPad(this.gameObject);
    }
}
