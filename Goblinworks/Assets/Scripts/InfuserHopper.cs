using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfuserHopper : RuneHopper
{
    Queue<Rune> outGoingRunes;
    [SerializeField]
    int maxNumOutgoingRunes = 50;
    // Start is called before the first frame update
    void Start()
    {
        outGoingRunes = new Queue<Rune>();
    }

    public Rune GetRune()
    {
        if (outGoingRunes.Count <= 0)
            return null;
        return outGoingRunes.Dequeue();
    }
    public int getNumOutRunes()
    {
        return outGoingRunes.Count;
    }
    public int getMaxOutRunes()
    {
        return maxNumOutgoingRunes;
    }
}
