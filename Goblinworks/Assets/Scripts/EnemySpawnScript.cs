using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnScript : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Wave spawn rate in seconds")]
    float enemySpawnRate = 5f;
    [SerializeField]
    float spawnMaxZOffset = 4f;
    [SerializeField]
    int numEnemiesPerWave = 5;
    [SerializeField]
    GameObject enemyFab = null;
    Vector3 enemySpawnPosit;
    float timer = 0f;
    FiringController firingController = null;
    Transform enemyFolder = null;

    // Start is called before the first frame update
    void Start()
    {
        enemyFolder = new GameObject("Enemy Folder").transform;
        enemySpawnPosit = transform.position;
        firingController = GameObject.Find("GameController").GetComponent<FiringController>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= enemySpawnRate)
        {
            timer = 0f;
            for(int i = 0; i < numEnemiesPerWave; i++)
            {
                enemySpawnPosit.x++;
                spawnEnemy();
            }
            enemySpawnPosit.x -= numEnemiesPerWave;
        }
    }

    void spawnEnemy()
    {
        GameObject enemy = Instantiate(enemyFab, enemyFolder);
        EnemyScript es = enemy.GetComponent<EnemyScript>();
        es.initPosit(getEnemySpawnposit());
        firingController.addEnemy(es);

    }
    Vector3 getEnemySpawnposit()
    {
        Vector3 spawnPosit = enemySpawnPosit;
        spawnPosit.z = Random.Range(enemySpawnPosit.z - spawnMaxZOffset, enemySpawnPosit.z + spawnMaxZOffset);
        return spawnPosit;
    }
}
