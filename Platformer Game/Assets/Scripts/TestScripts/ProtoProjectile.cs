using UnityEngine;
using System.Collections;

public class ProtoProjectile : MonoBehaviour
{
    public GameObject explosion;        // Prefab of explosion effect.
    public float lifetime;
    public float speed = 20f;
    public float damage = 10f;
    public bool penetrating = false;
    float timer = 0;

    void Start()
    {
        // Destroy the rocket after 2 seconds if it doesn't get destroyed before then.
        
        
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > lifetime)
        {
            GetComponent<Prototype>().ReturnToPool();
            OnExplode();
            timer = 0;
        }
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
        if (col.tag == "Enemy" || col.tag == "Destroyable" || col.tag == "Player")
        {
            // Destroy the rocket.
            if (!penetrating)
            {
                GetComponent<Prototype>().ReturnToPool();
                OnExplode();
            }
            col.SendMessageUpwards("Damaged", damage);
        }
        // Otherwise if the player manages to shoot himself...
        else
        {
            if (col.tag == "Rider" && gameObject.tag != "Platform")
            {
                col.SendMessageUpwards("Damaged", damage);
            }
            GetComponent<Prototype>().ReturnToPool();
            OnExplode();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D col = collision.collider;
        if (col.tag != "Rider")
        {
            GetComponent<Prototype>().ReturnToPool();
            OnExplode();
            Debug.Log("1");
        }
    }
}
