using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rune
{
    public enum TYPE
    {
        FIRE, EARTH, WATER
    }
    public Rune()
    {
        fireCharges = 0;
        earthCharges = 0;
        waterCharges = 0;
    }
    public Rune(int fire, int earth, int water)
    {
        fireCharges = fire;
        earthCharges = earth;
        waterCharges = water;
    }
    public void infuse(TYPE type)
    {
        switch (type)
        {
            case TYPE.FIRE:
                fireCharges++;
                break;
            case TYPE.EARTH:
                earthCharges++;
                break;
            case TYPE.WATER:
                waterCharges++;
                break;
        }
    }
    public int fireCharges;
    public int earthCharges;
    public int waterCharges;
}
