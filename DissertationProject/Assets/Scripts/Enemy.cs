using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameObject pathObject;
    Transform targetPathNode;
    int pathNodeIndex = 0;
    float speed = 5;
    Vector2 direction;
    public int health = 5;
    ScoreManager scoreManager;
    // Start is called before the first frame update
    void Start()
    {
        pathObject = GameObject.Find("Path");
        direction = new Vector2(0, 0);
        scoreManager = GameObject.FindObjectOfType<ScoreManager>();
    }

    void getNextPathNode()
    {
        if(pathNodeIndex >= pathObject.transform.childCount)
        {
            reachedGoal();
        }
        else
        {
            //Debug.Log("pathNodeIndex: " + pathNodeIndex);
            targetPathNode = pathObject.transform.GetChild(pathNodeIndex);
            pathNodeIndex++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(targetPathNode == null)
        {
            getNextPathNode();
        }

        direction = targetPathNode.position - transform.localPosition;
        //This represents the distance we covered this frame
        float disThisFrame = speed * Time.deltaTime;

        if(direction.magnitude <= disThisFrame)
        {
            //We reached the node
            targetPathNode = null;
        }
        else
        {
            //Move towards the node
            transform.Translate(direction.normalized * disThisFrame, Space.World);
            float z = Quaternion.LookRotation(direction).z;
            transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, z, transform.rotation.w);
        }

    }

    public void takeDamge(int damage)
    {
        health -= damage;

        //Debug.Log("Took damage. Health = " + health);

        if(health <= 0)
        {
            //Should change this to dedicated func
            die();
        }
    }

    void reachedGoal()
    {
        scoreManager.decrementLife(1);
        Destroy(gameObject);
    }

    void die()
    {
        scoreManager.incrementMoney(1);
        Destroy(gameObject);
    }
}
