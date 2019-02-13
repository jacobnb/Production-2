using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rune
{
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
    //these should all default to zero
    public int fireCharges;
    public int earthCharges;
    public int waterCharges;
}
