using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

public class PauseGame : Command
{
    public override void Execute()
    {
        if (DataManager.instance.CheckGameState("PauseState"))
        {
            DataManager.instance.SetGameState(new PlayingState());
        }
        else
        {
            DataManager.instance.SetGameState(new PauseState());
        }
    }

    public override void Execute(Robot robot)
    {

    }
}

public class RestartLevel : Command
{
    public override void Execute()
    {
        if (DataManager.instance.CheckGameState("PauseState"))
        {
            DataManager.instance.SetGameState(new PlayingState());
        }
        else
        {
            DataManager.instance.SetGameState(new PauseState());
        }
    }

    public override void Execute(Robot robot)
    {

    }
}

public class ReloadLevel : Command
{
    public override void Execute()
    {
        int scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
        DataManager.instance.SetGameState(new PlayingState());
    }

    public override void Execute(Robot robot)
    {

    }
}

public class LoadScene : Command
{
    public override void Execute()
    {

    }

    public override void Execute(Robot robot)
    {

    }

    public void Execute(string level)
    {
        SceneManager.LoadSceneAsync(level);
        DataManager.instance.SetGameState(new PlayingState());
    }
}

public class ExitCommand : Command
{
    public override void Execute()
    {
        Application.Quit();
    }

    public override void Execute(Robot robot)
    {

    }
}