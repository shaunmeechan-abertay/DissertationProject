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
        //Calculate where the mouse is at
        mouseInput.x = Input.mousePosition.x - (Screen.width / 2f);
        mouseInput.x = Input.mousePosition.y - (Screen.height / 2f);
        mouseInput.Normalize();

        if(mouseInput != Vector2.zero)
        {
            float angle = Mathf.Atan2(mouseInput.y, -mouseInput.x) / Mathf.PI;
            angle *= 180;
            //angle += 90f;
            if(angle < 0)
            {
                angle += 360;
            }

            Debug.Log(angle);
        }


        if(Input.GetMouseButtonUp(0))
        {
            Vector2 ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray, Vector2.zero);

            //Clciked the map
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
