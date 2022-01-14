using UnityEngine;

public class buildUIObjectParent : MonoBehaviour
{
    Transform padTransform;
    GameObject buildPad;
    ScoreManager scoreManager;
    private void Start()
    {
        scoreManager = GameObject.FindObjectOfType<ScoreManager>();
    }
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

            //Clicked the map
            if (hit.collider == null)
            {
                print("We hit something with no collider (e.g the ground)");
                close();
                return;
            }

            if(hit.collider.GetComponent<BuildPad>() == true)
            {
                //It's just the build pad so ignore
                if (hit.collider.gameObject == buildPad)
                {
                    return;
                }
                else
                {
                    close();
                    return;
                }
            }

            Debug.Log(hit.collider.name);

            //Clicked an enemy
            if (hit.collider.GetComponent<Enemy>().isActiveAndEnabled == true)
            {
                close();
                return;
            }

        }
    }

    void close()
    {
        Destroy(gameObject);
        scoreManager.setIsBuildMenuOpen(false);
    }
}
