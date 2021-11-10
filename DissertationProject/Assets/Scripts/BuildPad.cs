using UnityEngine;

public class BuildPad : MonoBehaviour
{
    public GameObject buildUIObject;
    public bool isCentre = false;

    private void OnMouseUp()
    {
        GameObject buildUI = Instantiate(buildUIObject, transform.position, transform.rotation);
        buildUI.GetComponent<buildUIObjectParent>().setBuildPad(this.gameObject);
    }
}
