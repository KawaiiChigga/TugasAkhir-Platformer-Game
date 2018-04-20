using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
        protected Robot robot;

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
        if (timer < 0.2f)
        {
            robot.Dash();
            timer += Time.deltaTime;
        }
        else
        {
            robot.SetState(new AirborneState(robot));
        }
    }

    public override void OnStateEnter()
    {
        timer = 0f;
        robot.TriggerAnimation("Skill");
    }

    public override void OnStateExit()
    {

    }
}