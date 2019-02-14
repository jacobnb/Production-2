using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneHopper : MonoBehaviour
{
    //Base class to hold runes for towers
    protected Queue<Rune> mRunes;
    [SerializeField]
    protected int maxNumRunes = 50;
    protected void Awake()
    {
        //moved from start to awake b/c it was being 
        // accessed before initialization
        mRunes = new Queue<Rune>();
        testAddRunes();
    }
    protected void Start()
    {
        
    }

    void testAddRunes()
    { // add several runes
        for (int i = 0; i < 5; i++)
        {
            addRune(new Rune(1, 0, 0));
            addRune(new Rune(0, 1, 0));
            addRune(new Rune(0, 0, 1));
            addRune(new Rune(1, 1, 1));
            addRune(new Rune(3, 5, 2));
        }
    }

    public bool addRune(Rune rune)
    {
        if(mRunes.Count < maxNumRunes)
        {
            mRunes.Enqueue(rune);
            return true;
        }
        return false;
    }

    virtual public Rune getRune()
    {
        if (mRunes.Count <= 0)
            return null;
        return mRunes.Dequeue();
    }

    public int getNumRunes()
    {
        return mRunes.Count;
    }
    public int getMaxRunes()
    {
        return maxNumRunes;
    }
}
