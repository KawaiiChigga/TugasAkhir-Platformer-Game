using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Robot
{
    private Transform fallCheck;
    public bool findEdge = true;
    protected AIState currentAIState;
    public bool isFlyer = false;
    public AnimationCurve hoverCurve;
    private float startingY;

    private void Awake()
    {
        Anim = GetComponent<Animator>();
        Rb2d = GetComponent<Rigidbody2D>();
        GroundCheck = gameObject.transform.Find("groundCheck").GetComponent<Transform>();
        fallCheck = gameObject.transform.Find("fallCheck").GetComponent<Transform>();
        SetState(new GroundedState(this));
    }

    void Start()
    {
       
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
            Grounded = Physics2D.Linecast(transform.position, GroundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
            if (Grounded && CurrentState.GetType().Name != "GroundedState")
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
        CurrentState.Tick();
        currentAIState.Tick();
    }

    public void Patrol()
    {
        float h;
        if (FacingRight)
        {
            h = 1f;
        }
        else
        {
            h = -1f;
        }
        HorizVel = Mathf.Clamp(MoveDampening * HorizVel + (MoveSpeed / Rb2d.mass) * h / Time.timeScale * Time.deltaTime, -MaxSpeed, MaxSpeed);
        Vector3 vel = Rb2d.velocity;
        vel.x = HorizVel;
        Anim.SetFloat("Speed", Mathf.Abs(HorizVel));
        Rb2d.velocity = vel;
        DetectEdge();
    }

    public void Hover()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, startingY + hoverCurve.Evaluate(Time.time), transform.localPosition.z);
    }

    public void DetectEdge()
    {
        if (!findEdge && Grounded)  
        {
            FlipObject();
        }
    }

    public override void Kill()
    {
        GetComponent<Prototype>().ReturnToPool();
        CurrentHealth = StartingHealth;
        GetComponentInChildren<Weapon>().ResetCooldown();
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

    public bool CheckAIState(int aistate)
    {
        return currentAIState.StateId == aistate;
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
