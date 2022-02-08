using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameObject pathObject;
    Transform targetPathNode;
    int pathNodeIndex = 0;
    public float speed = 5;
    Vector2 direction;
    public float health = 5.0f;
    ScoreManager scoreManager;
    float timeToLeave = 0.0f;
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
            //Debug.Log("Length of pathObject: " + pathObject.transform.childCount);
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
            return;
        }

        direction = targetPathNode.position - transform.localPosition;
        //This represents the distance we covered this frame
        float disThisFrame = speed * Time.deltaTime;
        timeToLeave += Time.deltaTime;
        if(direction.magnitude <= disThisFrame)
        {
            //We reached the node
            targetPathNode = null;
        }
        else
        {
            //Move towards the node
            transform.Translate(direction.normalized * disThisFrame, Space.World);
            //float z = Quaternion.LookRotation(direction).z;
            //transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, z, transform.rotation.w);

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            if (angle < 0)
            {
                angle += 360;
            }
            transform.eulerAngles = new Vector3(0, 0, angle);

        }

    }

    public void takeDamge(float damage)
    {
        health -= damage;

        if(health <= 0)
        {
            die();
        }
    }

    void reachedGoal()
    {
        //Debug.Log("Time to leave: " + timeToLeave);
        scoreManager.decrementLife(1);
        Destroy(gameObject);
    }

    void die()
    {
        scoreManager.incrementMoney(1);
        Destroy(gameObject);
    }
}
