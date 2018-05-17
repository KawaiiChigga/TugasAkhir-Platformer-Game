using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IPlayer : Robot
{

    public GameObject healthUI;
    // Use this for initialization
    void Start()
    {
        healthUI.SendMessage("UpdateHealth", currentHealth);
        SetState(new GroundedState(this));
    }

    // Update is called once per frame
    void Update()
    {
        // Check if Player is touching a ground

        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground")) || Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Projectile"));

        if (!CheckState("DashingState"))
        {
            if (grounded && !CheckState("GroundedState"))
            {
                SetState(new GroundedState(this));
            }
            else
            {
                if (!grounded && !CheckState("AirborneState"))
                {
                    SetState(new AirborneState(this));
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            SetState(new DashingState(this));
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            SceneManager.LoadSceneAsync(0);
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            SceneManager.LoadSceneAsync(1);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            currentHealth -= 1;
            HealthChange();
        }
        /* Example of no command pattern
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Time.timeScale /= 2f;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            Time.timeScale = 1f;
        }
        
        if (Input.GetButton("Fire1"))
        {
            GetComponentInChildren<WeaponManager>().ValidFire();
        }
        */
    }

    private void FixedUpdate() //Ga kepengaruh frame per second
    {
        currentState.Tick();
        float h = GetMovementInput();
        horizVel = Mathf.Clamp(moveDampening * horizVel + (moveSpeed / rb2d.mass) * h / Time.timeScale  * Time.deltaTime, -maxSpeed, maxSpeed);
        Vector3 vel = rb2d.velocity;
        vel.x = horizVel;
        anim.SetFloat("Speed", Mathf.Abs(horizVel));
        if (!CheckState("DashingState"))
        {
            rb2d.velocity = vel;
        }
        if ((h < -0.01f && facingRight) || (h > 0.01f && !facingRight))
        {

            transform.localScale = Vector3.Scale(transform.localScale, mirrorX);
            facingRight = !facingRight;
        }

    }

    private float GetMovementInput()
    {
        return Input.GetAxis("Horizontal"); // Input Kiri dan Kanan
    }

    public override void Kill()
    {
        Debug.Log("YOU DIED");
    }

    public override void HealthChange()
    {
        SetState(new DamagedState(this));
        healthUI.SendMessage("UpdateHealth", currentHealth);
    }

    
}
