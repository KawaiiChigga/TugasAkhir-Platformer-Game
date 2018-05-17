using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIState
{
    protected IEnemy enemy;

    public abstract void Tick();

    public virtual void OnStateEnter() { }
    public virtual void OnStateExit() { }

    public AIState(IEnemy enemy)
    {
        this.enemy = enemy;
    }
}

public class PatrollingState : AIState
{
    private Vector3 destination;

    public PatrollingState(IEnemy enemy) : base(enemy)
    {

    }

    public override void Tick()
    {
        enemy.Patrol();
    }

    public override void OnStateEnter()
    {

    }

    public override void OnStateExit()
    {

    }
}

public class ShootingState : AIState
{
    private Vector3 destination;

    public ShootingState(IEnemy enemy) : base(enemy)
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

public class HoverState : AIState
{
    private Vector3 destination;

    public HoverState(IEnemy enemy) : base(enemy)
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
