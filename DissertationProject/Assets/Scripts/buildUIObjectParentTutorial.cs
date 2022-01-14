using UnityEngine;

public class buildUIObjectParentTutorial : MonoBehaviour
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

            //Clciked the map
            if (hit.collider == null)
            {
                close();
                return;
            }

            if (hit.collider.GetComponent<BuildPadTutorial>() == true)
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
