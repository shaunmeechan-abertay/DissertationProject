using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildPad : MonoBehaviour
{
    BuildingManager buildingManager;
    ScoreManager scoreManager;
    EnemySpawner enemySpawner;

    private void Start()
    {
        buildingManager = GameObject.FindObjectOfType<BuildingManager>();
        scoreManager = GameObject.FindObjectOfType<ScoreManager>();
        enemySpawner = GameObject.FindObjectOfType<EnemySpawner>();
    }
    private void OnMouseUp()
    {
        //Debug.Log("Build pad clicked");

        if(buildingManager.selectedTower != null && scoreManager.canAffordPurchase(buildingManager.selectedTower.cost) == true)
        {
            GameObject newTower = Instantiate(buildingManager.selectedTower.gameObject, transform.position, transform.rotation);
            enemySpawner.addTower(newTower.GetComponent<Tower>());
            Destroy(this.gameObject);
        }
    }

}
