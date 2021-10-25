using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 15;
    public int damage = 1;
    public float radius = 0.0f;
    public Transform target;
    Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        direction = new Vector2(0, 0);

    }

    // Update is called once per frame
    void Update()
    {

        if(target == null)
        {
            //Enemy went away
            Destroy(this.gameObject);
            return;
        }
        direction = target.position - transform.localPosition;
        //This represents the distance we covered this frame
        float disThisFrame = speed * Time.deltaTime;

        if (direction.magnitude <= disThisFrame)
        {
            //We reached the node
            hitTarget();
        }
        else
        {
            //Move towards the node
            transform.Translate(direction.normalized * disThisFrame, Space.World);
            float z = Quaternion.LookRotation(direction).z;
            transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, z, transform.rotation.w);
        }
    }

    void hitTarget()
    {
        if(radius == 0)
        {
            target.GetComponent<Enemy>().takeDamge(damage);
        }
        else
        {
            Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, radius);
            foreach(Collider2D c in cols)
            {
                Enemy e = c.GetComponent<Enemy>();
                if(e != null)
                {
                    e.takeDamge(damage);
                }
            }
        }

        Destroy(this.gameObject);
    }
}