using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIState
{
    protected Enemy enemy;

    public abstract void Tick();
    private int stateId;

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
    public virtual void OnStateEnter() { }
    public virtual void OnStateExit() { }

    public AIState(Enemy enemy)
    {
        this.enemy = enemy;
    }
}

public class PatrollingState : AIState
{
    private Vector3 destination;

    public PatrollingState(Enemy enemy) : base(enemy)
    {

    }

    public override void Tick()
    {
        enemy.Patrol();
    }

    public override void OnStateEnter()
    {
        StateId = 1;
    }

    public override void OnStateExit()
    {

    }
}

public class ShootingState : AIState
{
    private Vector3 destination;

    public ShootingState(Enemy enemy) : base(enemy)
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

public class HoverState : AIState
{

    public HoverState(Enemy enemy) : base(enemy)
    {
        
    }

    public override void Tick()
    {
        enemy.Hover();
    }

    public override void OnStateEnter()
    {
        
    }

    public override void OnStateExit()
    {

    }
}
