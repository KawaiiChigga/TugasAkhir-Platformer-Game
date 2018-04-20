using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour {


    public Prototype enemyPrototype;
    public float spawntime = 5;
    private float count = 0;
    // Use this for initialization
    void Start () {
        var newEnemy = enemyPrototype.Instantiate<Robot>();
    }
	
	// Update is called once per frame
	void Update () {
        if (count >= spawntime)
        {
            count = 0;
            var newEnemy = enemyPrototype.Instantiate<Robot>();
        }
        count += Time.deltaTime;
	}
}
