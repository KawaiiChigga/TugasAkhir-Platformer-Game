using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour {

    public Player player;
    private Command buttonZ, buttonX, buttonLeftMouse, buttonESC, buttonSpace, buttonLeftShift;

    // Use this for initialization
    void Start () {
        buttonZ = new Slowtime();
        buttonX = new NormalizeTime();
        buttonLeftMouse = new FireWeapon();
        buttonESC = new PauseGame();
        buttonSpace = new JumpCommand();
        buttonLeftShift = new DashCommand();
    }
	
	// Update is called once per frame
	void Update () {
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
            if (Input.GetKeyDown(KeyCode.X))
            {
                buttonX.Execute();
            }
            if (Input.GetMouseButton(0))
            {
                buttonLeftMouse.Execute(player);
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                buttonESC.Execute();
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                buttonSpace.Execute(player);
            }
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                buttonLeftShift.Execute(player);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                buttonESC.Execute();
            }
        }
    }
    
}
