using UnityEngine;

public class buildUIObjectTutorial : MonoBehaviour
{
    public Tower tower;
    Transform padTransform;
    public ScoreManager scoreManager;
    buildUIObjectParentTutorial parent;
    private void Start()
    {
        scoreManager = GameObject.FindObjectOfType<ScoreManager>();
        parent = GetComponentInParent<buildUIObjectParentTutorial>();
    }

    private void OnMouseUp()
    {
        //Debug.Log(gameObject.name);
        if(scoreManager.canAffordPurchase(tower.cost) == true)
        {
            padTransform = parent.getBuildPadTransform();
            GameObject newTower = Instantiate(tower.gameObject, padTransform.position, padTransform.rotation);
            Tower towerComponent = newTower.GetComponent<Tower>();
            GameObject.FindObjectOfType<TutorialManager>().setTower(ref towerComponent);
            GameObject.FindObjectOfType<AI>().addTower(newTower.GetComponent<Tower>());
            //Debug.Log("Player placed a tower!");
            Destroy(parent.getBuildPad().gameObject);
            //Destroy(parent.gameObject);
            parent.gameObject.SetActive(false);
            scoreManager.setIsBuildMenuOpen(false);
        }
    }
}
