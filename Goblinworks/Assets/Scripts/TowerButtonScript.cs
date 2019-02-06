using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerButtonScript : MonoBehaviour
{
    [SerializeField]
    float Y_buildlocation;
    [SerializeField]
    GameObject towerFab;
    Camera cam;
    bool active = false;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (!active)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            checkLocation();
        }
    }

    void checkLocation()
    {
        //if mouse not too close to anything, then build
        Vector3 clickLoc = Input.mousePosition;
        clickLoc.z = cam.transform.position.y;
        Vector3 buildLoc = cam.ScreenToWorldPoint(clickLoc);
        buildLoc.y = Y_buildlocation;
        // TODO actually check if it's a valid build location.
        if (true)
        {
            buildTower(buildLoc);
        }
        active = false;
    }

    void buildTower(Vector3 buildLoc)
    {
        GameObject tower = Instantiate(towerFab);
        tower.transform.position = buildLoc;
        tower.AddComponent<TowerScript>();
    }

    public void onClick()
    {
        if (active)
        {
            active = false;
        }
        else
        {
            active = true;
        }
    }
}
