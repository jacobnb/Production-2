using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MiningPostScript : MonoBehaviour
{
    MeshRenderer renderer;
    Material hoverMaterial;
    Material defaultMaterial;

    int unassignedGoblins;
    int numGoblins = 0;
    [SerializeField]
    TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        renderer = gameObject.GetComponent<MeshRenderer>();
        defaultMaterial = renderer.material;
        hoverMaterial = Resources.Load<Material>("Materials/RuneMineHover");
    }

    // Update is called once per frame
    void Update()
    {
        if (unassignedGoblins > 0)
        {
            AssignGoblin();
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

        if(num > 0)
        {
            AssignGoblin();
        }
        else
        {
            RemoveGoblin();
        }
    }

    private void AssignGoblin()
    {
        // delegate an additional goblin       
        List<GameObject> goblins = GameObject.Find("Base").GetComponent<BaseScript>().GetGoblins();
        GoblinScript goblinScript;

        // should be able to just grab the scripts?
        // check if no one is doing something first (ideally this would be a dictionary in base of who isn't currently doing anything)
        foreach (GameObject goblin in goblins)
        {
            goblinScript = goblin.GetComponent<GoblinScript>();
            if (goblinScript.GetTask() == GoblinScript.Task.INVALID_TASK)
            {
                goblinScript.SetTask(GoblinScript.Task.MINE_TASK);
                unassignedGoblins--;
                return;
            }
        }
        // then pull first goblin who isn't mining
        foreach (GameObject goblin in goblins)
        {
            goblinScript = goblin.GetComponent<GoblinScript>();
            if (goblinScript.GetTask() != GoblinScript.Task.MINE_TASK)
            {
                goblinScript.SetTask(GoblinScript.Task.MINE_TASK);
                unassignedGoblins--;
                return;
            }
        }
    }

    void RemoveGoblin()
    {
        if(unassignedGoblins < 0)
        {
            List<GameObject> goblins = GameObject.Find("Base").GetComponent<BaseScript>().GetGoblins();
            GoblinScript goblinScript;

            foreach(GameObject goblin in goblins)
            {
                goblinScript = goblin.GetComponent<GoblinScript>();
                if(goblinScript.GetTask() == GoblinScript.Task.MINE_TASK)
                {
                    goblinScript.SetTask(GoblinScript.Task.INVALID_TASK);
                    unassignedGoblins++;
                    return;
                }
            }
        }
    }

    public int GetUnassigned()
    {
        return unassignedGoblins;
    }
}
