using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour {

    public IPlayer player;
    private Command buttonZ, buttonX, buttonLeftMouse;

    // Use this for initialization
    void Start () {
        buttonZ = new Slowtime();
        buttonX = new NormalizeTime();
        buttonLeftMouse = new FireWeapon();
    }
	
	// Update is called once per frame
	void Update () {
        HandleInput();
    }

    public void HandleInput()
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
    }
}
