﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IEnemy : Robot
{
    private Transform fallCheck;
    public bool findEdge = true;
    protected AIState currentAIState;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        groundCheck = gameObject.transform.Find("groundCheck").GetComponent<Transform>();
        fallCheck = gameObject.transform.Find("fallCheck").GetComponent<Transform>();
        SetState(new GroundedState(this));
        SetAIState(new PatrollingState(this));
    }

    void Update()
    {    
        findEdge = Physics2D.Linecast(transform.position, fallCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        if (grounded && currentState.GetType().Name != "GroundedState")
        {
            SetState(new GroundedState(this));
        }
    }

    private void FixedUpdate()
    {
        currentState.Tick();
        currentAIState.Tick();
        if (jump)
        {
            rb2d.AddForce(new Vector2(0, jumpForce));
            jump = false;
        }
        transform.Find("WeaponSlot").GetComponentInChildren<WeaponManager>().ValidFire();
    }

    public void Patrol()
    {
        float h;
        if (facingRight)
        {
            h = 1f;
        }
        else
        {
            h = -1f;
        }
        horizVel = Mathf.Clamp(moveDampening * horizVel + (moveSpeed / rb2d.mass) * h * Time.deltaTime, -maxSpeed, maxSpeed);
        Vector3 vel = rb2d.velocity;
        vel.x = horizVel;
        anim.SetFloat("Speed", Mathf.Abs(horizVel));
        if (!dashing)
        {
            rb2d.velocity = vel;
        }
        DetectEdge();
    }

    public void DetectEdge()
    {
        if (!findEdge && grounded)
        {
            transform.localScale = Vector3.Scale(transform.localScale, mirrorX);
            facingRight = !facingRight;
        }
    }

    public override void Kill()
    {
        GetComponent<Prototype>().ReturnToPool();
        currentHealth = startingHealth;
        facingRight = true;
    }

    public void SetAIState(AIState aistate)
    {
        if (currentAIState != null)
            currentAIState.OnStateExit();

        currentAIState = aistate;
        gameObject.name = "Enemy - " + aistate.GetType().Name;

        if (currentAIState != null)
            currentAIState.OnStateEnter();
    }

    public bool CheckAIState(string aistate)
    {
        return currentAIState.GetType().Name == aistate;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            Debug.Log("Touch");
        }

    }

}
