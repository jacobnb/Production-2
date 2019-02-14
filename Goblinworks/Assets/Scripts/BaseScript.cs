using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BaseScript : MonoBehaviour
{
    /*
    * Gold
	* Runes
	* Goblin Training
    * Might have external upgrade building
    */
    int timeDivision = 10; // time = seconds / timeDivision;
    [Header("Goblin Spawn Info")]
    [SerializeField]
    [Tooltip("How many 10ths of a second it takes for a new goblin to spawn")]
    int goblinSpawnRate = 50;
    [SerializeField]
    int goblinCost = 5;
    [SerializeField]
    float goblinUpkeepPerSecond = .1f;
    [SerializeField]
    Vector3 goblinSpawnPosition = new Vector3(1.0f, 1.0f, 1.0f);
    [SerializeField]
    GameObject GoblinFab = null;
    [Header("")]
    [SerializeField]
    TextMeshProUGUI goldText = null;
    Transform goblinFolder = null;

    //Passive income, disable later.
    [Header("how many tenths of a second it takes to gain 1 of x")]
    [SerializeField]
    int goldGenRate = 20;
    [SerializeField]
    int runesGenRate = 10;

    [Header("Displays for debugging")]
    //Resource tracking.
    RuneHopper mRuneHopper;
    public float mGold;
    private List<GameObject> mGoblins;
    private List<GoblinScript> mGoblinScripts;

    float mTimer = 0f;
    //This is used to stop the timer from executing multiple times in a 10th of a second
    int lastTime = -1;
    //Goblin creation queue
    int goblinSpawnQueue;
    //how long until the next goblin spawns.
    float goblinSpawnTimer;
    // Start is called before the first frame update
    void Start()
    {
        goblinFolder = new GameObject("Goblin Folder").transform;
        mRuneHopper = gameObject.GetComponent<RuneHopper>();
        mGoblins = new List<GameObject>();
        mGoblinScripts = new List<GoblinScript>();
        goblinSpawnTimer = goblinSpawnRate;

        goblinSpawnQueue = 2;
    }

    void Update()
    {
        timer();
        goblinGestation();
    }

    void timer()
    {

        mTimer += Time.deltaTime;

        int time = (int)(mTimer * timeDivision);

        //stop timer from executing multiple times per 10th of a second
        if (time == lastTime)
            return;
        lastTime = time;
        int shouldReset = 0; //tracks if can reset time.
        if (time % goldGenRate == 0)
        {
            addGold(1);
            shouldReset++;
        }
        if (time % runesGenRate == 0)
        {
            addRune(new Rune());
            shouldReset++;
        }
        if (time % timeDivision == 0)
        { //every second
            spendGold(goblinUpkeepPerSecond * mGoblins.Count, "Goblin Upkeep");
            shouldReset++;
        }
        if (shouldReset >= 3) //number of if statements
        {
            //This might cause everything to trigger twice
            mTimer = 0f;

            //this should prevent that
            lastTime = 0;
        }
    }
    void goblinGestation()
    {
        if (goblinSpawnQueue > 0)
        {
            //make goblins
            goblinSpawnTimer -= Time.deltaTime * timeDivision;
            if (goblinSpawnTimer <= 0)
            {
                makeGoblin();
                goblinSpawnTimer = goblinSpawnRate;
            }
        }
    }

    public void queueGoblin(int numGoblins)
    {
        while (numGoblins * goblinCost > mGold)
        { //if can't afford goblins, reduce numGoblins
            numGoblins--;
        }
        goblinSpawnQueue += numGoblins;
        string msg = "Bought " + numGoblins + " Goblins";
        spendGold(numGoblins * goblinCost, msg);
    }
    void makeGoblin()
    {
        goblinSpawnQueue--;
        GameObject tempGoblin = goblinConstructor();
        mGoblins.Add(tempGoblin);
        mGoblinScripts.Add(tempGoblin.GetComponent<GoblinScript>());
    }

    GameObject goblinConstructor()
    {
        GameObject babyGoblin = Instantiate(GoblinFab, goblinFolder);
        babyGoblin.transform.position = goblinSpawnPosition;
        return babyGoblin;
    }

    public bool spendGold(float gold, string purchaseName)
    {
        if (gold > mGold)
        {
            return false;
        }
        //TODO purchase logging 
        //Debug.Log("Spent " + gold + " gold on " + purchaseName);
        mGold -= gold;
        updateGoldUI();
        return true;
    }

    public void addGold(float gold)
    {
        mGold += gold;
        updateGoldUI();
    }

    public void addRune(Rune rune)
    {
        mRuneHopper.addRune(rune);
    }

    void updateGoldUI()
    {
        goldText.text = "Gold: " + (int)mGold;
    }

    public List<GameObject> GetGoblins()
    {
        return mGoblins;
    }
}
