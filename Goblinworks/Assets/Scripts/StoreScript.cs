using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StoreScript : MonoBehaviour
{
    MeshRenderer renderer;
    Material hoverMaterial;
    Material defaultMaterial;

    int unassignedGoblins;
    int numGoblins = 0;

    [SerializeField]
    TextMeshProUGUI text;

    RuneHopper runeHopper;
    // Start is called before the first frame update
    void Start()
    {
        renderer = gameObject.GetComponent<MeshRenderer>();
        defaultMaterial = renderer.material;
        hoverMaterial = Resources.Load<Material>("Materials/StoreHover");
        runeHopper = gameObject.GetComponent<RuneHopper>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Mining Post").GetComponent<MiningPostScript>().GetUnassigned() <= 0 && unassignedGoblins > 0)
        {
            AssignGoblin();
        }

        Rune rune = null;
        while((rune = runeHopper.getRune()) != null)
        {
            GameObject.Find("Base").GetComponent<BaseScript>().addGold(5);
        }
    }

    private void OnMouseOver()
    {
        renderer.material = hoverMaterial;
    }

    private void OnMouseExit()
    {
        renderer.material = defaultMaterial;
    }

    private void OnMouseDown()
    {
        transform.GetChild(0).gameObject.SetActive(!transform.GetChild(0).gameObject.activeSelf);
    }

    public void UIConfirm(int num)
    {
        numGoblins += num;
        unassignedGoblins += num;
        text.text = "Goblins: " + numGoblins.ToString();

        if (GameObject.Find("Mining Post").GetComponent<MiningPostScript>().GetUnassigned() <= 0)
        {
            if (num > 0)
            {
                AssignGoblin();
            }
            else
            {
                RemoveGoblin();
            }
        }
    }
    
    void AssignGoblin()
    {
        List<GameObject> goblins = GameObject.Find("Base").GetComponent<BaseScript>().GetGoblins();
        GoblinScript goblinScript;
        // check if no one is doing something first (ideally this would be a dictionary in base of who isn't currently doing anything)
        foreach (GameObject goblin in goblins)
        {
            goblinScript = goblin.GetComponent<GoblinScript>();
            if(goblinScript.GetTask() == GoblinScript.Task.INVALID_TASK)
            {
                goblinScript.SetTask(GoblinScript.Task.SELL_TASK);
                unassignedGoblins--;
                return;
            }
        }
        foreach(GameObject goblin in goblins)
        {
            goblinScript = goblin.GetComponent<GoblinScript>();
            if (goblinScript.GetTask() != GoblinScript.Task.MINE_TASK && goblinScript.GetTask() != GoblinScript.Task.SELL_TASK)
            {
                goblinScript.SetTask(GoblinScript.Task.SELL_TASK);
                unassignedGoblins--;
                return;
            }
        }
    }
    
    void RemoveGoblin()
    {
        if (unassignedGoblins < 0)
        {
            List<GameObject> goblins = GameObject.Find("Base").GetComponent<BaseScript>().GetGoblins();
            GoblinScript goblinScript;

            foreach (GameObject goblin in goblins)
            {
                goblinScript = goblin.GetComponent<GoblinScript>();
                if (goblinScript.GetTask() == GoblinScript.Task.SELL_TASK)
                {
                    goblinScript.SetTask(GoblinScript.Task.INVALID_TASK);
                    unassignedGoblins++;
                    return;
                }
            }
        }
    }

    public void DeallocGoblin()
    {
        unassignedGoblins++;
    }
}
