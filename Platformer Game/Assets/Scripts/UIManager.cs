using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour {
    public Image[] healths;
    public GameObject pauseMenu;
    // Use this for initialization
    void Start () {
        
    }

    void Update()
    {
        if (DataManager.instance.CheckGameState("PauseState"))
        {
            pauseMenu.SetActive(true);
        }
        else
        {
            pauseMenu.SetActive(false);
        }
    }
    // Update is called once per frame
    public void UpdateHealth (int health) {
        for (int i = 0; i < healths.Length; i++)
        {
            if (i < health)
            {
                healths[i].gameObject.SetActive(true);
            }
            else
            {
                healths[i].gameObject.SetActive(false);
            }
            
        }
	}

    public void LoadLevel(string level)
    {
        new LoadScene().Execute(level);
    }

    public void Restart()
    {
        new ReloadLevel().Execute();
    }

    public void Resume()
    {
        new PauseGame().Execute();
    }

    public void Quit()
    {
        new ExitCommand().Execute();
    }
}
