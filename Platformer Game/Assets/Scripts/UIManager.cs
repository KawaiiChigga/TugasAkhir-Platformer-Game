using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public Image[] healths;
    public GameObject[] pauseMenu;
    public GameObject[] overMenu;
    public GameObject[] mainMenu;
    public GameObject[] optionMenu;
    // Use this for initialization
    void Start()
    {
        pauseMenu = GameObject.FindGameObjectsWithTag("ShowOnPause");
        overMenu = GameObject.FindGameObjectsWithTag("ShowOnGameOver");
        mainMenu = GameObject.FindGameObjectsWithTag("ShowOnMainMenu");
        optionMenu = GameObject.FindGameObjectsWithTag("ShowOnOption");
        hideOption();
    }

    void Update()
    {
        if (DataManager.instance.CheckGameState("PauseState"))
        {
            showPaused();
        }
        else
        {
            hidePaused();
            if (DataManager.instance.CheckGameState("GameOverState"))
            {
                showOver();
            }
            else
            {
                hideOver();
            }
        }
    }
    // Update is called once per frame
    public void UpdateHealth(int health)
    {
        for (int i = 0; i < healths.Length; i++)
        {
            if (i < health/10)
            {
                healths[i].gameObject.SetActive(true);
            }
            else
            {
                healths[i].gameObject.SetActive(false);
            }

        }
    }

    /*
     public void ButtonLevelOne(){
        SceneManager.LoadScene("LevelOne");   
     }
         */

    public void LoadLevel(string level)
    {
        new LoadScene().Execute(level);
    }

    public void MainMenu()
    {
        new LoadMainMenu().Execute();
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

    public void ConfirmOption()
    {
        hideOption();
        ShowMain();
    }

    public void Option()
    {
        hideMain();
        showOption();
    }

    public void showPaused()
    {
        foreach (GameObject g in pauseMenu)
        {
            g.SetActive(true);
        }
    }

    public void hidePaused()
    {
        foreach (GameObject g in pauseMenu)
        {
            g.SetActive(false);
        }
    }

    public void showOver()
    {
        foreach (GameObject g in overMenu)
        {
            g.SetActive(true);
        }
    }

    public void hideOver()
    {
        foreach (GameObject g in overMenu)
        {
            g.SetActive(false);
        }
    }

    public void ShowMain()
    {
        foreach (GameObject g in mainMenu)
        {
            g.SetActive(true);
        }
    }

    public void hideMain()
    {
        foreach (GameObject g in mainMenu)
        {
            g.SetActive(false);
        }
    }

    public void showOption()
    {
        foreach (GameObject g in optionMenu)
        {
            g.SetActive(true);
        }
    }

    public void hideOption()
    {
        foreach (GameObject g in optionMenu)
        {
            g.SetActive(false);
        }
    }

    public void changeVolume()
    {
        AudioListener.volume = GameObject.Find("VolumeSlider").GetComponent<Slider>().value;
    }

}
