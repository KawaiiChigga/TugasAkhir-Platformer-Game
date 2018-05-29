using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    public Rigidbody2D projectile;              // Prefab of the rocket.

    private Robot robot;       // Reference to the PlayerControl script.
    private Animator anim;                  // Reference to the Animator component.
    public float cooldown;
    private float cdtimer;

    void Start()
    {
        // Setting up the references.
        anim = transform.parent.gameObject.GetComponentInParent<Animator>();
        robot = transform.parent.GetComponentInParent<Robot>();
        ResetCooldown();
    }


    void Update()
    {
        cdtimer += Time.deltaTime;
    }

    void FixedUpdate()
    {
        
    }

    public void ValidFire()
    {
        if (cdtimer >= cooldown)
        {
            Shoot();
            ResetCooldown();
        }
    }

    void Shoot()
    {
        anim.SetTrigger("Shoot");
        // ... set the animator Shoot trigger parameter and play the audioclip.
        GetComponent<AudioSource>().Play();

        // If the player is facing right...
        if (robot.FacingRight)
        {
            // ... instantiate the rocket facing right and set it's velocity to the right. 
            Rigidbody2D bulletInstance = Instantiate(projectile, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody2D;
            float bulletSpeed = bulletInstance.GetComponent<Projectile>().speed;
            bulletInstance.velocity = new Vector2(bulletSpeed, 0);
        }
        else
        {
            // Otherwise instantiate the rocket facing left and set it's velocity to the left.
            Rigidbody2D bulletInstance = Instantiate(projectile, transform.position, Quaternion.Euler(new Vector3(0, 0, 180f))) as Rigidbody2D;
            float bulletSpeed = bulletInstance.GetComponent<Projectile>().speed;
            bulletInstance.velocity = new Vector2(-bulletSpeed, 0);
        }
    }
    
    public void ResetCooldown()
    {
        cdtimer = 0;
    }

}
