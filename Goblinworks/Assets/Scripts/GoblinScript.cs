using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinScript : MonoBehaviour
{
    /*
     *  Implement as timed queue for now.
	 * Small upfront and upkeep cost.
	 * Timed build like sc2.
	 * Maybe population system.
	 * Somehow keep track of skills / time to mine.
	    * Mining
	    * Transportation - speed for now
	    * Magic - throwing runes for now.
	        * Will probably be spell based in the end. 
	    * Carry weight.
     */

    // Number of runes/rock the goblin can carry at any point and time
    [SerializeField]
    int runeCarryWeight = 10;
    int numRunes = 0;
    // Time it takes for the goblin to mine a rock
    [SerializeField]
    float mineTime = 1f;
    // How many runes/rocks the gobling mines at once
    [SerializeField]
    int miningPower = 1;
    float miningTimer = 0.0f;
    // Movespeed of the goblin
    [SerializeField]
    float moveSpeed = 1.0f;

    // current task the goblin is assigned
    int currentTask = -1;
    enum Tasks
    {
        INVALID_TASK = -1,
        MINE_TASK = 0,
        RUNNER_TASK = 1,
    }

    GameObject powerSource;
    GameObject miningPost;
    List<MeshRenderer> bodyParts;

    // Start is called before the first frame update
    void Start()
    {
        powerSource = GameObject.Find("PowerSource");
        miningPost = GameObject.Find("Mining Post");
        bodyParts = new List<MeshRenderer>();
        for(int i = 0; i < transform.GetChild(0).childCount; ++i)
        {
            bodyParts.Add(transform.GetChild(0).GetChild(i).GetComponent<MeshRenderer>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentTask)
        {
            case (int)Tasks.MINE_TASK:
            {
                MineTask();
                break;
            }
            case (int)Tasks.RUNNER_TASK:
            {
                RunnerTask();
                break;
            }
            default:
                break;
        }
    }

    void MineTask()
    {
        if (runeCarryWeight == numRunes && Vector3.Distance(transform.position, powerSource.transform.position) >= .5)
        {
            SetRender(true);
            transform.position = Vector3.MoveTowards(transform.position, powerSource.transform.position, moveSpeed * Time.deltaTime);
            // fixed y rotation
            transform.LookAt(new Vector3(powerSource.transform.position.x, transform.position.y, powerSource.transform.position.z));
        }
        else if(numRunes == 0 && Vector3.Distance(transform.position, miningPost.transform.position) >= .5)
        {
            transform.position = Vector3.MoveTowards(transform.position, miningPost.transform.position, moveSpeed * Time.deltaTime);
            // fixed y rotation
            transform.LookAt(new Vector3(miningPost.transform.position.x, transform.position.y, miningPost.transform.position.z));
        }
        else
        {
            SetRender(false);
            miningTimer += Time.deltaTime;
            if (miningTimer >= mineTime)
            {
                numRunes += miningPower;
                if (numRunes >= runeCarryWeight)
                {
                    numRunes = runeCarryWeight;
                }
                miningTimer = 0.0f;
                Debug.Log("Mining->Current Runes: " + numRunes.ToString());
            }
        }
    }

    void RunnerTask()
    {

    }

    // Set the whole object to which render
    void SetRender(bool val)
    {
        foreach(MeshRenderer part in bodyParts)
        {
            part.enabled = val;
        }
    }

    // set task by name
    public void SetTask(string taskName)
    {
    }

    // set task by index
    public void SetTask(int taskNum)
    {
        currentTask = taskNum;
    }

    [ContextMenu("SetMining")]
    void SetMining()
    {
        currentTask = (int)Tasks.MINE_TASK;
    }
}
