using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceTowerScript : MonoBehaviour
{
    [SerializeField]
    private Color highlightColor = new Color(1.0f, 0.0f, 0.0f);
    private Color originalColor;

    [SerializeField]
    private GameObject tower = null;
    private GameObject ownTower = null;

    [SerializeField]
    float costOfTower = 5f;

    private Material objectMaterial;
    BaseScript baseScript = null;
    // Start is called before the first frame update
    void Start()
    {
        objectMaterial = GetComponent<Renderer>().material;
        originalColor = objectMaterial.color;
        baseScript = GameObject.Find("Base").GetComponent<BaseScript>();
    }



    private void OnMouseEnter()
    {
        objectMaterial.SetColor("_Color", highlightColor);
    }

    private void OnMouseOver()
    {
        // 0 is left, 1 right, 2 middle
        if(Input.GetMouseButtonDown(0))
        {
            if (ownTower == null && transform.childCount == 0 && baseScript.spendGold(costOfTower, "Tower"))
            {
                ownTower = Instantiate(tower, gameObject.transform);
                ownTower.transform.Rotate(-90, 0, 0);
                ownTower.transform.Translate(new Vector3(0.0f, 1.0f, 0.0f));
            }
        }
    }

    private void OnMouseExit()
    {
        objectMaterial.SetColor("_Color", originalColor); 
    }
}
