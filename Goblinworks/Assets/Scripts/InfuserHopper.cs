using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfuserHopper : RuneHopper
{
    Queue<Rune> outGoingRunes;
    [SerializeField]
    int maxNumOutgoingRunes = 50;
    private void Awake()
    {
        //moved from start to awake b/c it was being 
        // accessed by infuser script before initialization
        base.Awake();
        outGoingRunes = new Queue<Rune>();
    }
    new void Start()
    {
        
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
    public bool InfuseRune(Rune.TYPE type)
    {
        if (mRunes.Count > 0
          && getNumOutRunes() < maxNumOutgoingRunes)
        {
            Rune rune = mRunes.Dequeue();
            rune.infuse(type);
            outGoingRunes.Enqueue(rune);
            return true;
        }
        return false;
    }
}
