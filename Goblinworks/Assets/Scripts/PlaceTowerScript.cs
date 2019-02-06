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

    private Material objectMaterial;

    [SerializeField]
    [Tooltip("This doesn't need to be set")]
    GameObject goldObject;
    // Start is called before the first frame update
    void Start()
    {
        objectMaterial = GetComponent<Renderer>().material;
        originalColor = objectMaterial.color;
    }

    // Update is called once per frame
    void Update()
    {
        goldObject = GameObject.Find("GoldText");
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
            if(ownTower == null && transform.childCount == 0 && goldObject.GetComponent<GoldScript>().GetGold() >= 5)
            {
                ownTower = Instantiate(tower, gameObject.transform);
                ownTower.transform.Rotate(90, 0, 0);
                ownTower.transform.Translate(new Vector3(0.0f, -1.0f, 0.0f));
                goldObject.GetComponent<GoldScript>().ChangeGold(-5);
            }
        }
    }

    private void OnMouseExit()
    {
        objectMaterial.SetColor("_Color", originalColor); 
    }
}
