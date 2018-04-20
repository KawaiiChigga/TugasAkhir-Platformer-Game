using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IPlayer : Robot {

    public GameObject healthUI;

    // Use this for initialization
    void Start () {
        healthUI.SendMessage("UpdateHealth", currentHealth);
        SetState(new GroundedState(this));
    }
	
	// Update is called once per frame
	void Update () {
        // Check if Player is touching a ground
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

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

        if (Input.GetKeyDown(KeyCode.E))
        {
            currentHealth -= 1;
            healthUI.SendMessage("UpdateHealth", currentHealth);
        }
    }

    private void FixedUpdate() //Ga kepengaruh frame per second
    {
        currentState.Tick();

        float h = Input.GetAxis("Horizontal"); // Input Kiri dan Kanan

        //Movement Code//
        anim.SetFloat("Speed", Mathf.Abs(h));
        horizVel = Mathf.Clamp(moveDampening * horizVel + (moveSpeed / rb2d.mass) * h * Time.deltaTime, -maxSpeed, maxSpeed);
        Vector3 vel = rb2d.velocity;
        vel.x = horizVel;

        if (!dashing)
        {
            rb2d.velocity = vel;
        }

        if ((h < -0.01f && facingRight) || (h > 0.01f && !facingRight))
        {

            transform.localScale = Vector3.Scale(transform.localScale, mirrorX);
            facingRight = !facingRight;
        }

    }

    


}
