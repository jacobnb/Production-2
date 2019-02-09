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
    int goblinProcreationRate = 50;
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

    //Passive income, disable later.
    [Header("how many tenths of a second it takes to gain 1 of x")]
    [SerializeField]
    int goldGenRate = 20;
    [SerializeField]
    int runesGenRate = 10;

    [Header("Displays for debugging")]
    //Resource tracking.
    public List<Rune> mRunes; //move this to a rune tracker class
    public float mGold;
    public List<GoblinScript> mGoblins;

    float mTimer = 0f;
    //This is used to stop the timer from executing multiple times in a 10th of a second
    int lastTime = -1;
    //Goblin creation queue
    int numPregnantGoblins;
    //how long until the next goblin spawns.
    float goblinBirthTimer;
    // Start is called before the first frame update
    void Start()
    {
        numPregnantGoblins = 2;
        mRunes = new List<Rune>();
        mGoblins = new List<GoblinScript>();
        goblinBirthTimer = goblinProcreationRate;
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
        if(time % goldGenRate == 0)
        {
            addGold(1);
            shouldReset++;
        }
        if(time % runesGenRate == 0)
        {
            addRune(new Rune());
            shouldReset++;
        }
        if (time % timeDivision == 0)
        { //every second
            spendGold(goblinUpkeepPerSecond * mGoblins.Count, "Goblin Upkeep");
            shouldReset++;
        }
        if(shouldReset >= 3) //number of if statements
        {
            //This might cause everything to trigger twice
            mTimer = 0f;

            //this should prevent that
            lastTime = 0;
        }
    }
    void goblinGestation()
    {
        if(numPregnantGoblins > 0)
        {
            //make goblins
            goblinBirthTimer -= Time.deltaTime * timeDivision;
            if(goblinBirthTimer <= 0)
            {
                makeGoblin();
                goblinBirthTimer = goblinProcreationRate;
            }
        }
    }

    public void queueGoblin(int numGoblins)
    {
        while (numGoblins * goblinCost > mGold)
        { //if can't afford goblins, reduce numGoblins
            numGoblins--;
        }
        numPregnantGoblins += numGoblins;
        string msg = "Bought " + numGoblins + " Goblins";
        spendGold(numGoblins * goblinCost, msg);
    }
    void makeGoblin()
    { 
        numPregnantGoblins--;
        mGoblins.Add(goblinConstructor());
    }

    GoblinScript goblinConstructor()
    {
        GameObject babyGoblin = Instantiate(GoblinFab);
        babyGoblin.transform.position = goblinSpawnPosition;
        return babyGoblin.GetComponent<GoblinScript>();
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
        mRunes.Add(rune);
    }

    void updateGoldUI()
    {
        goldText.text = "Gold: " + (int)mGold;
    }
}
