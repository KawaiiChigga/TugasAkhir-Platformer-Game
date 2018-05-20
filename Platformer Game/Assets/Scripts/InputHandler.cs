using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour {

    public IPlayer player;
    private Command buttonZ, buttonX, buttonLeftMouse, buttonESC;

    // Use this for initialization
    void Start () {
        buttonZ = new Slowtime();
        buttonX = new NormalizeTime();
        buttonLeftMouse = new FireWeapon();
        buttonESC = new PauseGame();
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
            else if (Input.GetKeyDown(KeyCode.X))
            {
                buttonX.Execute();
            }
            else if (Input.GetMouseButton(0))
            {
                buttonLeftMouse.Execute(player);
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
        }
    }
    
}
