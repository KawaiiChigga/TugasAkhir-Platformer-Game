using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugOption : MonoBehaviour {
    public Spawner Spawner1;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Spawner1.Spawn();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Spawner1.DeleteLast();
        }
    }
}
