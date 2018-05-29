using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected Robot robot;
    private static int stateId;

    public int StateId
    {
        get
        {
            return stateId;
        }

        set
        {
            stateId = value;
        }
    }

    public abstract void Tick();

    public virtual void OnStateEnter() { }
    public virtual void OnStateExit() { }

    public State(Robot robot)
    {
        this.robot = robot;
    }
}

public class GroundedState : State
{
    private Vector3 destination;
    public GroundedState(Robot robot) : base(robot)
    {

    }

    public override void Tick()
    {

    }

    public override void OnStateEnter()
    {
        robot.RechargeDash();
        StateId = 1;
    }

    public override void OnStateExit()
    {

    }
}

public class AirborneState : State
{
    private Vector3 destination;

    public AirborneState(Robot robot) : base(robot)
    {

    }

    public override void Tick()
    {

    }

    public override void OnStateEnter()
    {
        StateId = 2;
    }

    public override void OnStateExit()
    {

    }
}

public class DashingState : State
{
    private Vector3 destination;
    private float timer = 0f;
    public DashingState(Robot robot) : base(robot)
    {

    }

    public override void Tick()
    {
        if (timer < 0.5f)
        {
            robot.Dash();
            timer += Time.deltaTime;
        }
        else
        {
            robot.SetState(new AirborneState(robot));
        }
    }

    public override void OnStateEnter() //Dashing ignore Y physics//
    {
        StateId = 3;
        timer = 0f;
        robot.TriggerAnimation("Skill");
        robot.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
    }

    public override void OnStateExit()
    {
        robot.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}

public class DamagedState : State
{
    private Vector3 destination;
    private Component[] bodyparts;
    public DamagedState(Robot robot) : base(robot)
    {

    }

    public override void Tick()
    {

    }

    public override void OnStateEnter()
    {
        StateId = 4;
    }

    public override void OnStateExit()
    {

    }
}