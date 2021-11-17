using UnityEngine;

public class BuildPad : MonoBehaviour
{
    public GameObject buildUIObject;
    public ScoreManager scoreManager;
    public bool isCentre = false;
    public bool isDestroyable = false;

    private void OnMouseUp()
    {
        if(scoreManager.getIsBuildMenuOpen() == false)
        {
            Vector3 screenCentre = new Vector3(0.0f, 0.0f , 0.0f);
            GameObject buildUI = Instantiate(buildUIObject, screenCentre, transform.rotation);
            buildUI.GetComponent<buildUIObjectParent>().setBuildPad(this.gameObject);
            scoreManager.setIsBuildMenuOpen(true);
        }
        else
        {
            return;
        }
    }
}
