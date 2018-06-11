using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugInput : MonoBehaviour
{

    private Command buttonZ, buttonX, buttonLeftMouse, buttonESC;
    public Player[] players;

    // Use this for initialization
    void Start()
    {
        buttonZ = new Slowtime();
        buttonX = new NormalizeTime();
        buttonLeftMouse = new FireWeapon();
        buttonESC = new PauseGame();
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    public void HandleInput()
    {
        if (DataManager.instance.CheckGameState("PlayingState"))
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                buttonZ.Execute();
            }
            else if (Input.GetKeyDown(KeyCode.X))
            {
                buttonX.Execute();
            }
            else if (Input.GetMouseButton(0))
            {
                foreach (Player p in players)
                {
                    buttonLeftMouse.Execute(p);
                }

            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                buttonESC.Execute();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                buttonESC.Execute();
            }
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Debug.Log(DataManager.gameState);
            }
        }
    }

}
