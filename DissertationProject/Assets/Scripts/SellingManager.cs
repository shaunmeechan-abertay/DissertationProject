using UnityEngine;
using System.Collections;
public class SellingManager : MonoBehaviour
{
    Tower selectedTower = null;
    ScoreManager scoreManager;
    public GameObject prefabToSpawn;
    bool isHolding = false;
    SpriteRenderer towerSR;
    float aValue = 1.0f;

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
            
            if(isHolding == true)
            {
                isHolding = false;
                aValue = 1.0f;
                towerSR.color = new Color(towerSR.color.r, towerSR.color.g, towerSR.color.b, aValue);
            }

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

        //Hold for Android
        if(Input.GetMouseButton(0))
        {
            Vector2 ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray, Vector2.zero);

            if (hit.collider == null)
            {
                //Debug.Log("Hit nothing!");
                return;
            }

            if (hit.collider.tag == "Tower")
            {
                //Debug.Log("Hit tower");
                if(isHolding == false)
                {
                    isHolding = true;
                    //StartCoroutine(sellTimer());
                }

                //if (selectedTower != null)
                //{
                //    selectedTower.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
                //}
                selectedTower = hit.collider.GetComponent<Tower>();
                towerSR = selectedTower.GetComponent<SpriteRenderer>();
                towerSR.color = Color.yellow;

                //Start a timer, if player stops holding button stop timer
            }
        }

        if(isHolding == true)
        {
            //Issue, this isn't frame rate independent!
            aValue -= (0.9f * Time.deltaTime);
            towerSR.color = new Color(towerSR.color.r, towerSR.color.g, towerSR.color.b, aValue);
            if(aValue <= 0.0f)
            {
                destroyTower();
            }
        }

        if (Input.GetKeyDown(KeyCode.Delete))
        {
            destroyTower();
        }
    }

    void destroyTower()
    {
        if (selectedTower != null)
        {
            scoreManager.incrementMoney(selectedTower.cost);
            //Now need to place a new buildPad
            if (selectedTower.transform.position.y >= 3.5f)
            {
                GameObject temp = Instantiate(prefabToSpawn, selectedTower.transform.position, Quaternion.identity);
                temp.GetComponent<BuildPad>().isDestroyable = true;
            }
            else if(selectedTower.transform.position.y == 1.5f)
            {
                GameObject temp = Instantiate(prefabToSpawn, selectedTower.transform.position, Quaternion.identity);
                temp.GetComponent<BuildPad>().isCentre = true;
            }
            else if(selectedTower.transform.position.y == -0.5f)
            {
                GameObject temp = Instantiate(prefabToSpawn, selectedTower.transform.position, Quaternion.identity);
                temp.GetComponent<BuildPad>().isCentre = true;
            }
            else
            {
                Instantiate(prefabToSpawn, selectedTower.transform.position, Quaternion.identity);
            }
            Destroy(selectedTower.gameObject);
            selectedTower = null;
            aValue = 1.0f;
        }
    }

    IEnumerator sellTimer()
    {
        yield return new WaitForSeconds(2.0f);
        if(isHolding == true)
        {
            destroyTower();
        }
    }
}
