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
    List<Rune> runeInventory;
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
    Task currentTask = Task.INVALID_TASK;
    public enum Task
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
        runeInventory = new List<Rune>();
        runeInventory.Capacity = runeCarryWeight;
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentTask)
        {
            case Task.MINE_TASK:
            {
                MineTask();
                break;
            }
            case Task.RUNNER_TASK:
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
        if (runeCarryWeight == runeInventory.Count && Vector3.Distance(transform.position, powerSource.transform.position) >= .5)
        {
            SetRender(true);
            transform.position = Vector3.MoveTowards(transform.position, powerSource.transform.position, moveSpeed * Time.deltaTime);
            // fixed y rotation
            transform.LookAt(new Vector3(powerSource.transform.position.x, transform.position.y, powerSource.transform.position.z));
        }
        else if(runeInventory.Count == 0 && Vector3.Distance(transform.position, miningPost.transform.position) >= .5)
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
                for(int i = 0; i < miningPower; ++i)
                {
                    runeInventory.Add(new Rune());
                }
                miningTimer = 0.0f;
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

    public Task GetTask()
    {
        return currentTask;
    }

    // set task by index
    public void SetTask(int taskNum)
    {
        currentTask = (Task)taskNum;
    }

    public void SetTask(Task task)
    {
        currentTask = task;
    }

    [ContextMenu("SetMining")]
    void SetMining()
    {
        currentTask = Task.MINE_TASK;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Hopper")
        {
            for(int i = 0; i < runeCarryWeight; ++i)
            {
                other.gameObject.GetComponent<RuneHopper>().addRune(runeInventory[i]);
            }
            runeInventory.Clear();
        }
    }
}
