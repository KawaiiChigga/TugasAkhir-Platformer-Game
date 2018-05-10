using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour {
    public Image[] healths;
    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    public void UpdateHealth (int health) {
        for (int i = 0; i < healths.Length; i++)
        {
            if (i < health)
            {
                healths[i].gameObject.SetActive(true);
            }
            else
            {
                healths[i].gameObject.SetActive(false);
            }
            
        }
	}
}
