using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneHopper : MonoBehaviour
{
    //Base class to hold runes for towers
    Queue<Rune> mRunes;
    [SerializeField]
    int maxNumRunes = 50;
    private void Start()
    {
        mRunes = new Queue<Rune>();
        testAddRunes();
    }

    void testAddRunes()
    { // add several runes
        for (int i = 0; i < 10; i++)
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

    public Rune getRune()
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
