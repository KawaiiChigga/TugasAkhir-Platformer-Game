using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    public GameObject explosion;
    public float lifetime;
    public float speed = 20f;
    public float damage = 10f;
    public bool penetrating = false;
    private float timer = 0;

    void Start()
    {
    }

    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (timer > lifetime)
        {
            Destroy(gameObject);
        }
    }


    void OnExplode()
    {
        Quaternion randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
        Instantiate(explosion, transform.position, randomRotation);
    }

    private void OnDestroy()
    {
        OnExplode();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Enemy" || col.tag == "Destroyable" || col.tag == "Player")
        {
            if (!penetrating)
            {
                Destroy(gameObject);
            }
            col.SendMessageUpwards("Damaged", damage);
        }
        else
        {
            if (col.tag == "Rider" && gameObject.tag != "Platform")
            {
                col.SendMessageUpwards("Damaged", damage);
            }
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D col = collision.collider;
        if (col.tag != "Rider")
        {
            Destroy(gameObject);
            Debug.Log("1");
        }
    }
}
