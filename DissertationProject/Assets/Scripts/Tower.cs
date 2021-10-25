using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    float range = 10f;
    public GameObject bulletPrefab;
    public float coolDown = 1.0f;
    float fireCoolDownLeft = 0.0f;
    public int cost = 5;
    int damage = 1;
    public float damageRadius = 0.0f;

    // Start is called before the first frame update
    //void Start()
    //{
    //}

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
            //TODO: If we can't find any enemies then the round is probably over so no point
            //keeping this object active. It should turn off then be turned back on when a new round starts
            //Debug.Log("Could not find any enemies");
            return;
        }

        Vector3 dir = (closestEnemy.transform.position - transform.position).normalized;
        //This should be looked at again when we have real art in
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if(angle < 0)
        {
            angle += 360;
        }
        transform.eulerAngles = new Vector3(0, 0, angle);
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
        bullet.damage = damage;
        bullet.radius = damageRadius;
        bullet.target = e.transform;
    }
}
