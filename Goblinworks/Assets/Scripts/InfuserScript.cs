﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfuserScript : MonoBehaviour
{
    InfuserHopper runeHopper;
    [SerializeField]
    float infuseTime = 5f;
    float infuseTimer;
    [SerializeField]
    Rune.TYPE infuseType = Rune.TYPE.FIRE;
    // Start is called before the first frame update
    void Start()
    {
        runeHopper = gameObject.GetComponent<InfuserHopper>();
        infuseTimer = infuseTime;
        setColor();
    }

    public void setType(Rune.TYPE type)
    {
        infuseType = type;
        setColor();
    }
    void setColor()
    {
        Color color = new Color(0f, 0f, 0f);
        switch (infuseType)
        {
            case Rune.TYPE.FIRE:
                color.r = 1;
                break;
            case Rune.TYPE.EARTH:
                color.g = 1;
                break;
            case Rune.TYPE.WATER:
                color.b = 1;
                break;
        }
        gameObject.GetComponent<Renderer>().material.color = color;
    }
    // Update is called once per frame
    void Update()
    {
        checkIfCanInfuse();
    }

    void checkIfCanInfuse()
    {
      
        infuseTimer -= Time.deltaTime;
        if (infuseTimer <= 0f)
        {
            runeHopper.InfuseRune(infuseType);
            infuseTimer = infuseTime;
        }
    }
}
