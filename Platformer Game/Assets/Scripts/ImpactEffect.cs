using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactEffect : MonoBehaviour {
    public float lifetime;
    // Use this for initialization
    void Start () {
        Destroy(gameObject, lifetime);
        
    }
	
	// Update is called once per frame
	void Update () {
        transform.localScale += new Vector3(0.025f,0.025f,0.025f);
        Color tmp = GetComponent<SpriteRenderer>().color;
        tmp.a = tmp.a - 0.05f;
        GetComponent<SpriteRenderer>().color = tmp;
    }
}
