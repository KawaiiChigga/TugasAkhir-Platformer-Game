using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{


    public Prototype enemyPrototype;
    public IEnemy[] enemy;
    public float spawntime = 5;
    private float count = 0;
    private int currentEnemyCount = 0;
    // Use this for initialization
    void Start()
    {
        enemy = new IEnemy[99];
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (count >= spawntime)
        {
            count = 0;
            var newEnemy = enemyPrototype.Instantiate<Robot>();
        }
        count += Time.deltaTime;
        */
    }
    public void Spawn()
    {
        var newEnemy = enemyPrototype.Instantiate<IEnemy>();
        enemy[currentEnemyCount] = newEnemy;
        currentEnemyCount++;
    }

    public void DeleteLast()
    {
        enemy[currentEnemyCount - 1].Kill();
        currentEnemyCount--;
    }
}
