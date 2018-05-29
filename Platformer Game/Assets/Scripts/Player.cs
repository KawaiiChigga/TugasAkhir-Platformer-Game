using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Robot
{
    private float dashTimer;
    public GameObject UIManager;
    // Use this for initialization
    void Awake()
    {
        UIManager.SendMessage("UpdateHealth", CurrentHealth);
        SetState(new GroundedState(this));
    }

    // Update is called once per frame
    void Update()
    {
        // Check if Player is touching a ground

        Grounded = Physics2D.Linecast(transform.position, GroundCheck.position, 1 << LayerMask.NameToLayer("Ground")) || Physics2D.Linecast(transform.position, GroundCheck.position, 1 << LayerMask.NameToLayer("Projectile"));

        if (!CheckState(3))
        {
            if (Grounded && !CheckState(1))
            {
                SetState(new GroundedState(this));
            }
            else
            {
                if (!Grounded && !CheckState(2))
                {
                    SetState(new AirborneState(this));
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (DashRecharged)
            {
                SetState(new DashingState(this));
            }

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
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
        CurrentState.Tick();
        float h = GetMovementInput();
        HorizVel = Mathf.Clamp(MoveDampening * HorizVel + (MoveSpeed / Rb2d.mass) * h / Time.timeScale * Time.deltaTime, -MaxSpeed, MaxSpeed);
        Vector3 vel = Rb2d.velocity;
        vel.x = HorizVel;
        Anim.SetFloat("Speed", Mathf.Abs(HorizVel));
        if (!CheckState(3))
        {
            Rb2d.velocity = vel;
        }
        if ((h < -0.01f && FacingRight) || (h > 0.01f && !FacingRight))
        {
            FlipObject();
        }

    }

    private float GetMovementInput()
    {
        return Input.GetAxis("Horizontal"); // Input Kiri dan Kanan
    }

    public override void Kill()
    {
        DataManager.instance.SetGameState(new GameOverState());
        Destroy(gameObject);
    }

    public override void HealthChange()
    {
        SetState(new DamagedState(this));
        UIManager.SendMessage("UpdateHealth", CurrentHealth);
    }
}
