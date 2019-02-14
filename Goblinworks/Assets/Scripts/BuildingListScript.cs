using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingListScript : MonoBehaviour
{
    /// TO DO \\\
    /// THIS IS MASSIVE TECH DEBT PLEASE FIX ME AS SOON AS POSSIBLE \\\
    /// List Wrapper to keep track of buildings for now \\\
    List<GameObject> buildings;

    private void Start()
    {
        buildings = new List<GameObject>();
    }

    public void AddBuilding(GameObject building)
    {
        buildings.Add(building);
    }

    public void RemoveBuilding(int index)
    {
        buildings.RemoveAt(index);
    }

    public void RemoveBuilding(GameObject building)
    {
        buildings.Remove(building);
    }

    public List<GameObject> GetBuildings()
    {
        return buildings;
    }

    public GameObject GetLowBuilding(int threshhold)
    {
        GameObject returnObject = null;
        int returnObjectRunes = 0;
        if (buildings.Count > 0)
        {
            returnObject = buildings[0];
            returnObjectRunes = buildings[0].GetComponent<RuneHopper>().getNumRunes();
        }
        foreach(GameObject building in buildings)
        {
            RuneHopper buildingHopper = building.GetComponent<RuneHopper>();
            if(buildingHopper.getNumRunes() < returnObjectRunes && buildingHopper.getNumRunes() < threshhold)
            {
                returnObject = building;
                returnObjectRunes = buildingHopper.getNumRunes();
            }
        }
        return returnObject;
    }

    public GameObject GetLowBuilding()
    {
        GameObject returnObject = null;
        int returnObjectRunes = 0;
        if (buildings.Count > 0)
        {
            returnObject = buildings[0];
            returnObjectRunes = buildings[0].GetComponent<RuneHopper>().getNumRunes();
        }
        foreach (GameObject building in buildings)
        {
            RuneHopper buildingHopper = building.GetComponent<RuneHopper>();
            if (buildingHopper.getNumRunes() < returnObjectRunes)
            {
                returnObject = building;
                returnObjectRunes = buildingHopper.getNumRunes();
            }
        }
        return returnObject;
    }
}
