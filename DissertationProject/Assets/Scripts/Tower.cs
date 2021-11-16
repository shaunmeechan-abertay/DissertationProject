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
    public int damage = 1;
    public float damageRadius = 0.0f;
    Animator animator;
    AudioSource audioSource;
    EnemySpawner enemySpawner;
    public enum towerType
    {
        Standard,
        Fast,
        Cannon
    }

    public towerType type;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        enemySpawner = GameObject.FindObjectOfType<EnemySpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        if(fireCoolDownLeft > 0)
        {
            fireCoolDownLeft -= Time.deltaTime;
        }

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
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if(angle < 0)
        {
            angle += 360;
        }
        transform.eulerAngles = new Vector3(0, 0, angle);

        if (fireCoolDownLeft <= 0 && dir.magnitude <= range)
        {
            fireCoolDownLeft = coolDown;
            fire(closestEnemy);
        }
        else
        {
            animator.SetBool("canFire", false);
        }
    }

    void fire(Enemy e)
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, this.transform.position, this.transform.rotation);

        Bullet bullet = bulletGO.GetComponent<Bullet>();
        bullet.damage = damage;
        bullet.radius = damageRadius;
        bullet.target = e.transform;
        animator.SetBool("canFire", true);
        audioSource.PlayOneShot(audioSource.clip);
    }
}
