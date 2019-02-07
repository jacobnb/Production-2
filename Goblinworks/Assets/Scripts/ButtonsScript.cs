using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsScript : MonoBehaviour
{
    BaseScript baseScript = null;
    // Start is called before the first frame update
    void Start()
    {
        baseScript = GameObject.Find("Base").GetComponent<BaseScript>();


        if (!baseScript)
            Debug.LogError("Couldn't find Base / BaseScript");
    }

    public void fuckAGoblin()
    {
        baseScript.queueGoblin(1);
    }
}
