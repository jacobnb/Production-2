using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiringController : MonoBehaviour
{
    //Centralized targeting b/c 
    //i think it'll be faster than collider based.
    List<EnemyScript> enemies;
    List<TowerScript> towers;
    // Start is called before the first frame update
    void Start()
    {
        enemies = new List<EnemyScript>();
        towers = new List<TowerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        clearDead();
        setTowerTargets();
    }

    void clearDead()
    {
        for(int i=0; i < enemies.Count; i++)
        {
            EnemyScript es = enemies[i];
            if (es.isDead())
            {
                enemies.RemoveAt(i);
                es.suicide();
            }
        }
    }
    void setTowerTargets()
    {
        if (enemies.Count > 0)
        {
            Vector3 towerLoc;
            EnemyScript closest = null;
            float minDist = float.MaxValue;
            foreach (TowerScript ts in towers)
            {
                towerLoc = ts.getLoc();
                foreach (EnemyScript es in enemies)
                {
                    float dist = Vector3.Distance(towerLoc, es.getLoc());
                    if (dist < minDist)
                    {
                        minDist = dist;
                        closest = es;
                    }
                }
                ts.setTarget(closest.getLoc());
            }
        }
        else
        {
            foreach(TowerScript ts in towers)
            {
                ts.setTarget(Vector3.zero, false);
            }
        }
    }

    public void addEnemy(EnemyScript es)
    {
        enemies.Add(es);
    }
    public void addTower(TowerScript tower)
    {
        towers.Add(tower);
    }
}
