using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public int damage;
    public Spawner [] spawners;
    public bool isTrigger;
    public bool isOneTime;
    private bool activated = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(activated == true)
        {
            TriggerSpawn();
            if (isOneTime)
            {
                activated = false;
                isTrigger = false;
            }
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D col = collision.collider;
        if (damage > 0)
        {
            col.SendMessageUpwards("Damaged", damage);
        }
        if (isTrigger)
        {
            if(activated == false)
            {
                activated = true;
            }
            else
            {
                activated = false;
            }
        }
    }

    private void TriggerSpawn()
    {
        foreach (Spawner spawner in spawners)
        {
            if (spawner.Timer >= spawner.Spawntime)
            {
                spawner.Spawn();
                spawner.Timer = 0;
            }
            else
            {
                spawner.Timer += Time.deltaTime;
            }
        }
    }
}
