using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameState
{

    public abstract void Tick();
    public static float originalTimeScale = 1f;
    public virtual void OnStateEnter() { }
    public virtual void OnStateExit() { }

}

public class PlayingState : GameState
{

    public override void Tick()
    {

    }

    public override void OnStateEnter()
    {
        Time.timeScale = originalTimeScale;
    }

    public override void OnStateExit()
    {

    }
}

public class PauseState : GameState
{

    public override void Tick()
    {

    }

    public override void OnStateEnter()
    {
        originalTimeScale = Time.timeScale;
        Time.timeScale = 0;
    }

    public override void OnStateExit()
    {

    }
}

public class GameOverState : GameState
{

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

public class MainMenuState : GameState
{

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

public class FinishState : GameState
{

    public override void Tick()
    {

    }

    public override void OnStateEnter()
    {
        originalTimeScale = Time.timeScale;
        Time.timeScale = 0;
    }

    public override void OnStateExit()
    {

    }
}