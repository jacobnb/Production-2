using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TowerScript : MonoBehaviour
{
    [SerializeField]
    float towerRange = 5f;
    [SerializeField]
    float health;
    [SerializeField]
    [Tooltip("Time between shots in seconds")]
    float reloadTime = 0.5f;
    [SerializeField]
    GameObject cannonball = null;
    RuneHopper mRuneHopper;
    bool hasTarget = false;
    Vector3 mTarget;
    Transform t;
    float shootTimer;
    TextMeshProUGUI mouseOverText;
    Canvas mouseOver;
    // Start is called before the first frame update
    void Start()
    {
        mRuneHopper = gameObject.GetComponent<RuneHopper>();
        GameObject.Find("GameController")
            .GetComponent<FiringController>()
            .addTower(this);
        t = gameObject.transform;
        shootTimer = 0f;
        mouseOver = gameObject.GetComponentInChildren<Canvas>();
        mouseOverText = mouseOver.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void OnMouseOver()
    {
        mouseOver.enabled = true;
        mouseOverText.text = "Tower Runes " + 
            mRuneHopper.getNumRunes() + "/" + 
            mRuneHopper.getMaxRunes();
    }
    private void OnMouseExit()
    {
        mouseOver.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (hasTarget)
        {
            shoot();
        }
    }

    void shoot()
    {
        shootTimer -= Time.deltaTime;
        if(shootTimer <= 0)
        {
            Rune rune = mRuneHopper.getRune();
            float colorRunes = 255f;
            if (rune != null)
            {
                shootTimer = reloadTime;
                GameObject ball = Instantiate(cannonball, t);
                ball.GetComponent<CannonballScript>().setTargetAndPosit(mTarget, t.position);
                ball.GetComponent<Renderer>().material.color = 
                    new Color(
                    rune.fireCharges*colorRunes
                    , rune.earthCharges * colorRunes
                    , rune.waterCharges * colorRunes);
            }
        }
    }

    public void setTarget(Vector3 target, bool targetInRange)
    {
        hasTarget = targetInRange;
        mTarget = target;
    }
    public void setTarget(Vector3 target)
    {
        mTarget = target;
        hasTarget = true;
        if (Vector3.Distance(mTarget, t.position) > towerRange)
            hasTarget = false;
    }
    public Vector3 getLoc()
    {
        return t.position;
    }

}
