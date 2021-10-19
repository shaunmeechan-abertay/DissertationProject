using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildPad : MonoBehaviour
{
    BuildingManager buildingManager;
    ScoreManager scoreManager;
    private void Start()
    {
        buildingManager = GameObject.FindObjectOfType<BuildingManager>();
        scoreManager = GameObject.FindObjectOfType<ScoreManager>();
    }
    private void OnMouseUp()
    {
        Debug.Log("Build pad clicked");

        if(buildingManager.selectedTower != null && scoreManager.canAffordPurchase(buildingManager.selectedTower.cost) == true)
        {
            Instantiate(buildingManager.selectedTower.gameObject, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }

}
