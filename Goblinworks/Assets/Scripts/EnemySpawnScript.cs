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
    List<EnemyScript> enemys;

    // Start is called before the first frame update
    void Start()
    {
        enemys = new List<EnemyScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void spawnEnemy()
    {

    }
}
