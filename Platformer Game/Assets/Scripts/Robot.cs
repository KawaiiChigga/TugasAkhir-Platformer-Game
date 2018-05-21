using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Robot : MonoBehaviour
{

    public bool facingRight = true;
    [HideInInspector] public bool jump = false;
    [HideInInspector] public bool dash = false;
    public float moveSpeed = 45f;
    public float maxSpeed = 8f;
    public float jumpForce = 750f;
    protected Transform groundCheck;
    protected float horizVel = 0f;
    protected static Vector3 mirrorX = new Vector3(-1, 1, 1);
    public float moveDampening = 0.2f;
    public int startingHealth = 3;
    public int currentHealth = 3;
    public bool grounded = false;
    protected Animator anim;
    protected Rigidbody2D rb2d;
    protected State currentState;
    public bool startfacingRight;
    protected bool dashRecharged;

    // Use this for initialization
    void Start()
    {
        if (startfacingRight == false)
        {
            ChangeDirection();
            facingRight = false;
        }
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        groundCheck = gameObject.transform.Find("groundCheck").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    // Ngga kepengaruh frame rate   
    private void FixedUpdate()
    {

    }

    public void Jump()
    {
        if (CheckState("GroundedState"))
        {
            rb2d.AddForce(new Vector2(0, jumpForce / Time.timeScale));
        }
    }

    abstract public void Kill();

    public void Dash()
    {
        if (!facingRight)
        {
            rb2d.AddForce(new Vector2(-500, 0));
        }
        else
        {
            rb2d.AddForce(new Vector2(500, 0));
        }
        dashRecharged = false;
    }

    public void SetState(State state)
    {
        if (currentState != null)
            currentState.OnStateExit();

        currentState = state;
        gameObject.name = "Robot - " + state.GetType().Name;

        if (currentState != null)
            currentState.OnStateEnter();
    }

    public bool CheckState(string state)
    {
        return currentState.GetType().Name == state;
    }

    public void TriggerAnimation(string animation)
    {
        anim.SetTrigger(animation);
    }

    public abstract void HealthChange();

    public void Shoot()
    {
        if (!CheckState("DashingState")) //No firing when dashing//
        {
            GetComponentInChildren<Weapon>().ValidFire();
        }
    }

    public void ChangeDirection()
    {
        transform.localScale = Vector3.Scale(transform.localScale, mirrorX);
        facingRight = !facingRight;
    }

    public void RechargeDash()
    {
        dashRecharged = true;
    }


}
