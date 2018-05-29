using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{


    public Prototype enemyPrototype;
    public Enemy[] enemy;
    private float spawntime = 5;
    private float timer;
    private int currentEnemyCount = 0;
    public bool spawnAtRuntime = false;

    public float Spawntime
    {
        get
        {
            return spawntime;
        }

        set
        {
            spawntime = value;
        }
    }


    public float Timer
    {
        get
        {
            return Timer1;
        }

        set
        {
            Timer1 = value;
        }
    }

    public float Timer1
    {
        get
        {
            return timer;
        }

        set
        {
            timer = value;
        }
    }

    // Use this for initialization
    void Start()
    {
        enemy = new Enemy[99];
        if (spawnAtRuntime)
        {
            Spawn();
        }
        timer = spawntime;
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
        var newEnemy = enemyPrototype.Instantiate<Enemy>();
        enemy[currentEnemyCount] = newEnemy;
        currentEnemyCount++;
    }

    public void DeleteLast()
    {
        enemy[currentEnemyCount - 1].Kill();
        currentEnemyCount--;
    }
}
