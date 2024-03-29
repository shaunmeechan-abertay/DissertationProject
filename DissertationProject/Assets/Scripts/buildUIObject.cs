using UnityEngine;

public class buildUIObject : MonoBehaviour
{
    public Tower tower;
    Transform padTransform;
    public ScoreManager scoreManager;
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
            BuildPad buildPad = parent.getBuildPad().GetComponent<BuildPad>();
            if (buildPad.isCentre == true && buildPad.isDestroyable == false)
            {
                GameObject.FindObjectOfType<AI>().addCentreTower(newTower.GetComponent<Tower>());
            }
            else if(parent.getBuildPad().GetComponent<BuildPad>().isDestroyable == true)
            {
                GameObject.FindObjectOfType<AI>().addTowersInPath(newTower.GetComponent<Tower>());
            }
            else
            {
                GameObject.FindObjectOfType<AI>().addTower(newTower.GetComponent<Tower>());
            }

            //Send data to the analytics manager
            AnalyticsManager analyticsManager = GameObject.FindGameObjectWithTag("AnalyticsManager").GetComponent<AnalyticsManager>();
            analyticsManager.incrementTowerCount();
            switch(tower.type)
            {
                case Tower.towerType.Standard:
                    analyticsManager.incrementBasicTowerCount();
                    break;
                case Tower.towerType.Fast:
                    analyticsManager.incrementFastTowerCount();
                    break;
                case Tower.towerType.Cannon:
                    analyticsManager.incrementCannonTowerCount();
                    break;
            }
            Destroy(parent.getBuildPad().gameObject);
            Destroy(parent.gameObject);
            scoreManager.setIsBuildMenuOpen(false);
        }
    }
}
