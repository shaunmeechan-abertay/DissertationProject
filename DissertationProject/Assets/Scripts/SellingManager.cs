using UnityEngine;

public class SellingManager : MonoBehaviour
{
    Tower selectedTower = null;
    ScoreManager scoreManager;
    public GameObject prefabToSpawn;

    private void Start()
    {
        scoreManager = GameObject.FindObjectOfType<ScoreManager>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonUp(0))
        {
            Vector2 ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray, Vector2.zero);

            if(hit.collider == null)
            {
                Debug.Log("Hit nothing!");
                if(selectedTower != null)
                {
                    selectedTower.GetComponent<SpriteRenderer>().color = new Color(1,1,1);
                }
                selectedTower = null;
                return;
            }
            
            if(hit.collider.tag == "Tower")
            {
                Debug.Log("Hit tower");
                if (selectedTower != null)
                {
                    selectedTower.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
                }
                selectedTower = hit.collider.GetComponent<Tower>();

                selectedTower.GetComponent<SpriteRenderer>().color = Color.yellow;
            }
        }

        if(Input.GetKeyDown(KeyCode.Delete))
        {
            if(selectedTower != null)
            {
                scoreManager.incrementMoney(selectedTower.cost);
                //Now need to place a new buildPad
                Instantiate(prefabToSpawn, selectedTower.transform.position, Quaternion.identity);
                Destroy(selectedTower.gameObject);
                selectedTower = null;
            }
        }
    }
}
