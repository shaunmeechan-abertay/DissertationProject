using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildUIObjectParent : MonoBehaviour
{
    Transform padTransform;
    GameObject buildPad;

    public void setBuildPad(GameObject pad)
    {
        buildPad = pad;
        padTransform = pad.transform;
    }

    public GameObject getBuildPad()
    {
        return buildPad;
    }

    public Transform getBuildPadTransform()
    {
        return padTransform;
    }

    private void Update()
    {
        if(Input.GetMouseButtonUp(0))
        {
            Vector2 ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray, Vector2.zero);

            //Clciked the map
            if (hit.collider == null)
            {
                print("We hit something with no collider (e.g the ground)");
                Destroy(gameObject);
            }

            //Clicked an enemy
            if (hit.collider.GetComponent<Enemy>().isActiveAndEnabled == true)
            {
                Destroy(gameObject);
            }

        }
    }
}
