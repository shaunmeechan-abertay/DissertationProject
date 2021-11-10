using UnityEngine;

public class buildUIObject : MonoBehaviour
{
    public Tower tower;
    Transform padTransform;
    ScoreManager scoreManager;
    buildUIObjectParent parent;
    private void Start()
    {
        scoreManager = GameObject.FindObjectOfType<ScoreManager>();
        parent = GetComponentInParent<buildUIObjectParent>();
    }

    private void OnMouseUp()
    {
        Debug.Log(gameObject.name);
        if(scoreManager.canAffordPurchase(tower.cost) == true)
        {
            padTransform = parent.getBuildPadTransform();
            GameObject newTower = Instantiate(tower.gameObject, padTransform.position, padTransform.rotation);
            if(parent.getBuildPad().GetComponent<BuildPad>().isCentre == true)
            {
                GameObject.FindObjectOfType<AI>().addCentreTower(newTower.GetComponent<Tower>());
            }
            else
            {
                GameObject.FindObjectOfType<AI>().addTower(newTower.GetComponent<Tower>());
            }
            Destroy(parent.getBuildPad().gameObject);
            Destroy(parent.gameObject);
        }
    }
}
