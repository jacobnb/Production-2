using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridScript : MonoBehaviour
{
    [SerializeField]
    private int gridWidth = 10, gridHeight = 10;
    [SerializeField]
    private GameObject tilePrefab;
    private float prefabWidth = 1, prefabHeight = 1;
    [SerializeField]
    private GameObject wallPrefab;

    private GameObject[,] grid;
    // Start is called before the first frame update
    void Start()
    {
        // create grid
        grid = new GameObject[gridWidth, gridHeight];
        // get tile width and height
        prefabWidth = tilePrefab.GetComponent<Renderer>().bounds.size.x;
        prefabHeight = tilePrefab.GetComponent<Renderer>().bounds.size.y;
        // create grid in game
        for (int j = 0; j < gridWidth; ++j)
        {
            for(int k = 0; k < gridHeight; ++k)
            {
                GameObject obj = Instantiate(tilePrefab, new Vector3(j * prefabWidth, 0, k * prefabHeight), Quaternion.Euler(90, 0, 0));
                obj.transform.parent = GameObject.Find("GridObject").transform;
                grid[j, k] = obj;

                // generate walls
                if (j <= gridWidth / 5 || k <= gridHeight / 3 || k >= gridHeight - (gridHeight / 3))
                {
                    Instantiate(wallPrefab, obj.transform.position, Quaternion.identity, obj.transform);
                }
            }
        }
        // find center and set the camera and powersource
        // change cause this is hacky af
        Vector2 center = new Vector2((gridWidth / 2) * prefabWidth, (gridHeight / 2) * prefabHeight);
        Camera.main.transform.position = new Vector3(center.x - .5f, 10, center.y - .5f);
        GameObject.Find("PowerSource").transform.position = grid[gridWidth / 2 - 4, gridHeight / 2].transform.position + new Vector3(0.0f, 0.5f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
