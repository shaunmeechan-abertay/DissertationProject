using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildUIObject : MonoBehaviour
{
    public Tower tower;
    Transform padTransform;
    ScoreManager scoreManager;
    AI ai;
    buildUIObjectParent parent;
    private void Start()
    {
        ai = GameObject.FindObjectOfType<AI>();
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
            ai.addTower(newTower.GetComponent<Tower>());
            Destroy(parent.getBuildPad().gameObject);
            Destroy(parent.gameObject);
        }
    }
}
