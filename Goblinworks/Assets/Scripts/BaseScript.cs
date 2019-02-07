using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseScript : MonoBehaviour
{
    /*
    * Gold
	* Runes
	* Goblin Training
    * Might have external upgrade building
    */
    [Header("Goblin Spawn Info")]
    [SerializeField]
    [Tooltip("How many 10ths of a second it takes for a new goblin to spawn")]
    int goblinProcreationRate = 50;
    [SerializeField]
    int goblinCost = 5;
    [SerializeField]
    float goblinUpkeepPerSecond = .1f;
    

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
    // Start is called before the first frame update
    void Start()
    {
        numPregnantGoblins = 2;
        mRunes = new List<Rune>();
        mGoblins = new List<GoblinScript>();
    }

    void Update()
    {
        timer();
    }

    void timer()
    {
        
        mTimer += Time.deltaTime;
        int timeDivision = 10; // time = seconds / timeDivision;
        int time = (int)(mTimer * timeDivision);

        //stop timer from executing multiple times per 10th of a second
        if (time == lastTime)
            return;
        lastTime = time;
        int shouldReset = 0; //tracks if can reset time.
        if(time % goblinProcreationRate == 0)
        {
            makeGoblin();
            shouldReset++;
        }
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
            spendGold(goblinUpkeepPerSecond, "Goblin Upkeep");
            shouldReset++;
        }
        if(shouldReset >= 4) //number of if statements
        {
            mTimer = 0f;
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
    { //if there's a pregnant goblin, make a goblin
        if(numPregnantGoblins > 0)
        {
            numPregnantGoblins--;
            mGoblins.Add(goblinConstructor());
        }
    }

    GoblinScript goblinConstructor()
    {
        //TODO
        return null;
    }

    public bool spendGold(float gold, string purchaseName)
    {
        if (gold > mGold)
        {
            return false;
        }
        //TODO purchase logging 
        //update gold text
        //Debug.Log("Spent " + gold + " gold on " + purchaseName);
        mGold -= gold;
        return true;
    }
    public void addGold(float gold)
    {
        //TODO update gold text
        mGold += gold;
    }

    public void addRune(Rune rune)
    {
        mRunes.Add(rune);
    }
}
