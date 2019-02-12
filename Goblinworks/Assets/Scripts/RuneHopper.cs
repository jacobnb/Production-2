using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneHopper : MonoBehaviour
{
    //Base class to hold runes for towers
    Queue<Rune> mRunes;

    private void Start()
    {
        mRunes = new Queue<Rune>();
        testAddRunes();
    }

    void testAddRunes()
    { // add several runes
        for (int i = 0; i < 100; i++)
        {
            addRune(new Rune()); addRune(new Rune()); addRune(new Rune()); addRune(new Rune()); addRune(new Rune()); addRune(new Rune()); addRune(new Rune()); addRune(new Rune()); addRune(new Rune()); addRune(new Rune()); addRune(new Rune());
        }
    }

    public void addRune(Rune rune)
    {
        mRunes.Enqueue(rune);
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
}
