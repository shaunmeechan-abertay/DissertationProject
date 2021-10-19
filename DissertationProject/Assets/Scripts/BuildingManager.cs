using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public Tower selectedTower;

    public void setTowerType(Tower prefab)
    {
        selectedTower = prefab;
    }
}
