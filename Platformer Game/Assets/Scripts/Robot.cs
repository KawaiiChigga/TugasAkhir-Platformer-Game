using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Robot : MonoBehaviour //Alasan abstract class//
{
    //Getter Setter//
    [SerializeField] private bool facingRight;
    [HideInInspector] private bool jump1 = false;
    [HideInInspector] private bool dash1 = false;
    [SerializeField] private float moveSpeed = 45f;
    [SerializeField] private float maxSpeed = 8f;
    [SerializeField]  private float jumpForce = 750f;
    private Transform groundCheck;
    private float horizVel = 0f;
    private static Vector3 mirrorX = new Vector3(-1, 1, 1);
    private float moveDampening = 0.2f;
    [SerializeField]  private int startingHealth = 3;
    [SerializeField] private int currentHealth = 3;
    private bool grounded = false;
    private Animator anim;
    private Rigidbody2D rb2d;
    private RobotState currentState;
    private bool dashRecharged;

    // Use this for initialization
    void Start()
    {
        Anim = GetComponent<Animator>();
        Rb2d = GetComponent<Rigidbody2D>();
        GroundCheck = gameObject.transform.Find("groundCheck").GetComponent<Transform>();
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
        if (CheckState(1))
        {
            Rb2d.AddForce(new Vector2(0, JumpForce / Time.timeScale));
        }
    }

    abstract public void Kill();

    public abstract void Dash();

    public void DashMovement()
    {
        if (!FacingRight)
        {
            Rb2d.AddForce(new Vector2(-500, 0));
        }
        else
        {
            Rb2d.AddForce(new Vector2(500, 0));
        }
        DashRecharged = false;
    }

    public void SetState(RobotState state)
    {
        if (CurrentState != null)
            CurrentState.OnStateExit();

        CurrentState = state;
        gameObject.name = "Robot - " + state.GetType().Name;

        if (CurrentState != null)
            CurrentState.OnStateEnter();
    }

    public bool CheckState(int state)
    {
        return CurrentState.StateId == state;
    }

    public void TriggerAnimation(string animation)
    {
        Anim.SetTrigger(animation);
    }

    public abstract void HealthChange();

    public void Shoot()
    {
        if (!CheckState(3)) //No firing when dashing//
        {
            GetComponentInChildren<Weapon>().ValidFire();
            //GetComponentInChildren<ProtoWeapon>().ValidFire();
        }
    }

    public void SwapWeapon()
    {
        if (GetComponentInChildren<Weapon>().projectile.name.Equals("Bullet") && GetComponentInChildren<Weapon>().projectile!=null)
        {
            var bombweapon = Resources.Load("Prefabs/Projectiles/Bomb", typeof(Rigidbody2D)) as Rigidbody2D;
            GetComponentInChildren<Weapon>().cooldown = 1.5f;
            GetComponentInChildren<Weapon>().projectile = bombweapon;
        }
        else
        {
            var mgun = Resources.Load("Prefabs/Projectiles/Bullet", typeof(Rigidbody2D)) as Rigidbody2D;
            GetComponentInChildren<Weapon>().cooldown = 0.07f;
            GetComponentInChildren<Weapon>().projectile = mgun;
        }
    }

    public void FlipObject()
    {
        transform.localScale = Vector3.Scale(transform.localScale, MirrorX);
        FacingRight = !FacingRight;
    }

    public void RechargeDash()
    {
        DashRecharged = true;
    }

    // GETTER AND SETTER//

    public float MoveSpeed
    {
        get
        {
            return MoveSpeed1;
        }

        set
        {
            MoveSpeed1 = value;
        }
    }

    protected bool DashRecharged
    {
        get
        {
            return dashRecharged;
        }

        set
        {
            dashRecharged = value;
        }
    }

    protected RobotState CurrentState
    {
        get
        {
            return currentState;
        }

        set
        {
            currentState = value;
        }
    }

    public bool FacingRight
    {
        get
        {
            return FacingRight1;
        }

        set
        {
            FacingRight1 = value;
        }
    }

    public int CurrentHealth
    {
        get
        {
            return CurrentHealth1;
        }

        set
        {
            CurrentHealth1 = value;
        }
    }

    public bool FacingRight1
    {
        get
        {
            return facingRight;
        }

        set
        {
            facingRight = value;
        }
    }

    public bool Jump1
    {
        get
        {
            return jump1;
        }

        set
        {
            jump1 = value;
        }
    }

    public bool Dash1
    {
        get
        {
            return dash1;
        }

        set
        {
            dash1 = value;
        }
    }

    public float MoveSpeed1
    {
        get
        {
            return moveSpeed;
        }

        set
        {
            moveSpeed = value;
        }
    }

    public float MaxSpeed
    {
        get
        {
            return maxSpeed;
        }

        set
        {
            maxSpeed = value;
        }
    }

    public float JumpForce
    {
        get
        {
            return jumpForce;
        }

        set
        {
            jumpForce = value;
        }
    }

    protected Transform GroundCheck
    {
        get
        {
            return groundCheck;
        }

        set
        {
            groundCheck = value;
        }
    }

    protected float HorizVel
    {
        get
        {
            return horizVel;
        }

        set
        {
            horizVel = value;
        }
    }

    protected static Vector3 MirrorX
    {
        get
        {
            return mirrorX;
        }

        set
        {
            mirrorX = value;
        }
    }

    public float MoveDampening
    {
        get
        {
            return moveDampening;
        }

        set
        {
            moveDampening = value;
        }
    }

    public int StartingHealth
    {
        get
        {
            return startingHealth;
        }

        set
        {
            startingHealth = value;
        }
    }

    public int CurrentHealth1
    {
        get
        {
            return currentHealth;
        }

        set
        {
            currentHealth = value;
        }
    }

    public bool Grounded
    {
        get
        {
            return grounded;
        }

        set
        {
            grounded = value;
        }
    }

    protected Animator Anim
    {
        get
        {
            return anim;
        }

        set
        {
            anim = value;
        }
    }

    protected Rigidbody2D Rb2d
    {
        get
        {
            return rb2d;
        }

        set
        {
            rb2d = value;
        }
    }
}
