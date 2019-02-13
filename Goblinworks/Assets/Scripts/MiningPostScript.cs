using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MiningPostScript : MonoBehaviour
{
    MeshRenderer renderer;
    Material hoverMaterial;
    Material defaultMaterial;

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
        text.text = "Goblins: " + numGoblins.ToString();
        // delegate an additional goblin
    }
}
