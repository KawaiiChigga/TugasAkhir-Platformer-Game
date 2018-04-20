using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    public GameObject explosion;        // Prefab of explosion effect.
    public float lifetime;
    public float speed = 20f;
    public float damage = 10f;

    void Start()
    {
        // Destroy the rocket after 2 seconds if it doesn't get destroyed before then.
        Destroy(gameObject, lifetime);
    }


    void OnExplode()
    {
        // Create a quaternion with a random rotation in the z-axis.
        Quaternion randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));

        // Instantiate the explosion where the rocket is with the random rotation.
        Instantiate(explosion, transform.position, randomRotation);
    }

    private void OnDestroy()
    {
        OnExplode();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // If it hits an enemy...
        if (col.tag == "Enemy" || col.tag == "Destroyable")
        {
            // ... find the Enemy script and call the Hurt function.
            // col.gameObject.GetComponent<Enemy>().Hurt();

            // Call the explosion instantiation.
            OnExplode();
            col.transform.SendMessage("Damaged", damage);
            // Destroy the rocket.
            Destroy(gameObject);
        }
        // Otherwise if the player manages to shoot himself...
        else
        {
            // Instantiate the explosion and destroy the rocket.
            OnExplode();
            Destroy(gameObject);
        }
    }
}
