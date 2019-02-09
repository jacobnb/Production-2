using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScript : MonoBehaviour
{
    [SerializeField]
    float towerRange;
    [SerializeField]
    float health;
    RuneHopper mRuneHopper;
    
    // Start is called before the first frame update
    void Start()
    {
        mRuneHopper = new RuneHopper();
        Debug.Log("I'M ALIVE!!!!!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
