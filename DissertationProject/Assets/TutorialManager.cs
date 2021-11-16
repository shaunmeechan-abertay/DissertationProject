using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    /*Steps:
     * 1: Check player can place basic tower
     * Spawn basic enemies slowly
     * 2: Check player can place fast shooter (maybe turn of other tower?)
     * Spawn fast enemies
     * 3: Check player can place cannon (maybe turn off fast shooter)
     * Spawn a group of enemies
    */


    Tower placedTower = null;

    // Start is called before the first frame update
    void Update()
    {
        if(checkStepOne() == true)
        {
            if(checkStepTwo() == true)
            {
                if(checkStepThree() == true)
                {
                    Debug.Log("Tutorial finished");
                }
            }
        }
    }

    //This waits for the player to place a basic tower
    //We need to make sure that the player can't spawn any other towers than the basic one
    bool checkStepOne()
    {
        if(placedTower.type == Tower.towerType.Standard)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool checkStepTwo()
    {
        return true;
    }

    bool checkStepThree()
    {
        return true;
    }

    public void setTower(ref Tower newTower)
    {
        placedTower = newTower;
    }
}
