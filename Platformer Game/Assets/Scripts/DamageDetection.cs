using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDetection : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Damaged(float damage)
    {
        Debug.Log(gameObject.name + " receives " + damage + " damage");
        if(gameObject.tag == "Enemy" || gameObject.tag == "Player")
        {
            gameObject.GetComponent<Robot>().currentHealth -= (int)damage;
            if (gameObject.GetComponent<Robot>().currentHealth <= 0)
            {
                gameObject.GetComponent<Robot>().Kill();
            }
        }
    }
}
