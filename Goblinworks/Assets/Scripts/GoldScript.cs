using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldScript : MonoBehaviour
{
    private int gold = 0;
    private float timer;
    [SerializeField]
    Text goldText;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= .5)
        {
            gold++;
            timer = 0;
        }
        goldText.text = "Gold: " + gold;
    }

    public int GetGold()
    {
        return gold;
    }

    public void ChangeGold(int val)
    {
        gold += val;
    }
}
