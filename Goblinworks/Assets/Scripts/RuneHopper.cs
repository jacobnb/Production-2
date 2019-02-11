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
    }

    public void addRune(Rune rune)
    {
        mRunes.Enqueue(rune);
    }

    public Rune getRune()
    {
        return mRunes.Dequeue();
    }

    public int getNumRunes()
    {
        return mRunes.Count;
    }
}
