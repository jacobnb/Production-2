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

    public enum Task
    {
        INVALID_TASK = -1,
        MINE_TASK,
        TOWER_RUNNER_TASK,
        INFUSER_RUNNER_TASK,
    }
    // current task the goblin is assigned
    Task currentTask = Task.INVALID_TASK;
    static bool nextTask = true;
    // current transform the goblin is running to
    GameObject runnerGameObject = null;

    GameObject powerSource;
    GameObject miningPost;
    /// TO DO \\\
    /// THIS IS MASSIVE TECH DEBT PLEASE FIX ME AS SOON AS POSSIBLE \\\
    GameObject towerTracker;
    GameObject infuserTracker;

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

        towerTracker = GameObject.Find("TowerObject");
        infuserTracker = GameObject.Find("InfuserObject");
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentTask)
        {
            case Task.INVALID_TASK:
            {
                    if(nextTask)
                    {
                        currentTask = Task.TOWER_RUNNER_TASK;
                        nextTask = !nextTask;
                    }
                    else
                    {
                        currentTask = Task.INFUSER_RUNNER_TASK;
                        nextTask = !nextTask;
                    }
                    break;
            }
            case Task.MINE_TASK:
            {
                MineTask();
                break;
            }
            case Task.TOWER_RUNNER_TASK:
            {
                RunnerTask();
                break;
            }
            case Task.INFUSER_RUNNER_TASK:
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
            Vector3 target = new Vector3(powerSource.transform.position.x, transform.position.y, powerSource.transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
            // fixed y rotation
            transform.LookAt(new Vector3(powerSource.transform.position.x, transform.position.y, powerSource.transform.position.z));
        }
        else if(runeInventory.Count == 0 && Vector3.Distance(transform.position, miningPost.transform.position) >= .5)
        {
            Vector3 target = new Vector3(miningPost.transform.position.x, transform.position.y, miningPost.transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
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
        if(runnerGameObject != null && Vector3.Distance(transform.position, runnerGameObject.transform.position) >= 1.1)
        {
            Vector3 target = new Vector3(runnerGameObject.transform.position.x, transform.position.y, runnerGameObject.transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
            transform.LookAt(new Vector3(runnerGameObject.transform.position.x, transform.position.y, runnerGameObject.transform.position.z));
        }
        // reached destination
        else if (runnerGameObject != null && runnerGameObject.name == "PowerSource")
        {
            if(currentTask == Task.TOWER_RUNNER_TASK)
            {
                // go to new tower
                runnerGameObject = towerTracker.GetComponent<BuildingListScript>().GetLowBuilding();
            }
            else if(currentTask == Task.INFUSER_RUNNER_TASK)
            {
                // go to new infuser
                runnerGameObject = infuserTracker.GetComponent<BuildingListScript>().GetLowBuilding();
            }
        }
        else if(runnerGameObject != null && (runnerGameObject.name == "Tower(Clone)" || runnerGameObject.name == "Infuse(Clone)"))
        {
            runnerGameObject = powerSource;
        }
        else if(currentTask == Task.TOWER_RUNNER_TASK)
        {
            // go to new tower
            runnerGameObject = towerTracker.GetComponent<BuildingListScript>().GetLowBuilding();
        }
        else if (currentTask == Task.INFUSER_RUNNER_TASK)
        {
            // go to new infuser
            runnerGameObject = infuserTracker.GetComponent<BuildingListScript>().GetLowBuilding();
        }
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

    private void OnTriggerEnter(Collider other)
    {
        if(runeInventory.Count > 0 && other.tag == "Hopper" && (runnerGameObject == null || other.name == runnerGameObject.name))
        {
            foreach(Rune rune in runeInventory)
            {
                other.gameObject.GetComponent<RuneHopper>().addRune(rune);
            }
            runeInventory.Clear();            
        }
        else if(runeInventory.Count == 0 && other.tag == "Hopper" && other.name == "PowerSource" && other.name == runnerGameObject.name)
        {
            Rune rune;
            for(int i = 0; i < runeInventory.Capacity; ++i)
            {
                rune = other.gameObject.GetComponent<RuneHopper>().getRune();
                if(rune == null)
                {
                    return;
                }
                runeInventory.Add(rune);
            }
        }
    }
}
