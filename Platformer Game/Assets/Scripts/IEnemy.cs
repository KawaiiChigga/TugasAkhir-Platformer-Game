using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IEnemy : Robot
{
    private Transform fallCheck;
    public bool findEdge = true;
    protected AIState currentAIState;
    public bool isFlyer = false;
    public AnimationCurve hoverCurve;
    private float startingY;
    void Start()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        groundCheck = gameObject.transform.Find("groundCheck").GetComponent<Transform>();
        fallCheck = gameObject.transform.Find("fallCheck").GetComponent<Transform>();
        SetState(new GroundedState(this));
        if (!isFlyer)
        {
            SetAIState(new PatrollingState(this));
        }
        else
        {
            SetAIState(new HoverState(this));
            startingY = transform.localPosition.y;
        }
    }

    void Update()
    {
        if (!isFlyer)
        {
            findEdge = Physics2D.Linecast(transform.position, fallCheck.position, 1 << LayerMask.NameToLayer("Ground"));
            grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
            if (grounded && currentState.GetType().Name != "GroundedState")
            {
                SetState(new GroundedState(this));
            }
        }
        else
        {
            SetState(new AirborneState(this));
        }
    }

    private void FixedUpdate()
    {
        Shoot();
        currentState.Tick();
        currentAIState.Tick();
        if (jump)
        {
            Jump();
            jump = false;
        }
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
        horizVel = Mathf.Clamp(moveDampening * horizVel + (moveSpeed / rb2d.mass) * h / Time.timeScale * Time.deltaTime, -maxSpeed, maxSpeed);
        Vector3 vel = rb2d.velocity;
        vel.x = horizVel;
        anim.SetFloat("Speed", Mathf.Abs(horizVel));
        rb2d.velocity = vel;
        DetectEdge();
    }

    public void Hover()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, startingY + hoverCurve.Evaluate(Time.time), transform.localPosition.z);
    }

    public void DetectEdge()
    {
        if (!findEdge && grounded)
        {
            ChangeDirection();
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
    public override void HealthChange()
    {
        
    }

}
