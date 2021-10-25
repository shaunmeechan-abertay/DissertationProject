using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildPad : MonoBehaviour
{
    BuildingManager buildingManager;
    ScoreManager scoreManager;
    AI ai;

    private void Start()
    {
        buildingManager = GameObject.FindObjectOfType<BuildingManager>();
        scoreManager = GameObject.FindObjectOfType<ScoreManager>();
        ai = GameObject.FindObjectOfType<AI>();
    }
    private void OnMouseUp()
    {
        if(buildingManager.selectedTower != null && scoreManager.canAffordPurchase(buildingManager.selectedTower.cost) == true)
        {
            GameObject newTower = Instantiate(buildingManager.selectedTower.gameObject, transform.position, transform.rotation);
            ai.addTower(newTower.GetComponent<Tower>());
            Destroy(this.gameObject);
        }
    }

}
