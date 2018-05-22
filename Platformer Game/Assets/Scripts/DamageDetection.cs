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
        if(gameObject.tag=="Rider")
        {
            gameObject.GetComponentInParent<Robot>().CurrentHealth -= (int)damage;
            gameObject.GetComponentInParent<Robot>().HealthChange();
            if (gameObject.GetComponentInParent<Robot>().CurrentHealth <= 0)
            {
                gameObject.GetComponentInParent<Robot>().Kill();
            }
        }
    }
}
