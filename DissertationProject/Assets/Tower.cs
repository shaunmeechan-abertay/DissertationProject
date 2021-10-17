using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    float range = 10f;
    public GameObject bulletPrefab;
    public float coolDown = 1.0f;
    float fireCoolDownLeft = 0.0f; 
    // Start is called before the first frame update
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: Optimise this
        Enemy[] enemies = GameObject.FindObjectsOfType<Enemy>();

        Enemy closestEnemy = null;
        float dist = 0.0f;

        foreach(Enemy e in enemies)
        {
            float d = Vector2.Distance(this.transform.position, e.transform.position);
            if(closestEnemy == null || d < dist)
            {
                closestEnemy = e;
                dist = d;
            }
        }

        if(closestEnemy == null)
        {
            Debug.Log("Could not find any enemies");
            return;
        }

        Vector3 dir = closestEnemy.transform.position - this.transform.position;
        Quaternion lookRot = Quaternion.LookRotation(dir);
        //Debug.Log(lookRot.eulerAngles);
        transform.right = closestEnemy.transform.position - transform.position;

        fireCoolDownLeft -= Time.deltaTime;
        if(fireCoolDownLeft <= 0 && dir.magnitude <= range)
        {
            fireCoolDownLeft = coolDown;
            fire(closestEnemy);
        }
    }

    void fire(Enemy e)
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, this.transform.position, this.transform.rotation);

        Bullet bullet = bulletGO.GetComponent<Bullet>();
        bullet.target = e.transform;
    }
}
