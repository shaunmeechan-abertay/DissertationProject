using UnityEngine;

public class buildUIObjectParent : MonoBehaviour
{
    Transform padTransform;
    GameObject buildPad;
    ScoreManager scoreManager;
    Vector2 mouseInput = new Vector2(0.0f, 0.0f);
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
                Destroy(gameObject);
                scoreManager.setIsBuildMenuOpen(false);
                return;
            }

            //Clicked an enemy
            if (hit.collider.GetComponent<Enemy>().isActiveAndEnabled == true)
            {
                Destroy(gameObject);
                scoreManager.setIsBuildMenuOpen(false);
                return;
            }

        }
    }
}
