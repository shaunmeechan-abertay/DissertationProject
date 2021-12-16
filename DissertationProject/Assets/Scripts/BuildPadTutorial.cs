using UnityEngine;

public class BuildPadTutorial : MonoBehaviour
{
    public GameObject buildUIObject;
    public ScoreManager scoreManager;
    bool bCanOpenBuildMenu = true;

    private void OnMouseUp()
    {
        if(scoreManager.getIsBuildMenuOpen() == false && bCanOpenBuildMenu == true)
        {
            buildUIObject.SetActive(true);
            buildUIObject.GetComponent<buildUIObjectParentTutorial>().setBuildPad(this.gameObject);
            scoreManager.setIsBuildMenuOpen(true);
        }
        else
        {
            return;
        }
    }

    public void setCanOpenBuildMenu(bool value)
    {
        bCanOpenBuildMenu = value;
    }
}
