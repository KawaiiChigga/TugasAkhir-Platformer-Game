using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command  {

    public abstract void Execute();
    public abstract void Execute(Robot robot);

}

public class DoNothing : Command
{
    public override void Execute()
    {

    }

    public override void Execute(Robot robot)
    {
        
    }
}

public class FireWeapon : Command
{
    public override void Execute(Robot player)
    {
        player.Shoot();
    }

    public override void Execute()
    {
        
    }
}

public class Slowtime : Command
{
    public override void Execute()
    {
        Time.timeScale /= 2;
        Time.fixedDeltaTime = 0.02F * Time.timeScale;
    }

    public override void Execute(Robot robot)
    {

    }
}

public class NormalizeTime : Command
{
    public override void Execute()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02F;
    }

    public override void Execute(Robot robot)
    {

    }
}