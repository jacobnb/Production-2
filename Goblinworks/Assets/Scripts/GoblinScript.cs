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
    int runeCarryWeight = 1;
    int numRunes = 0;
    // Time it takes for the goblin to mine a rock
    [SerializeField]
    float mineTime = .5f;
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

    GameObject baseObject;
    GameObject miningPost;

    // Start is called before the first frame update
    void Start()
    {
        baseObject = GameObject.Find("Base");
        miningPost = GameObject.Find("Mining Post");
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
        if (runeCarryWeight == numRunes)
        {
            Debug.Log("Going To Base");
            Vector3.MoveTowards(transform.position, baseObject.transform.position, moveSpeed);
        }
        else if(numRunes == 0 && transform.position != miningPost.transform.position)
        {
            Debug.Log("Going To Mine");
            Vector3.MoveTowards(transform.position, miningPost.transform.position, moveSpeed);
        }
        else
        {
            Debug.Log("Mining");
            miningTimer += Time.deltaTime;
            if (miningTimer >= mineTime)
            {
                numRunes += miningPower;
                if (numRunes >= runeCarryWeight)
                {
                    numRunes = runeCarryWeight;
                }
                miningTimer = 0.0f;
            }
        }
    }

    void RunnerTask()
    {

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
}
