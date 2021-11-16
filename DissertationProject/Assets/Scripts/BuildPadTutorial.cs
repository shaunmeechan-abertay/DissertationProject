using UnityEngine;

public class BuildPadTutorial : MonoBehaviour
{
    public GameObject buildUIObject;
    public ScoreManager scoreManager;
    public bool isCentre = false;

    private void OnMouseUp()
    {
        if(scoreManager.getIsBuildMenuOpen() == false)
        {
            Vector3 screenCentre = new Vector3(0.0f, 0.0f , 0.0f);
            buildUIObject.SetActive(true);
            buildUIObject.GetComponent<buildUIObjectParentTutorial>().setBuildPad(this.gameObject);
            scoreManager.setIsBuildMenuOpen(true);
        }
        else
        {
            return;
        }
    }
}
